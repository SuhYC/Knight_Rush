using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int money;
    public float soundEffectVolume;
    public float BGMVolume;
    public int language;
    public int highScore;

    //------ 선택해둔 스킨정보 -----

    public string knightskin;
    public string pawnskin;
    public string bishopskin;
    public string rookskin;
    public string queenskin;

    //----- 스킨 레벨 및 해금정보 -----

    public List<int> SkinLevel;
    public List<bool> PawnSkinOpened;
    public List<bool> RookSkinOpened;
    public List<bool> BishopSkinOpened;
    public List<bool> QueenSkinOpened;

    //----- 세이브데이터 버전 -----
    public int dataVer;

    //----- 이 지점 이전으로 데이터버전 1 -----

    public bool guideEffect;

    //----- 이 지점 이전으로 데이터버전 2 -----

    public static GameData NewData(){ // GameData타입 생성 및 초기화 후 반환
        GameData data = new GameData();

        data.money = 0;
        data.soundEffectVolume = 0.5f;
        data.BGMVolume = 0.5f;
        data.language = 0; // None
        data.highScore = 0;

        data.knightskin = "Knight";
        data.pawnskin = "Pawn";
        data.bishopskin = "Bishop";
        data.rookskin = "Rook";
        data.queenskin = "Queen";

        data.SkinLevel = new List<int>();
        data.PawnSkinOpened = new List<bool>();
        data.RookSkinOpened = new List<bool>();
        data.BishopSkinOpened = new List<bool>();
        data.QueenSkinOpened = new List<bool>();

        data.SkinLevel.Add(1); // 기본스킨 하나는 열어두기
        for(int i = 1; i < SceneController.MAXSKINCOUNT; i++){
            data.SkinLevel.Add(0);
        }

        data.PawnSkinOpened = new List<bool>();
        data.PawnSkinOpened.Add(true); // 기본스킨 하나는 열어두기
        for(int i = 1; i < SceneController.PAWNMAXSKINCOUNT; i++){
            data.PawnSkinOpened.Add(false);
        }

        data.RookSkinOpened = new List<bool>();
        data.RookSkinOpened.Add(true); // 기본스킨 하나는 열어두기
        for(int i = 1; i < SceneController.ROOKMAXSKINCOUNT; i++){
            data.RookSkinOpened.Add(false);
        }

        data.BishopSkinOpened = new List<bool>();
        data.BishopSkinOpened.Add(true); // 기본스킨 하나는 열어두기
        for(int i = 1; i < SceneController.BISHOPMAXSKINCOUNT; i++){
            data.BishopSkinOpened.Add(false);
        }

        data.QueenSkinOpened = new List<bool>();
        data.QueenSkinOpened.Add(true); // 기본스킨 하나는 열어두기
        for(int i = 1; i < SceneController.QUEENMAXSKINCOUNT; i++){
            data.QueenSkinOpened.Add(false);
        }

        data.dataVer = 2; // **세이브 데이터 버전이 바뀔때마다 수정할 것**

        data.guideEffect = true;

        return data;
    }
}
