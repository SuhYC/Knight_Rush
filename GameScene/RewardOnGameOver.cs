using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardOnGameOver : MonoBehaviour
{
    void OnEnable(){
        Init();
    }

    void Init(){
        TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();

        txt.text = SceneController.GetReward().ToString();
    }
}
