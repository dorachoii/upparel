using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterView : MonoBehaviour
{
    [SerializeField]
    TMP_Text nickText;
    // Start is called before the first frame update
    void Start()
    {
        object objnickname;
        PageManager.Get.data.TryGetValue("nickname",out objnickname);
        string nickname = (string)objnickname;
        nickText.text = nickname + "님을 나타내는\n캐릭터를 골라주세요";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
