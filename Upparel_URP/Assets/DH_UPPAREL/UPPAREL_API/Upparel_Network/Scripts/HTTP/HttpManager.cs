using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//�޸𸮿� �÷��� �ٸ� ��ü���� ���ϰ� HTTP ��û�� ������ �� �ֵ��� �ϴ� ������ ����� Ŭ����
public class HttpManager
{
    public static string defaultAddress = "http://192.168.0.102:8000";
    public Dictionary<string,string> address = new Dictionary<string, string>();
    public string token;

    //�α��� ���¸� �����ϱ� ���� ��ū�� �����Ѵ�..
    public static void SaveToken()
    {
        
    }
    public static string GetQuaery(List<KeyValue<string, string>> lst)
    {
        string query = "?";
        lst.ForEach((keyvalue) => {
            query += keyvalue.key + "=" + keyvalue.value + "&";
        });
        if (query.EndsWith("&"))
        {
            query = query.Substring(0, query.Length - 1);
        }
        return query;
    }

    public static string Combine(string str,params string[] arr)
    {
        string result = "";
        for(int i = 0; i < arr.Length; i++)
        {
            result += arr[i] + str;
        }
        if (result.EndsWith("/"))
        {
            result = result.Substring(0, result.Length - 1);
        }
        return result;
    }
    public static void SetHeader(UnityWebRequest webReq, HttpRequest req)
    {
        req.headers.ForEach((header) => {
            webReq.SetRequestHeader(header.key, header.value);
        });

    }

    public enum ContentType
    {
        ApplicationJson,
        MultipartFormData
    }
    public static string ParseContent(ContentType type)
    {
        string result = null ;
        switch (type)
        {
            case ContentType.ApplicationJson:
                result = "Application-json";
                break;
            case ContentType.MultipartFormData:
                result = "Multipart/form-data";
                break;
            default:
                Debug.Log("ERROR!");
                break;
        }
        return result;
    }
}
