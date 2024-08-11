using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshProUGUI를 사용할 경우 필요

public class Money : MonoBehaviour
{
    public int money = 75000;  // 초기 돈 75,000
    public GameObject cart1;
    public GameObject cart2;
    public TextMeshProUGUI moneyText;  // 돈을 표시할 TextMeshPro UI
    //public Text moneyText;  // 일반 UI Text를 사용할 경우

    void Awake()
    {
       moneyText.text = money.ToString();
    }

    public void DeductMoney()
    {
        money-= 12500;
        moneyText.text = money.ToString();
        
        AudioClip coin = SoundManager.instance.audioClips[0];
        SoundManager.instance.audioSource_SFX.PlayOneShot(coin);

        cart1.GetComponent<Image>().enabled = false;
        cart2.GetComponent<TextMeshProUGUI>().text = "";
    }

}
