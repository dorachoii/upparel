using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        CharButton.EnableChar = () => { btn.interactable = true; };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        //회원가입 진행하기
        //CharButton.currentBtn.type
    }
}
