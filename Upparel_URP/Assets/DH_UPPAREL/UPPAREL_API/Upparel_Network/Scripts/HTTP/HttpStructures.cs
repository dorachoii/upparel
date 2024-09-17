using System;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum RequestType
{
    GET, POST, UPDATE, DELETE
}
public enum ContentType
{
    Default, Multipart_form
}
public class HttpRequest
{
    public static string defaultAddress;
    //요청에 필요한 구조
    public string host;
    public string path;
    public RequestType type;
    public List<KeyValue<string, string>> headers = new List<KeyValue<string, string>>();
    public string body;
    public Action<DownloadHandler> successHandler;
    public Action failureHandler;
    public Action lodingHandler; //요청이 로딩중일때도 포함되어야 한다..  코루틴 VS 함수....어떤게 나을려나..? 일단 함수로 결정
    public Action<UnityWebRequest> reqHandler;
}
//Builder 패턴(잘못만듬 ㅋㅋ_)
/// <summary>
/// default paramater : Uri, Type
/// </summary>
public class HttpRequestBuilder
{
    HttpRequest myReq = new HttpRequest();
    public HttpRequestBuilder Path(string uri)
    {
        myReq.path = uri;
        return this;
    }
    public HttpRequestBuilder Type(RequestType type)
    {
        myReq.type = type;
        return this;
    }
    public HttpRequestBuilder Headers(List<KeyValue<string, string>> headers)
    {
        myReq.headers = headers;
        return this;
    }
    public HttpRequestBuilder Body(string body)
    {
        myReq.body = body;
        return this;
    }
    public HttpRequestBuilder SuccessHandler(Action<DownloadHandler> successHandler)
    {
        myReq.successHandler = successHandler;
        return this;
    }
    public HttpRequestBuilder FailureHandler(Action failureHandler)
    {
        myReq.failureHandler = failureHandler;
        return this;
    }
    public HttpRequestBuilder LodingHandler(Action lodingHandler)
    {
        myReq.lodingHandler = lodingHandler;
        return this;
    }
    public HttpRequestBuilder ReqHandler(Action<UnityWebRequest> reqHandler)
    {
        myReq.reqHandler = reqHandler;
        return this;
    }
    public HttpRequest build()
    {
        return myReq;
    }
}

//쓰기 불편해서 그냥 만듬
public struct KeyValue<KEY, VALUE>
{
    public KEY key;
    public VALUE value;
    public KeyValue(KEY key, VALUE value)
    {
        this.key = key;
        this.value = value;
    }
}