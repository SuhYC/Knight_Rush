using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HiddenMission : MonoBehaviour
{
    private string str = "";
    TextMeshProUGUI tmptxt;
    void Awake(){
        if(str == ""){
            tmptxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            str = tmptxt.text;
        }
    }

    void OnEnable(){
        if(str != ""){
            tmptxt.text = "Hidden";
        }
    }
}
