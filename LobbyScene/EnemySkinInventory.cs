using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkinInventory : KnightInventory
{
    private bool opened;

    public override void InitStateOfLock(){
        int enemyidx = transform.parent.GetSiblingIndex();
        int idx = transform.GetSiblingIndex();

        opened = SceneController.GetEnemySkinOpened(enemyidx,idx);
        if(opened == false){
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else{
            transform.GetChild(1).gameObject.SetActive(false);
        }

        InitStateOfSelected();
    }

    public override void ActOnSeleted(){
        if(skinName == null){
            LogWhenNoSkinOnInventory();
            return;
        }
        
        int idx = transform.parent.GetSiblingIndex();
        int count = transform.parent.parent.childCount;
        
        if(opened){ // 해당 스킨의 인벤토리가 해금되어있는지 체크
            SelectedKnight.sk.SetSkinString(skinName, idx+1); // 적

            DataController.DC.GetData();
            DataController.DC.SaveData();

            for(int i = 0; i<transform.parent.childCount; i++){ // 선택된 스킨의 체크표시 갱신
                transform.parent.GetChild(i).GetComponent<EnemySkinInventory>().InitStateOfSelected();
            }

            Transform illustrationTransform = transform.parent.parent.parent.parent.GetChild(0).GetChild(idx).GetChild(0);
            illustrationTransform.GetComponent<EnemySkinIllrustration>().init(); // 일러스트 설정
        }
        else{ // 해금되어있지 않은 스킨은 일러스트는 보여주되 인게임 스킨에 반영이 안되고 구매창이 떠야함
            Transform illustrationTransform = transform.parent.parent.parent.parent.GetChild(0).GetChild(idx).GetChild(0);
            illustrationTransform.GetComponent<EnemySkinIllrustration>().ActOnLevelZero(skinName, transform.GetSiblingIndex(), transform.parent.GetSiblingIndex());
            illustrationTransform.parent.GetChild(1).GetComponent<SkinPurchasePanel>().init(this);
        }
    }
}
