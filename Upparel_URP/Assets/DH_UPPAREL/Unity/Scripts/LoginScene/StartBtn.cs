using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

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
        // 버튼이 눌리면 1번 씬으로 이동
        SceneManager.LoadScene(1);
    }
}
