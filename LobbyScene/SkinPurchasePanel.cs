using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinPurchasePanel : MonoBehaviour
{
    private KnightInventory KI;

    public void init(KnightInventory K){
        KI = K;
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = KI.cost.ToString() + " $";
    }

    public void BuyItem(){
        if(transform.parent.parent.childCount == 1){ // 플레이어
            if(KI.cost > SceneController.GetMoneyData()){
                return;
            }
            int idx = KI.transform.GetSiblingIndex();
            SceneController.BuySkin(idx);
            SceneController.DecreaseMoneyData(KI.cost);
            MoneyUI.InitAllUI();
            KI.InitStateOfLock();
            gameObject.SetActive(false);
        }
        else{ // 적
            if(KI.cost > SceneController.GetMoneyData()){
                return;
            }
            int idx = KI.transform.GetSiblingIndex();
            int enemy = KI.transform.parent.GetSiblingIndex();
            
            SceneController.BuyEnemySkin(enemy, idx);
            SceneController.DecreaseMoneyData(KI.cost);
            MoneyUI.InitAllUI();
            KI.InitStateOfLock();
            gameObject.SetActive(false);
        }
    }
}
