using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarPanel : MonoBehaviour
{
    public static PlayerHPBarPanel instance;

    void OnEnable(){
        if(instance == null){
            instance = this;
        }
        
        return;
    }

    public void Renew(float currentHPRate){
        transform.GetChild(0).GetComponent<Image>().fillAmount = currentHPRate;

        return;
    }

}
