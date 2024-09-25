using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check1 : MonoBehaviour
{
    [SerializeField]
    private ContentChecker contentChecker;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Check1Activeness();
        
    }

    public void Check1Activeness()
    {
        if(gameObject.activeSelf)
        {
            contentChecker.contentStarted = true;
        }
    }
}
