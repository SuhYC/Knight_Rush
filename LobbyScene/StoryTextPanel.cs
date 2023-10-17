using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTextPanel : MonoBehaviour
{
    public string skin_0_EngText;
    public string skin_0_KorText;
    public string skin_1_EngText;
    public string skin_1_KorText;
    

    public void RenewText(){
        int language = SceneController.MainManager.language; // 0: Eng, 1: Kor
        int skinNum = UpgradeIllustration.currentSkinNum;

        switch(skinNum){
            case 0:
                if(language == 1){ // Eng
                    GetComponent<TextMeshProUGUI>().text = skin_0_EngText.Replace("\\n", "\n");
                }
                else if(language == 2){ // Kor
                    GetComponent<TextMeshProUGUI>().text = skin_0_KorText.Replace("\\n", "\n");
                }
                return;
            case 1:
                if(language == 1){ // Eng
                    GetComponent<TextMeshProUGUI>().text = skin_1_EngText.Replace("\\n", "\n");
                }
                else if(language == 2){ // Kor
                    GetComponent<TextMeshProUGUI>().text = skin_1_KorText.Replace("\\n", "\n");
                }
                return;
        }
    }
}
