using UnityEngine;

using UnityEditor;

using System;

using System.IO;

#if UNITY_5_3_OR_NEWER

using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

#endif



#region "�ڵ����� �ɼ�"

[Serializable]

struct AutoSaveOption

{

    public bool StartAutoSave;

    public bool PlayModeSceneSave;

    public bool CompileToSave;

    public bool TimedAutoSave;

    public int SaveTimeTickMinute;

}

#endregion



public class AutoSave : EditorWindow

{

    [SerializeField] static AutoSaveOption Option;

    static private DateTime lastSaveTime = DateTime.Now;

    static private Vector2 ScrollPosition = Vector2.zero;



    #region "������ ������"

    [MenuItem("Tools/AutoSave")]

    static void AutoSaveWindow()

    {

#if UNITY_5_3_OR_NEWER

   // Get existing open window or if none, make a new one:

   AutoSave window = (AutoSave)GetWindow(typeof(AutoSave));



   window.minSize = new Vector2(500f, 400f);

   window.maxSize = new Vector2(500f, 400f);



   window.autoRepaintOnSceneChange = true;

   window.Show();

#else

        Debug.LogError("Unity 5.3���� �̸��� ����Ҽ� �����ϴ�");

#endif

    }

    #endregion



    #region "������ �̺�Ʈ �Լ�"

    [InitializeOnLoadMethod]

    static private void OnInitalrize()

    {

        EditorApplication.update += update;

    }



    private void OnDestroy()

    {

        SaveAutoSaveOption();

        EditorSceneManager.sceneSaving -= (scene, path) => Debug.Log(DateTime.Now + " " + path + " ����Ϸ�");

    }



    private void OnEnable()

    {

        EditorSceneManager.sceneSaving += (scene, path) => Debug.Log(DateTime.Now + " " + path + " ����Ϸ�");

        try

        {

            LoadAutoSaveOption();

        }

        catch

        {

            Option = new AutoSaveOption();

            SaveAutoSaveOption();

        }

    }

    #endregion



    #region "�ɼ� ���� �ҷ�����"

    void SaveAutoSaveOption()

    {

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/AutoSaveOption.json");

        writer.Write(JsonUtility.ToJson(Option));

        writer.Close();

    }



    void LoadAutoSaveOption()

    {

        StreamReader reader = new StreamReader(Application.persistentDataPath + "/AutoSaveOption.json");

        Option = JsonUtility.FromJson<AutoSaveOption>(reader.ReadToEnd());

        reader.Close();

    }

    #endregion



    #region "�ڵ� ����"

    static private void update()

    {

        if (Option.StartAutoSave)

        {

            if (Option.TimedAutoSave && !EditorApplication.isPlaying)

            {

                TimeSpan TickTime = DateTime.Now - lastSaveTime;

                if (TickTime.TotalMinutes >= Option.SaveTimeTickMinute)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }



            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)

            {

                if (Option.PlayModeSceneSave)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }

            if (EditorApplication.isCompiling)

            {

                if (Option.CompileToSave)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }

        }

    }



    static void SaveScene()

    {

        SceneSetup[] Setup = EditorSceneManager.GetSceneManagerSetup();

        for (int i = 0; i < Setup.Length; i++)

        {

            if (Setup[i].isLoaded)

            {

                Scene scene = EditorSceneManager.GetSceneByPath(Setup[i].path);

                EditorSceneManager.SaveScene(scene);

            }

        }

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        Debug.Log("�ڵ� ���� �Ϸ�!");

    }

    #endregion



    #region "������ �����츦 �׸��ϴ�"

    void OnGUI()

    {

        if (!EditorApplication.isPlaying)

        {

            GUILayout.Label("ȯ�漳��", EditorStyles.boldLabel);

            Option.PlayModeSceneSave = GUILayout.Toggle(Option.PlayModeSceneSave, "�÷��̸��� ����");

            Option.CompileToSave = GUILayout.Toggle(Option.CompileToSave, "��ũ��Ʈ �����Ͻ� ����");

            Option.TimedAutoSave = EditorGUILayout.BeginToggleGroup("�ð� ���ݿ� ���� ����", Option.TimedAutoSave);

            Option.SaveTimeTickMinute = EditorGUILayout.IntSlider("����(��)", Option.SaveTimeTickMinute, 1, 60);

            EditorGUILayout.EndToggleGroup();



            EditorGUILayout.HelpBox("�ɼ������� " + Application.persistentDataPath + "/AutoSaveOption.json�� ���� �˴ϴ�", MessageType.Info, true);



            GUILayout.Label("���� ������ " + EditorSceneManager.loadedSceneCount, EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();

            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);

            SceneSetup[] Setup = EditorSceneManager.GetSceneManagerSetup();

            if (Setup != null)

            {

                for (int i = 0; i < Setup.Length; i++)

                {

                    if (Setup[i].isLoaded)

                    {

                        EditorGUILayout.LabelField(Setup[i].path, EditorStyles.textField);

                    }

                }

            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();



            if (Option.StartAutoSave)

            {

                EditorGUILayout.HelpBox("�ڵ����� ������ �Դϴ�", MessageType.Info, true);

            }

            else

            {

                EditorGUILayout.HelpBox("�ڵ����� �ߴ��� �Դϴ�", MessageType.Warning, true);

            }



            if (GUILayout.Button("�������"))

            {

                SaveScene();

            }



            if (GUILayout.Button("�ڵ����� ����"))

            {

                Option.StartAutoSave = true;

                DateTime lastSaveTime = DateTime.Now;

            }



            if (GUILayout.Button("�ڵ����� ����"))

            {

                Option.StartAutoSave = false; ;

            }

        }

    }

    #endregion

}