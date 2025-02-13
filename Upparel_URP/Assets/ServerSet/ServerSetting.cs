using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ServerSetting : MonoBehaviour
{
    static string sceneName = "ServerSet";
    static ServerSetting instance;
    public ServerSetting Instance
    {
        get
        {
            if(instance != null) { return instance; }
            GameObject go = new GameObject("ServerSetting");
            ServerSetting serversetting = go.AddComponent<ServerSetting>();
            instance = serversetting;
            return instance;
        }
    }
    //������ �����ϱ�.(�ڵ��)
    public static void SetIP(string ip)
    {
        HttpRequestManager.Instance.defaultAddress = ip;
        PlayerPrefs.SetString("ip", ip);
    }
    //���� ����Ǿ��ִ� ������ �ҷ��´�
    public static string LoadIP()
    {
        string ip = PlayerPrefs.GetString("ip");
        return ip;
    }
    //���� �Է��ؼ� ������ �����ϴ� ������ �̵��Ѵ�.
    public static void LoadSetScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    //�ʱ����
    public void Init()
    {
        HttpRequestManager.Instance.defaultAddress = LoadIP();
    }
    private void Awake()
    {
        Init();
    }

}
