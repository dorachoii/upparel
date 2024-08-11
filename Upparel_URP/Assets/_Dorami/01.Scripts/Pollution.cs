using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject pollution;
    public GameObject flower;
    public GameObject cleaningFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clean(){
         AudioClip clean = SoundManager.instance.audioClips[1];
        SoundManager.instance.audioSource_SFX.PlayOneShot(clean);
        cleaningFX.SetActive(true);
        pollution.SetActive(false);
        flower.SetActive(true);
    }
}
