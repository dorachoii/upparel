using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static HttpManager; //static을 참조없이 쓰겠다.
public class HttpRequestManager : MonoBehaviour
{

    //여러 객체들이 하나의 HttpManager에 접근한다
    //싱글톤을 사용해야한다
    public string defaultAddress = HttpManager.defaultAddress;
    static HttpRequestManager instance;

    //HttpManager을 호출했을때? 바로 사용할 수 있도록
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
            req.host = defaultAddress;

        string address = Combine("/", req.host, req.path, req.body);
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
                        print("정상적인 요청");
                        print(webRequest.downloadHandler.text);

                    }
                    else
                    {
                        print("에러 발생");
                        print(webRequest.error);
                        print(webRequest.downloadHandler.text);
                    }
                }

                break;
            case RequestType.POST:
                using (webRequest = UnityWebRequest.PostWwwForm(Combine("/", req.host, req.path), ""))
                {
                    SetHeader(webRequest, req);
                    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(req.body);
                    webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    yield return webRequest.SendWebRequest();
                    if (webRequest.error == null)
                    {
                        print("정상적인 요청");
                        print(webRequest.downloadHandler.text);
                        req.successHandler?.Invoke(webRequest.downloadHandler);

                    }
                    else
                    {
                        print("에러 발생");
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

//수정된 요청 방식
//사진요청도 만들어야됨


    //요청 전 실행할 함수
    static void BeforeRequest(UnityWebRequest webRequest,HttpRequest req) {


        string cookie = "";
        //Debug.Log(CookieStorageManager.Get.storage);
        //쿠키를 가져와서 세팅해야 함!!
        List<Cookie> list = CookieStorageManager.Get.storage.GetCookies(webRequest.uri.Host, webRequest.uri.LocalPath);
        list.ForEach((c) => 
        {
            print("반복문");
            //직렬화
            string ck = c.SerializeCookie(c);
            cookie += ck;
        });
        print("직렬화 된 쿠키 : " + cookie);
        //webRequest.SetRequestHeader("Cookie", cookie);

        req.headers.Add(new KeyValue<string, string>("Cookie", cookie));
        SetHeader(webRequest, req);

        req.lodingHandler?.Invoke();
    }
    static void AfterRequest(UnityWebRequest webRequest, HttpRequest req) {
        ExecuteHandler(webRequest, req);
        //응답 헤더에서 쿠키를 가져와야 함!
        string header = webRequest.GetResponseHeader("Set-Cookie");
        CookieStorageManager.Get.Parse(header, webRequest); //자동으로 메모리 등록

        

    }

    public static IEnumerator Get(HttpRequest req,string data,bool useDefaultAddress = true)
    {
        ChangeAddress(req, useDefaultAddress);
        string address = HttpManager.Combine("/", req.host, req.path);
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
        string address = HttpManager.Combine("/", req.host, req.path);
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
        string address = HttpManager.Combine("/", req.host, req.path);
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
        string address = HttpManager.Combine("/", req.host, req.path);
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
            req.host = HttpManager.defaultAddress;
        }
    }
    public static void ExecuteHandler(UnityWebRequest webRequest, HttpRequest req) 
    {
        
        if (webRequest.error == null)
        {
            print("정상적인 요청");
            print(webRequest.downloadHandler.text);
            req.reqHandler?.Invoke(webRequest);
            //req.successHandler?.Invoke(webRequest.downloadHandler);

        }
        else
        {
            print("에러 발생");
            print(webRequest.error);
            print(webRequest.downloadHandler.text);
            print(webRequest.uri);

            print(req.body);
        }
    }

}
