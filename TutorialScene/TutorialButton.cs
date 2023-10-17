using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public void ActOnClicked(){
        int idx = transform.GetSiblingIndex();
        if(idx != 0){
            gameObject.SetActive(false);
        }
        else{
            SceneController.MainManager.EnterLobbyScene();
        }
    }
}
