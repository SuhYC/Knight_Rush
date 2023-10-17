using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubButton : MonoBehaviour
{
    SlideUpButtonSet slideUpButtonSet;
    Image thisImg;
    Button thisButton;
    TextMeshProUGUI thisText;

    Vector2 buttonPosition;

    void Awake(){
        slideUpButtonSet = transform.parent.parent.gameObject.GetComponent<SlideUpButtonSet>();
        thisImg = gameObject.GetComponent<Image>();
        thisButton = gameObject.GetComponent<Button>();
        thisText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }
    
    void Update(){
        if(slideUpButtonSet.GetState()){
            buttonPosition = new Vector2(100.0f, 200.0f * (transform.GetSiblingIndex() + 1) + 10f - transform.GetSiblingIndex() * 20f);
        }
        else{
            buttonPosition = new Vector2(100.0f, 0f);
        }

        transform.localPosition = Vector2.Lerp(transform.localPosition, buttonPosition, Time.deltaTime * 5f);

        if(transform.localPosition.y < 50.0f){ // 서브메뉴가 본메뉴에 가까워지면 사라지는 효과
            thisImg.enabled = false;
            thisButton.enabled = false;
            thisText.enabled = false;
        }
        else{
            thisImg.enabled = true;
            thisButton.enabled = true;
            thisText.enabled = true;
        }
    }
}
