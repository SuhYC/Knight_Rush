using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityText : MonoBehaviour
{
    public static AbilityText AT;

    public void init(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        SetAttPoint(skinNum);
        SetHealthPoint(skinNum);
        SetCritDam(skinNum);
        SetCritRate(skinNum);
    }

    public void renew(){
        int skinNum = UpgradeIllustration.currentSkinNum;
        SetAttPoint(skinNum);
        SetHealthPoint(skinNum);
        SetCritDam(skinNum);
        SetCritRate(skinNum);
    }

    private void SetAttPoint(int skinNum){
        int attpoint = CalAttPoint(skinNum);
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = attpoint.ToString();
    }

    public static int CalAttPoint(int skinNum){
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        int attpoint = 0;
        switch(skinNum){
            case 0:
                attpoint = skinLevel * skinLevel * 5 + 20;
                break;
            case 1:
                attpoint = skinLevel * skinLevel * 6 + 30;
                break;
            default:
                LogWhenFunctionNotDefined();
                return -1;
        }

        return attpoint;
    }

    private void SetHealthPoint(int skinNum){
        int healthpoint = CalHealthPoint(skinNum);
        transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthpoint.ToString();
    }

    public static int CalHealthPoint(int skinNum){
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        int healthpoint = 0;
        switch(skinNum){
            case 0:
                healthpoint = skinLevel * 60 + 100;
                break;
            case 1:
                healthpoint = skinLevel * 120 + 150;
                break;
            default:
                LogWhenFunctionNotDefined();
                return -1;
        }

        return healthpoint;
    }

    private void SetCritRate(int skinNum){
        int critrate = CalCritRate(skinNum);
        transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = critrate.ToString() + "%";
    }

    public static int CalCritRate(int skinNum){
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        int critrate = 0;
        switch(skinNum){
            case 0:
                critrate = 10;
                break;
            case 1:
                critrate = 20;
                break;
            default:
                LogWhenFunctionNotDefined();
                return -1;
        }

        return critrate;
    }

    private void SetCritDam(int skinNum){
        float critdam = CalCritDam(skinNum);
        transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x " + critdam.ToString();
    }

    public static float CalCritDam(int skinNum){
        int skinLevel = SceneController.GetSkinLevel(skinNum);

        float critdam = 0;
        switch(skinNum){
            case 0:
                critdam = 1.5f;
                break;
            case 1:
                critdam = 1.3f;
                break;
            default:
                LogWhenFunctionNotDefined();
                return -1;
        }

        return critdam;
    }

    private static void LogWhenFunctionNotDefined(){
        Debug.Log("AbilityText: Function Not Defined");
        return;
    }
}
