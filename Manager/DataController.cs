using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    public static DataController DC;

    private string GameDataFileName = "KL.json";

    public static GameData GD;

    public void Awake(){
        if(DC == null){
            DC = this;
        }
    }
    void Start(){
        LoadData();
        PasteData();
    }

    private void OnApplicationQuit(){
        GetData();
        SaveData();
    }

    public void GetData(){ // SceneController에서 데이터 가져오기
        GD.money = SceneController.GetMoneyData();
        GD.soundEffectVolume = SceneController.MainManager.soundEffectVolume;
        GD.BGMVolume = SceneController.MainManager.BGMVolume;
        GD.language = SceneController.MainManager.language;
        GD.highScore = SceneController.MainManager.GetHighScore();
        GD.guideEffect = SceneController.MainManager.GetGuideEffect();

        //----- 선택된 스킨 정보 -----

        GD.knightskin = SceneController.PAttribute.knightskin;
        GD.pawnskin = SceneController.PAttribute.pawnskin;
        GD.bishopskin = SceneController.PAttribute.bishopskin;
        GD.rookskin = SceneController.PAttribute.rookskin;
        GD.queenskin = SceneController.PAttribute.queenskin;

        //----- 스킨 레벨 및 해금정보 -----

        GD.SkinLevel = SceneController.SkinLevel;
        GD.PawnSkinOpened = SceneController.PawnSkinOpened;
        GD.RookSkinOpened = SceneController.RookSkinOpened;
        GD.BishopSkinOpened = SceneController.BishopSkinOpened;
        GD.QueenSkinOpened = SceneController.QueenSkinOpened;

        //----- 세이브데이터 버전 -----

        GD.dataVer = 2; // **세이브 데이터 버전이 바뀔때마다 수정할 것**

        return;
    }

    public void PasteData(){ // SceneController에 데이터 덮어쓰기
        SceneController.SetMoneyData(GD.money);
        SceneController.MainManager.soundEffectVolume = GD.soundEffectVolume;
        SceneController.MainManager.BGMVolume = GD.BGMVolume;
        SceneController.MainManager.language = GD.language;
        if(GD.dataVer < 1){ // 이전버전 데이터인경우
            GD.highScore = 0;
        }
        SceneController.MainManager.SetHighScore(GD.highScore);
        if(GD.dataVer < 2){ // 이전버전 데이터인경우
            GD.guideEffect = true;
        }
        SceneController.MainManager.SetGuideEffect(GD.guideEffect);

        //----- 선택된 스킨 정보 -----

        SceneController.PAttribute.knightskin = GD.knightskin;
        SceneController.PAttribute.pawnskin = GD.pawnskin;
        SceneController.PAttribute.bishopskin = GD.bishopskin;
        SceneController.PAttribute.rookskin = GD.rookskin;
        SceneController.PAttribute.queenskin = GD.queenskin;

        SelectedKnight.sk.Init();

        //----- 스킨 레벨 및 해금정보 -----

        SceneController.SkinLevel = GD.SkinLevel;
        SceneController.PawnSkinOpened = GD.PawnSkinOpened;
        if(GD.dataVer < 1){ // 이전버전 데이터인경우
            GD.PawnSkinOpened.Add(false);
        }
        SceneController.RookSkinOpened = GD.RookSkinOpened;
        SceneController.BishopSkinOpened = GD.BishopSkinOpened;
        SceneController.QueenSkinOpened = GD.QueenSkinOpened;

        GD.dataVer = 2;

        return;
    }

    public bool LoadData(){ // 파일화된 데이터 가져오기
        string filePath = Application.persistentDataPath + GameDataFileName;

        if(File.Exists(filePath)){
            string FromJsonData = File.ReadAllText(filePath);
            GD = JsonUtility.FromJson<GameData>(FromJsonData);
            return true;
        }
        else{
            GD = GameData.NewData();
            return false;
        }
    }

    public void SaveData(){ // 데이터 파일화하기
        string ToJsonData = JsonUtility.ToJson(GD);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
    }

}
