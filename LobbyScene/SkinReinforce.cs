using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinReinforce : MonoBehaviour
{
    public void SkinLevelUpButtonClicked(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        if(skinLevel > SceneController.MAXSKINLEVEL - 1){
            return;
        }

        if(CostOfLevelUp() > SceneController.GetMoneyData()){
            return;
        }

        SceneController.SkinLevelUp(skinNum, CostOfLevelUp());

        MaxLevelCheck();
    }

    public void MaxLevelCheck(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        if(skinLevel == SceneController.MAXSKINLEVEL){
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            transform.GetChild(0).gameObject.SetActive(true);
            InitCostUI();
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void InitCostUI(){
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = CostOfLevelUp().ToString() + " $";

        return;
    }

    private int CostOfLevelUp(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        switch(skinNum){
            case 0:
                return 10 * skinLevel * skinLevel + 250;
            case 1:
                return skinLevel * skinLevel * skinLevel + 250;
            default:
                LogWhenSkinIdxError();
                return -1;
        }
    }

    private void LogWhenSkinIdxError(){
        Debug.Log("SkinReinforce: Skin Idx Error");

        return;
    }
}
