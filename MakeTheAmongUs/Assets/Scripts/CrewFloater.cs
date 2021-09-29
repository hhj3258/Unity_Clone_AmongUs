using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField] GameObject prefabs;
    [SerializeField] List<Sprite> sprites;

    // 색상 중복 방지용
    bool[] crewStates = new bool[12];
    float timer = 0.5f;     // 생성 주기
    float distance = 11f;   // 중심으로부터 소환될 위치 지정


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance));
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            int randomColor = Random.Range(0, 12);
            SpawnFloatingCrew((EPlayerColor)randomColor, distance);
            timer = 1f;
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if(!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            float angle = Random.Range(0f, 360f) * Mathf.PI / 180;
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            float floatingSpeed = Random.Range(1f, 4f);
            float rotateSpeed = Random.Range(-1f, 1f);

            var crew = Instantiate(prefabs, spawnPos, Quaternion.identity);

            crew.GetComponent<FoatingCrew>().SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], 
                playerColor, direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FoatingCrew>();
        
        if(crew!=null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
