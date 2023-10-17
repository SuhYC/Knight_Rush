using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private static TextMeshProUGUI tmpro;
    private static int score;
    private static int killscore;

    void Awake(){
        tmpro = GetComponent<TextMeshProUGUI>();
        if(tmpro == null){
            LogWhenNoTextMeshPro();
        }
        score = 0;
        killscore = 0;
        
        if(EnemySpawner.GetGameMode() == "score"){
            tmpro.text = "0m";
        }
        else if(EnemySpawner.GetGameMode() == "adventure"){
            tmpro.text = "0 $";
        }
    }

    public static void Init(){
        if(EnemySpawner.GetGameMode() == "score"){
            score = EnemySpawner.GetDistance() - EnemySpawner.NUMBER_OF_INIT_LINE;
            tmpro.text = score.ToString() + "m";
        }
        else if(EnemySpawner.GetGameMode() == "adventure"){
            tmpro.text = killscore.ToString() + " $";
        }
    }

    public static void PlusKillScore(int i){
        killscore += i;

        return;
    }

    public static int GetKillScore(){
        return killscore;
    }

    private void LogWhenNoTextMeshPro(){
        Debug.Log("ScoreBoard: No TMP");

        return;
    }
}
