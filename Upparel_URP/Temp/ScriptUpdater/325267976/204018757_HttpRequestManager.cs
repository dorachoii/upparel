using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static HttpManager; //static�� �������� ���ڴ�.
public class HttpRequestManager : MonoBehaviour
{

    //���� ��ü���� �ϳ��� HttpManager�� �����Ѵ�
    //�̱����� ����ؾ��Ѵ�
    public string defaultAddress = HttpManager.defaultAddress;
    static HttpRequestManager instance;

    //HttpManager�� ȣ��������? �ٷ� ����� �� �ֵ���
    public static HttpRequestManager Instance
    {
        get {
            if (instance == null)
            {
                GameObject managerObj = new GameObject("HttpManager");
                instance = managerObj.AddComponent<HttpRequestManager>();
                DontDestroyOnLoad(managerObj);
            }

            return instance;

        }
    }
    public Coroutine SendRequest(HttpRequest req)
    {
        return StartCoroutine(SendRequestCor(req));
    }
    public Coroutine SendRequest(IEnumerator ie)
    {
        return StartCoroutine(ie);
    }
     IEnumerator SendRequestCor(HttpRequest req, bool useDefaultAddress = true)
    {
        if (useDefaultAddress)
            req.address = defaultAddress;

        string address = Combine("/", req.address, req.uri, req.body);
        Debug.Log(address);
        UnityWebRequest webRequest = null;
        switch (req.type)
        {
            case RequestType.GET:
                using (webRequest = UnityWebRequest.Get(address))
                {
                    SetHeader(webRequest, req);


                    yield return webRequest.SendWebRequest();
                    if (webRequest.error == null)
                    {
                        print("�������� ��û");
                        print(webRequest.downloadHandler.text);

                    }
                    else
                    {
                        print("���� �߻�");
                        print(webRequest.error);
                        print(webRequest.downloadHandler.text);
                    }
                }

                break;
            case RequestType.POST:
                using (webRequest = UnityWebRequest.PostWwwForm(Combine("/", req.address, req.uri), ""))
                {
                    SetHeader(webRequest, req);
                    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(req.body);
                    webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    yield return webRequest.SendWebRequest();
                    if (webRequest.error == null)
                    {
                        print("�������� ��û");
                        print(webRequest.downloadHandler.text);
                        req.successHandler?.Invoke(webRequest.downloadHandler);

                    }
                    else
                    {
                        print("���� �߻�");
                        print(webRequest.error);
                        print(webRequest.downloadHandler.text);
                    }

                }
                break;
            case RequestType.UPDATE:



                break;
            case RequestType.DELETE:



                break;
            default:
                break;

        }

    }

/*    string address = HttpManager.Combine("/", req.address, req.uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(address, postdata))
{
    SetHeader(webRequest, req);


    ExecuteHandler(webRequest, req);
}*/

//������ ��û ���
//������û�� �����ߵ�


    //��û �� ������ �Լ�
    static void BeforeRequest(UnityWebRequest webRequest,HttpRequest req) {


        string cookie = "";
        //Debug.Log(CookieStorageManager.Get.storage);
        //��Ű�� �����ͼ� �����ؾ� ��!!
        List<Cookie> list = CookieStorageManager.Get.storage.GetCookies(webRequest.uri.Host, webRequest.uri.LocalPath);
        list.ForEach((c) => 
        {
            print("�ݺ���");
            //����ȭ
            string ck = c.SerializeCookie(c);
            cookie += ck;
        });
        print("����ȭ �� ��Ű : " + cookie);
        //webRequest.SetRequestHeader("Cookie", cookie);

        req.headers.Add(new KeyValue<string, string>("Cookie", cookie));
        SetHeader(webRequest, req);
    }
    static void AfterRequest(UnityWebRequest webRequest, HttpRequest req) {
        ExecuteHandler(webRequest, req);
        //���� ������� ��Ű�� �����;� ��!
        string header = webRequest.GetResponseHeader("Set-Cookie");
        CookieStorageManager.Get.Parse(header, webRequest); //�ڵ����� �޸� ���

        

    }

    public static IEnumerator Get(HttpRequest req,string data,bool useDefaultAddress = true)
    {
        ChangeAddress(req, useDefaultAddress);
        string address = HttpManager.Combine("/", req.address, req.uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(address + data))
        {
            
            BeforeRequest(webRequest, req);
            yield return webRequest.SendWebRequest();
            AfterRequest(webRequest, req);
        }
    }
    public static IEnumerator Post(HttpRequest req, string postdata, bool useDefaultAddress = true)
    {
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postdata);

        ChangeAddress(req, useDefaultAddress);
        string address = HttpManager.Combine("/", req.address, req.uri);
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(address,postdata))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            BeforeRequest(webRequest, req);
            yield return webRequest.SendWebRequest();
            AfterRequest(webRequest, req);
        }
    }
    public static IEnumerator Post(HttpRequest req, WWWForm form, bool useDefaultAddress = true)
    {
        ChangeAddress(req, useDefaultAddress);
        string address = HttpManager.Combine("/", req.address, req.uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(address, form))
        {
            BeforeRequest(webRequest, req);
            yield return webRequest.SendWebRequest();
            AfterRequest(webRequest, req);
        }
    }
    public static IEnumerator Post(HttpRequest req, List<IMultipartFormSection> multipartFormSections,bool useDefaultAddress = true)
    {
        ChangeAddress(req, useDefaultAddress);
        string address = HttpManager.Combine("/", req.address, req.uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(address, multipartFormSections))
        {
            BeforeRequest(webRequest, req);
            yield return webRequest.SendWebRequest();
            AfterRequest(webRequest, req);
        }
    }

    
    public static void ChangeAddress(HttpRequest req,bool option)
    {
        if (option)
        {
            req.address = HttpManager.defaultAddress;
        }
    }
    public static void ExecuteHandler(UnityWebRequest webRequest, HttpRequest req) 
    {
        
        if (webRequest.error == null)
        {
            print("�������� ��û");
            print(webRequest.downloadHandler.text);
            req.reqHandler?.Invoke(webRequest);
            //req.successHandler?.Invoke(webRequest.downloadHandler);

        }
        else
        {
            print("���� �߻�");
            print(webRequest.error);
            print(webRequest.downloadHandler.text);
            print(webRequest.uri);

            print(req.body);
        }
    }

}

