using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkinIllrustration : SkinIllustration
{
    private Transform purchasePanel;
    public override void init(){
        if(purchasePanel == null){
            purchasePanel = transform.parent.GetChild(1);
        }

        purchasePanel.gameObject.SetActive(false);
        
        int idx = transform.parent.GetSiblingIndex();
        string str;

        str = SelectedKnight.sk.GetSkinString(idx+1);

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

    public void ActOnLevelZero(string str, int idx, int enemyidx){
        newImg = Resources.Load<Sprite>("SkinIllrustration/" + str);

        SelectedKnight.sk.SetPurchaseEnemy(enemyidx, idx);
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
}
