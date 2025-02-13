using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterView : MonoBehaviour
{
    [SerializeField]
    TMP_Text prev_nickText;
    [SerializeField]
    TMP_Text nickText;

    // Start is called before the first frame update
    void Start()
    {
        // 스택 일단 이거 끌게요
        /*
        object objnickname;
        PageManager.Get.data.TryGetValue("nickname", out objnickname);
        string nickname = (string)objnickname;
        */

        nickText.text = prev_nickText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
