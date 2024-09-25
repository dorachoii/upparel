using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check3 : MonoBehaviour
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
        Check3Activeness();
        
    }

    public void Check3Activeness()
    {
        if(gameObject.activeSelf)
        {
            contentChecker.contentFinished = true;
        }
    }
}
