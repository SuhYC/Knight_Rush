using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSkinController : MonoBehaviour
{
    public static SelectedSkinController SC;

    void OnEnable(){
        if(SC == null){
            SC = this;
        }

        for(int i = 0; i<5; i++){
            ChangeSkinIllustration(i);
        }
    }

    public void ChangeSkinIllustration(int chess_piece){
        // ----- 각 기물 배치 상황 때문에 기존 스킨 넘버와 하이어라키 순서가 다름. -----

        string str = SelectedKnight.sk.GetSkinString(chess_piece);

        switch(chess_piece){
            case 0: // knight
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("mini robby/" + str);
                break;
            case 1: // pawn
                transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("mini robby/" + str);
                break;
            case 2: // bishop
                transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("mini robby/" + str);
                break;
            case 3: // rook
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("mini robby/" + str);
                break;
            case 4: // queen
                transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("mini robby/" + str);
                break;
        }

        return;
    }
}
