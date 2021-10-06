using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlType
{
    Mouse,
    KeyboardMouse
}

// 플레이어 조작 방법 static 선언
public class PlayerSettings
{
    public static EControlType controlType;
    public static string nickname;
}
