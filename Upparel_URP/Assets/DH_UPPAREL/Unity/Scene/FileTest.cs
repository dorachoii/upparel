using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileTest : MonoBehaviour
{
    public RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        HttpRequest req = new HttpRequestBuilder()
            .Path("file")
            .ReqHandler((req)=> {
                byte[] bytes = req.downloadHandler.data;
                Texture2D tex = new Texture2D(0, 0);
                tex.LoadImage(bytes);
                img.texture = tex;
            })
            .build();
        WWWForm form = new WWWForm();
        form.AddField("fileName", "e1c8dc23-8e98-4843-8eb8-59e72245c09e.png");
        StartCoroutine(HttpRequestManager.Post(req, form));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
