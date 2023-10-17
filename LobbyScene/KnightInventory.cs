using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightInventory : MonoBehaviour
{
    public string skinName;
    private int lv;
    public int cost;

    public virtual void InitStateOfLock(){
        int idx = transform.GetSiblingIndex();

        lv = SceneController.GetSkinLevel(idx);
        if(lv == 0){
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else{
            transform.GetChild(1).gameObject.SetActive(false);
        }

        InitStateOfSelected();
        return;
    }

    protected void OnEnable(){
        InitStateOfLock();
    }

    protected void InitStateOfSelected(){
        string str;
        if(transform.parent.parent.childCount == 1){ // 플레이어
            str = SelectedKnight.sk.GetSkinString(0);
        }
        else{
            str = SelectedKnight.sk.GetSkinString(transform.parent.GetSiblingIndex()+1);
        }
        
        if(str == skinName){
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else{
            transform.GetChild(2).gameObject.SetActive(false);
        }

        return;
    }

    public virtual void ActOnSeleted(){
        if(skinName == null){
            LogWhenNoSkinOnInventory();
            return;
        }

        int idx = transform.parent.GetSiblingIndex();
        int count = transform.parent.parent.childCount;
        
        if(lv != 0){
            if(count == 1){
                SelectedKnight.sk.SetSkinString(skinName, idx); // 플레이어
            }
            else{
                SelectedKnight.sk.SetSkinString(skinName, idx+1); // 적
            } // 현재 선택된 탭의 숫자를 넘김(기물의 종류)
            
            for(int i = 0; i<transform.parent.childCount; i++){
                transform.parent.GetChild(i).GetComponent<KnightInventory>().InitStateOfSelected();
            }
            
            DataController.DC.GetData();
            DataController.DC.SaveData();

            Transform illustrationTransform = transform.parent.parent.parent.parent.GetChild(0).GetChild(idx).GetChild(0);
            illustrationTransform.GetComponent<SkinIllustration>().init(); // 일러스트 설정
        }
        else{
            Transform illustrationTransform = transform.parent.parent.parent.parent.GetChild(0).GetChild(idx).GetChild(0);
            illustrationTransform.GetComponent<SkinIllustration>().ActOnLevelZero(skinName, transform.GetSiblingIndex());
            illustrationTransform.parent.GetChild(1).GetComponent<SkinPurchasePanel>().init(this);
        }
    }

    protected void LogWhenNoSkinOnInventory(){
        Debug.Log("KnightInventory: No Skin On Inventory");

        return;
    }
}
