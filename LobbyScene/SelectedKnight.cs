using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedKnight : MonoBehaviour
{
    public static SelectedKnight sk;
    private static Transform purchasePanel;

    private static string knightSkin;
    private static string pawnSkin;
    private static string bishopSkin;
    private static string rookSkin;
    private static string queenSkin;

    private static int purchaseKnight; // 스킨선택창에서 선택된 나이트. 구매할때 참조할 인덱스
    private static int purchaseEnemy; // 적 종류
    private static int purchaseEnemyidx; // 해당 종류에서 인덱스
    private static int cost;

    void Awake(){
        if(sk == null){
            sk = this;
        }
        else if (sk != this){
            Destroy(gameObject);
        }
    }

    public void Init(){
        knightSkin = SceneController.PAttribute.knightskin;
        pawnSkin = SceneController.PAttribute.pawnskin;
        bishopSkin = SceneController.PAttribute.bishopskin;
        rookSkin = SceneController.PAttribute.rookskin;
        queenSkin = SceneController.PAttribute.queenskin;

        return;
    }

    public void SetSkinString(string str, int chess_piece){

        switch(chess_piece){
            case 0:
                knightSkin = str;
                SceneController.PAttribute.knightskin = str;
                break;
            case 1:
                pawnSkin = str;
                SceneController.PAttribute.pawnskin = str;
                break;
            case 2:
                bishopSkin = str;
                SceneController.PAttribute.bishopskin = str;
                break;
            case 3:
                rookSkin = str;
                SceneController.PAttribute.rookskin = str;
                break;
            case 4:
                queenSkin = str;
                SceneController.PAttribute.queenskin = str;
                break;
            default:
                LogWhenInvalidChessPieceNumber();
                break;
        }

        SelectedSkinController.SC.ChangeSkinIllustration(chess_piece);

        return;
    }

    public string GetSkinString(int chess_piece){
        switch(chess_piece){
            case 0:
                return knightSkin;
            case 1:
                return pawnSkin;
            case 2:
                return bishopSkin;
            case 3:
                return rookSkin;
            case 4:
                return queenSkin;
            default:
                LogWhenInvalidChessPieceNumber();
                return null;
        }
    }

    private void LogWhenInvalidChessPieceNumber(){
        Debug.Log("SelectedKnight: Invalid Chess Piece Number");

        return;
    }

    public void SetPurchaseKnight(int i){
        if(purchasePanel == null){
            purchasePanel = SkinIllustration.GetPurchasePanel();
        }
        purchasePanel.gameObject.SetActive(true);
        purchaseKnight = i;
        return;
    }

    public void SetPurchaseEnemy(int enemyidx, int idx){
        purchaseEnemy = enemyidx;
        purchaseEnemyidx = idx;

        return;
    }

    public int GetPurchaseEnemyidx(){
        return purchaseEnemyidx;
    }

    public int GetPurchaseEnemy(){
        return purchaseEnemy;
    }

    public int GetPurchaseKnight(){
        return purchaseKnight;
    }
}
