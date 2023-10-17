using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void Restart(){
        SceneController.MainManager.EnterLobbyScene();
        SceneController.MainManager.EnterGameScene("score");
    }
}
