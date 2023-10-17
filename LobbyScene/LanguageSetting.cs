using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSetting : MonoBehaviour
{
    void OnEnable(){
        GetComponent<TMP_Dropdown>().value = SceneController.MainManager.language - 1;
    }

    public void SetLanguage(){
        SceneController.MainManager.LanguageRenew(GetComponent<TMP_Dropdown>().value + 1);
        
        DataController.DC.GetData();
        DataController.DC.SaveData();
        
        return;
    }
}
