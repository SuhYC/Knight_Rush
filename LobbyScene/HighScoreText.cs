using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    void OnEnable(){
        Init();
    }

    private void Init(){
        int score = SceneController.MainManager.GetHighScore();

        switch(SceneController.MainManager.language){
            case 1: // Eng
                GetComponent<TextMeshProUGUI>().text = "High Score: " + score.ToString() + "m";
                return;
            case 2: // Kor
                GetComponent<TextMeshProUGUI>().text = "최고기록: " + score.ToString() + "m";
                return;
        }
    }
}
