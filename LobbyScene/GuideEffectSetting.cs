using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideEffectSetting : MonoBehaviour
{
    void OnEnable(){
        GetComponent<Toggle>().isOn = SceneController.MainManager.GetGuideEffect();
    }

    public void Setting(){
        SceneController.MainManager.SetGuideEffect(GetComponent<Toggle>().isOn);

        DataController.DC.GetData();
        DataController.DC.SaveData();
        
        return;
    }
}
