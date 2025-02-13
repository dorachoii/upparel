using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ServerSet : MonoBehaviour
{
    public InputField field;
    public Button btn;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(()=> {
            set(field.text);
            text.text = "�ٲ����" + HttpRequestManager.Instance.defaultAddress;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set(string host)
    {
        ServerSetting.SetIP(host);
    }
}
