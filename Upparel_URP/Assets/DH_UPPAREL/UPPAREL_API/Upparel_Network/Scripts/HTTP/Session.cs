using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    //static memory에 올리기
    public static string session_id;
    private void Awake()
    {
        session_id = Load();
        DontDestroyOnLoad(this.gameObject);
    }
    //세션 ID 영구적으로 저장
    public static void Save(string value)
    {
        PlayerPrefs.SetString("session_id", value);
        session_id = value; //업데이트
    }
    //세션 ID 불러오기
    public static string Load()
    {
        return PlayerPrefs.GetString("session_id");
    }
}
