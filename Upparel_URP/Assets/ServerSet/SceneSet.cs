using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSet : MonoBehaviour
{
    public InputField field;
    public Button btn;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(()=> {
            set();
            text.text = "¾Àº¯°æ";
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set()
    {
        SceneManager.LoadScene(field.text);
    }
}
