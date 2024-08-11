using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    //static memory�� �ø���
    public static string session_id;
    private void Awake()
    {
        session_id = Load();
        DontDestroyOnLoad(this.gameObject);
    }
    //���� ID ���������� ����
    public static void Save(string value)
    {
        PlayerPrefs.SetString("session_id", value);
        session_id = value; //������Ʈ
    }
    //���� ID �ҷ�����
    public static string Load()
    {
        return PlayerPrefs.GetString("session_id");
    }
}
