using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLanguageSetting : MonoBehaviour
{
    void Awake(){
        if(SceneController.MainManager.language != 0){
            transform.parent.GetChild(SceneController.MainManager.language - 1).gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    public void SelectKor(){
        SceneController.MainManager.language = 2;
        transform.parent.GetChild(1).gameObject.SetActive(false);

        DataController.DC.GetData();
        DataController.DC.SaveData();
        gameObject.SetActive(false);
    }

    public void SelectEng(){
        SceneController.MainManager.language = 1;
        transform.parent.GetChild(0).gameObject.SetActive(false);
        
        DataController.DC.GetData();
        DataController.DC.SaveData();
        gameObject.SetActive(false);
    }
}
