using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart_DelayOnButton : MonoBehaviour
{
    private static float delay_time;

    void Start()
    {
        delay_time = 0.0f;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if(delay_time < 0.5f)
        {
            delay_time += Time.deltaTime;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
