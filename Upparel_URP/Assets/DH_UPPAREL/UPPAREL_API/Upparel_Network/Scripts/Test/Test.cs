using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using static HttpRequestManager;
public class Test : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        UnityWebRequest.ClearCookieCache();
#endif
        Debug.Log(Application.persistentDataPath);
    }
    // Start is called before the first frame update
    void Start()
    {
        /*MemberVO member = new MemberVO();
        member.email = "register@email.com";
        member.name = "kdh";
        member.phone = "010-0000-0000";
        member.rule = "producer";
        member.reward = 1111;

        string json = JsonUtility.ToJson(member);
        HttpRequest req = new HttpRequestBuilder()
            .Uri("register")
            .Type(RequestType.POST)
            .Body(json)
            .Headers(new List<KeyValue<string, string>>() {
                new KeyValue<string, string>("Content-Type", "application/json; charset=utf-8")

             })
            .build();*/

        /*HttpRequest req = new HttpRequestBuilder()
            .Uri("login")
            .ReqHandler((handler) =>
            {

                Dictionary<string, string> ad = handler.GetResponseHeaders();
                print(ad);
                MemberVO vo = JsonUtility.FromJson<MemberVO>(handler.downloadHandler.text);
                Debug.Log(vo.email);
                Debug.Log(vo.name);
            })
            .build();
        WWWForm form = new WWWForm();
        form.AddField("email", "register@email.com");
        form.AddField("password", "password");
        Instance.SendRequest(Post(req, form));*/

        HttpRequest req = new HttpRequestBuilder()
            .Path("member/response")
            .build();
        Instance.SendRequest(Get(req, ""));


        //Instance.SendRequest(req);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
