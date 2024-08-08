using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitWantedSec : MonoBehaviour
{
    public UnityEvent OnCalled;
    public float Sec;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void call()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(Sec);

        OnCalled.Invoke(); 
    }

}
