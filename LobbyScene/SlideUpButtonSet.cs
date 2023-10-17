using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUpButtonSet : MonoBehaviour
{
    Transform subButtonSet;
    private bool buttonUp;

    void Awake(){
        subButtonSet = transform.GetChild(0);
        buttonUp = false;
    }

    public void SubButtonToggle(){
        buttonUp = !buttonUp;

        return;
    }

    public void SubButtonDown(){
        buttonUp = false;

        return;
    }

    public bool GetState(){
        return buttonUp;
    }
}
