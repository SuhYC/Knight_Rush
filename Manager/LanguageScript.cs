using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageScript : MonoBehaviour
{
    public string KorText;
    public string EngText;

    void OnEnable(){
        switch(SceneController.MainManager.language){
            case 1: // Eng
                GetComponent<TextMeshProUGUI>().text = EngText;
                return;
            case 2: // Kor
                GetComponent<TextMeshProUGUI>().text = KorText;
                return;
        }

        return;
    }
}
