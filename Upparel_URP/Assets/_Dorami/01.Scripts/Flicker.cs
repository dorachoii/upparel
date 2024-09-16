using System.Collections;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject target;
    public float flickerInterval = 0.5f; 

    void Start()
    {
        StartCoroutine(IFlicker());
    }

    IEnumerator IFlicker()
    {
        while (true)
        {
            target.SetActive(!target.activeSelf);   // 상태를 반전!
            yield return new WaitForSeconds(flickerInterval);
        }
    }
}
