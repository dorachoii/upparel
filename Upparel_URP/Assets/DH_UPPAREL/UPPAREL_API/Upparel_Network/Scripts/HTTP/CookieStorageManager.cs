using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class CookieStorageManager : MonoBehaviour
{
    static string saveFileName = "cookies.json";
    static string saveDirPath = Path.Combine(Application.persistentDataPath, "Files", "Cookies");
    static string saveFilePath = Path.Combine(saveDirPath, saveFileName);
    public CookieStorage storage = new CookieStorage();

    static CookieStorageManager instance;
    public static CookieStorageManager Get
    {
        get {
            if (instance == null)
            {
                GameObject go = new GameObject("CookieStorageManager");
                CookieStorageManager manager =  go.AddComponent<CookieStorageManager>();
                instance = manager;
                DontDestroyOnLoad(go);
            }
            return instance;
            }
    }

    private void Awake()
    {
        storage = Load();
    }

    public void Save(CookieStorage storage)
    {
        string json = JsonUtility.ToJson(storage);
        FileManager.CreateDirorFile(saveDirPath,saveFileName);
        File.WriteAllText(saveFilePath, json);
    }
    public CookieStorage Load()
    {
        FileManager.CreateDirorFile(saveDirPath, saveFileName);
        string json = File.ReadAllText(saveFilePath);
        Debug.LogWarning(json);
        CookieStorage storage =  JsonUtility.FromJson<CookieStorage>(json);
        if (storage == null)
            return new CookieStorage();
        return storage;
    }

    public void Parse(string header,UnityWebRequest webrequest)
    {
        Cookie cookie = Cookie.Parse(header,webrequest);
        if(cookie != null)
            storage.AddCookie(cookie);

    }

    private void OnApplicationFocus(bool focus)
    {
        Save(storage);
    }
    private void OnApplicationQuit()
    {
        /*HttpRequest req = new HttpRequestBuilder()
        .Uri("cookie")
        .build();
        HttpRequestManager.Instance.SendRequest(HttpRequestManager.Get(req, ""));
        Save(storage);*/
    }
}

[System.Serializable]
public class CookieStorage
{
    public List<Cookie> cookies = new List<Cookie>();

    public void AddCookie(Cookie cookie)
    {
        Debug.Log(cookies);
        // ���� �̸��� ������, ����� ��Ű�� �ִٸ� ������Ʈ
        var existingCookie = cookies.FirstOrDefault(c => c.Key == cookie.Key && c.Domain == cookie.Domain && c.Path == cookie.Path);
        if (existingCookie != null)
        {
            cookies.Remove(existingCookie);
        }
        cookies.Add(cookie);
    }
    // ��Ű �˻�
    public Cookie GetCookie(string key, string domain, string path = "/")
    {
        return cookies.FirstOrDefault(c => c.Key == key && c.Domain == domain && c.Path == path && !c.IsExpired);
    }

    // ��Ű ����
    public void DeleteCookie(string key, string domain, string path = "/")
    {
        var cookie = GetCookie(key, domain, path);
        if (cookie != null)
        {
            cookies.Remove(cookie);
        }
    }

    // �����ΰ� ��ο� �´� ��Ű�� ��� ��������
    public List<Cookie> GetCookies(string domain, string path = "/")
    {
        return cookies.Where(c => c.Domain == domain && path.StartsWith(c.Path)).ToList();
    }
}

