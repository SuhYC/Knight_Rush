using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeIllustration : SkinIllustration
{
    public static int currentSkinNum;

    public override void init(){
        string str = SelectedKnight.sk.GetSkinString(0);

        if(str.Length == 6){
            currentSkinNum = 0;
        }
        else{
            if(!Int32.TryParse(str.Substring(6,1), out currentSkinNum)){
                LogWhenSkinNumberError();
            }
        }

        newImg = Resources.Load<Sprite>("SkinIllrustration/" + str);

        if(newImg != null){
            float sizex = newImg.bounds.size.x;
            float sizey = newImg.bounds.size.y;
            Vector3 scale = transform.localScale;
            scale.x = scale.y * sizex / sizey * 0.7f;
            transform.localScale = scale;

            thisImg.sprite = newImg;
        }
        else{
            LogWhenCantLoadIllustrationSprite();
        }
        transform.parent.parent.parent.parent.GetChild(1).GetChild(3).GetComponent<SkinReinforce>().MaxLevelCheck();
    }

    public void LoadIllustration(string str){
        newImg = Resources.Load<Sprite>("SkinIllrustration/" + str);

        if(newImg != null){
            float sizex = newImg.bounds.size.x;
            float sizey = newImg.bounds.size.y;
            Vector3 scale = transform.localScale;
            scale.x = scale.y * sizex / sizey * 0.7f;
            transform.localScale = scale;

            thisImg.sprite = newImg;
        }
        else{
            LogWhenCantLoadIllustrationSprite();
        }
        transform.parent.parent.parent.parent.GetChild(1).GetChild(3).GetComponent<SkinReinforce>().MaxLevelCheck();
    }

    public void OnLeftButtonClicked(){
        int count = 1;

        if(currentSkinNum == 0){
            return;
        }
        else{
            while(SceneController.GetSkinLevel(currentSkinNum - count) == 0){
                count++;
            }

            currentSkinNum -= count;

            if(currentSkinNum == 0){
                LoadIllustration("Knight");
            }
            else{
                LoadIllustration("Knight" + currentSkinNum.ToString());
            }
        }

        return;
    }

    public void OnRightButtonClicked(){
        int count = 1;
        if(SceneController.GetSkinLevel(currentSkinNum + 1) == -1){ // out of range
            return;
        }
        else{
            while(SceneController.GetSkinLevel(currentSkinNum + count) == 0){
                count++;
            }

            if(SceneController.GetSkinLevel(currentSkinNum + count) == -1){
                return;
            }
            currentSkinNum += count;
            LoadIllustration("Knight" + currentSkinNum.ToString());
        }

        return;
    }

    private void LogWhenSkinNumberError(){
        Debug.Log("UpgradeIllustration: Skin Number Error");
        return;
    }
}
