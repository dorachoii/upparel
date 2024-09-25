using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check4 : MonoBehaviour
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
        Check4Activeness();
        
    }

    public void Check4Activeness()
    {
        if(gameObject.activeSelf)
        {
            contentChecker.contentFinished = false;
        }
    }
}
