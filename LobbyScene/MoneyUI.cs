using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public static List<MoneyUI> MUI;

    void Awake(){
        if(MUI == null){
            MUI = new List<MoneyUI>();
        }
        MUI.Add(this);
        Init();
    }

    void OnEnable(){
        Init();
    }

    void OnDestroy(){
        MUI.Remove(this);
    }

    public static void InitAllUI(){
        for(int i = 0; i<MUI.Count; i++){
            if(MUI[i] != null){
                MUI[i].Init();
            }
        }
    }
    
    public void Init(){
        int data = SceneController.GetMoneyData();

        TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();

        txt.text = data.ToString() + " $";

        return;
    }

    public void SetImageDark(){
        Image img = transform.parent.GetComponent<Image>();
        img.color = new Color(40f/255f,40f/255f,40f/255f);

        TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();
        Color newColor = txt.color;
        newColor.a = 180f/255f;
        txt.color = newColor;

        return;
    }

    public void SetImageBright(){
        Image img = transform.parent.GetComponent<Image>();
        img.color = new Color(0f,0f,0f);

        TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();
        Color newColor = txt.color;
        newColor.a = 1f;
        txt.color = newColor;

        return;
    }
}
