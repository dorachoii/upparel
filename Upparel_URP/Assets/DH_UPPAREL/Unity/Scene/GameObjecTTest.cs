using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjecTTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("GameObject");
        Image image = go.GetComponent<Image>();
        print(image);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
