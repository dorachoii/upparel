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
    //��û�� �ʿ��� ����
    public string address;
    public string uri;
    public RequestType type;
    public List<KeyValue<string, string>> headers = new List<KeyValue<string, string>>();
    public string body;
    public Action<DownloadHandler> successHandler;
    public Action failureHandler;
    public Action lodingHandler; //��û�� �ε����϶��� ���ԵǾ�� �Ѵ�..  �ڷ�ƾ VS �Լ�....��� ��������..? �ϴ� �Լ��� ����
    public Action<UnityWebRequest> reqHandler;
}
//Builder ����(�߸����� ����_)
/// <summary>
/// default paramater : Uri, Type
/// </summary>
public class HttpRequestBuilder
{
    HttpRequest myReq = new HttpRequest();
    public HttpRequestBuilder Uri(string uri)
    {
        myReq.uri = uri;
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

//���� �����ؼ� �׳� ����
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