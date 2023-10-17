using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinUpgradeTitlePanel : MonoBehaviour
{
    public void RenewLevelText(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        int skinLevel = SceneController.GetSkinLevel(skinNum);
        int language = SceneController.MainManager.language;

        TextMeshProUGUI txt = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // NameText
        switch(skinNum){
            case 0:
                if(language == 1){ // Eng
                    txt.text = "Page Hote";
                }
                else if(language == 2){ // Kor
                    txt.text = "견습기사 호테";
                }
                break;
            case 1:
                if(language == 1){ // Eng
                    txt.text = "Chegal Kobi";
                }
                else if(language == 2){ // Kor
                    txt.text = "제갈 고비";
                }
                break;
        }

        txt = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); // Lv Text
        txt.text = skinLevel.ToString() + " / " + SceneController.MAXSKINLEVEL.ToString();

        return;
    }
}
