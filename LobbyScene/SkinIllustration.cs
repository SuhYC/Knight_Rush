using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinIllustration : MonoBehaviour
{
    protected Sprite newImg;
    protected Image thisImg;
    private static Transform purchasePanel;

    protected void Awake(){
        thisImg = GetComponent<Image>();
        if(purchasePanel == null && transform.parent.childCount == 2){
            purchasePanel = transform.parent.GetChild(1);
        }
    }

    protected void OnEnable(){
        init();
    }

    public virtual void init(){
        if(purchasePanel == null){
            LogWhenCantFindPurchasePanel();
        }
        else{
            purchasePanel.gameObject.SetActive(false);
        }
        int idx = transform.parent.GetSiblingIndex();
        string str;
        if(transform.parent.parent.childCount == 1){
            str = SelectedKnight.sk.GetSkinString(idx); // 플레이어
        }
        else{
            str = SelectedKnight.sk.GetSkinString(idx+1); // 적
        }

        newImg = Resources.Load<Sprite>("SkinIllrustration/" + str);

        if(newImg != null){
            float sizex = newImg.bounds.size.x;
            float sizey = newImg.bounds.size.y;
            Vector3 scale = transform.localScale;
            scale.x = scale.y * sizex / sizey * 1f;
            transform.localScale = scale;

            thisImg.sprite = newImg;
        }
        else{
            LogWhenCantLoadIllustrationSprite();
        }
    }

    public virtual void ActOnLevelZero(string str, int i){
        newImg = Resources.Load<Sprite>("SkinIllrustration/" + str);

        if(transform.parent.parent.childCount == 1){
            SelectedKnight.sk.SetPurchaseKnight(i);
        }
        transform.parent.GetChild(1).gameObject.SetActive(true);

        if(newImg != null){
            float sizex = newImg.bounds.size.x;
            float sizey = newImg.bounds.size.y;
            Vector3 scale = transform.localScale;
            scale.x = scale.y * sizex / sizey * 1f;
            transform.localScale = scale;

            thisImg.sprite = newImg;
        }
        else{
            LogWhenCantLoadIllustrationSprite();
        }


    }

    public static Transform GetPurchasePanel(){
        if(purchasePanel == null){
            LogWhenCantFindPurchasePanel();
        }
        return purchasePanel;
    }

    protected void LogWhenCantLoadIllustrationSprite(){
        Debug.Log("SkinIllustration: Cant Load Illustration");

        return;
    }

    private static void LogWhenCantFindPurchasePanel(){
        Debug.Log("SkinIllustration: Cant Find Purchase Panel");

        return;
    }
}
