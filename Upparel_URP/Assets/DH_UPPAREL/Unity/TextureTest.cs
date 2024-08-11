using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureTest : MonoBehaviour
{
    [SerializeField]
    private Scrollbar slider;

    [SerializeField]
    private int count = 4;
    private float pos = 0;
    private float distance;

    private void Awake()
    {
        distance = 1.0f / count;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float targetPos = pos * distance;

            if (slider.value > targetPos + distance / 2 && pos < count )
            {
                pos += 1;
                Debug.Log("POS: " + pos);
            }
            else if (slider.value < targetPos - distance / 2 && pos > 0)
            {
                pos -= 1;
                Debug.Log("POS: " + pos);
            }
        }

        if (!Input.GetMouseButton(0))
        {
            slider.value = Mathf.Lerp(slider.value, pos * distance, Time.deltaTime * 10);
        }
    }
}
