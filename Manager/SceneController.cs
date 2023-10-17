using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController MainManager;
    private string gameMode;
    private GameObject darkPanel;
    private static int money;
    private static int reward = 0;
    private static int highScore = 0;

    public static List<int> SkinLevel;
    public const int MAXSKINLEVEL = 40;
    public const int MAXSKINCOUNT = 2;

    public const int PAWNMAXSKINCOUNT = 2;
    public const int ROOKMAXSKINCOUNT = 3;
    public const int BISHOPMAXSKINCOUNT = 4;
    public const int QUEENMAXSKINCOUNT = 2;
    public static List<bool> PawnSkinOpened;
    public static List<bool> RookSkinOpened;
    public static List<bool> BishopSkinOpened;
    public static List<bool> QueenSkinOpened;

    public float soundEffectVolume;
    public float BGMVolume;

    public int language = 0; // 0: None, 1: English, 2: Korean
    private bool guideEffect = true;

    public struct PlayerAttribute // 게임 씬에 진입할 때 정보를 저장하고 넘길 예정
    {
        public string knightskin;
        public string pawnskin;
        public string bishopskin;
        public string rookskin;
        public string queenskin;
        
        public int attack;
        public int health_point;
    }

    public static PlayerAttribute PAttribute = new PlayerAttribute();

    void Awake(){
        if(MainManager != null && MainManager != this){
            Destroy(gameObject);
            return;
        }

        MainManager = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Init(){
        PAttribute.knightskin = SelectedKnight.sk.GetSkinString(0);
        PAttribute.pawnskin = SelectedKnight.sk.GetSkinString(1);
        PAttribute.bishopskin = SelectedKnight.sk.GetSkinString(2);
        PAttribute.rookskin = SelectedKnight.sk.GetSkinString(3);
        PAttribute.queenskin = SelectedKnight.sk.GetSkinString(4);
        darkPanel = GameObject.Find("Canvas").transform.Find("DarkPanel").gameObject;

        return;
    }

    public void EnterGameScene(string str){
        Init();
        darkPanel.SetActive(true);

        if(PAttribute.knightskin == null)
        {
            LogWhenNullPlayerSkin();
            //return;
        }

        this.gameMode = str;
        SceneManager.LoadScene("GameScene");

        PlayerKnight.Init(str, PAttribute);
        EnemySpawner.Init(PAttribute);
    }

    public string GetGameMode(){
        return gameMode;
    }

    public void StartSceneButtonClicked(){
        if(DataController.DC.LoadData()){
            DataController.DC.SaveData();
            DataController.DC.PasteData();
            EnterLobbyScene();
        }
        else{
            DataController.DC.SaveData();
            DataController.DC.PasteData();
            EnterTutorialScene();
        }
    }
    public void EnterLobbyScene(){
        SceneManager.LoadScene("LobbyScene");
    }

    public void EnterTutorialScene(){
        SceneManager.LoadScene("TutorialScene");
    }

    public void EnterLobbySceneOnGameOver(){
        if(gameMode == "score"){
            int distance = EnemySpawner.GetDistance() - EnemySpawner.NUMBER_OF_INIT_LINE;
            if(distance > highScore){
                SetHighScore(distance);
            }
        }
        money += reward;
        
        DataController.DC.GetData();
        DataController.DC.SaveData();

        SceneManager.LoadScene("LobbyScene");
    }

    public static int GetMoneyData(){
        return money;
    }

    public static void SetMoneyData(int i){
        money = i;

        return;
    }

    public static void DecreaseMoneyData(int i){
        if(money < i){
            LogWhenNoMoney();
        }

        money -= i;

        return;
    }

    public static void SetReward(int data){
        reward = data;

        return;
    }

    public static int GetReward(){
        return reward;
    }

    public static int GetSkinLevel(int skinNum){
        if(skinNum >= MAXSKINCOUNT || skinNum < 0){
            return -1;
        }
        return SkinLevel[skinNum];
    }

    public static void SkinLevelUp(int skinNum, int cost){
        if(SkinLevel[skinNum] == 0 || SkinLevel[skinNum] > MAXSKINLEVEL - 1){
            return;
        }
        money -= cost;
        MoneyUI.InitAllUI();
        SkinLevel[skinNum]++;

        DataController.DC.GetData();
        DataController.DC.SaveData();

        return;
    }

    public static void BuySkin(int skinNum){
        if(SkinLevel[skinNum] != 0){
            return;
        }

        SkinLevel[skinNum] = 1;

        DataController.DC.GetData();
        DataController.DC.SaveData();
        
        return;
    }

    public static void BuyEnemySkin(int enemy, int idx){
        switch(enemy){
            case 0: // Pawn
                if(idx > PAWNMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return;
                }
                PawnSkinOpened[idx] = true;
                DataController.DC.GetData();
                DataController.DC.SaveData();
                return;
            case 1: // Bishop
                if(idx > BISHOPMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return;
                }
                BishopSkinOpened[idx] = true;
                DataController.DC.GetData();
                DataController.DC.SaveData();
                return;
            case 2: // Rook
                if(idx > ROOKMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return;
                }
                RookSkinOpened[idx] = true;
                DataController.DC.GetData();
                DataController.DC.SaveData();
                return;
            case 3: // Queen
                if(idx > QUEENMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return;
                }
                QueenSkinOpened[idx] = true;
                DataController.DC.GetData();
                DataController.DC.SaveData();
                return;
            default:
                return;
        }
    }

    void LogWhenNullPlayerSkin(){
        Debug.Log("SceneController: No Player skin.");
        return;
    }

    public static bool GetEnemySkinOpened(int enemyidx, int idx){
        switch(enemyidx){
            case 0: // Pawn
                if(idx > PAWNMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return false;
                }
                return PawnSkinOpened[idx];
            case 1: // Bishop
                if(idx > BISHOPMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return false;
                }
                return BishopSkinOpened[idx];
            case 2: // Rook
                if(idx > ROOKMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return false;
                }
                return RookSkinOpened[idx];
            case 3: // Queen
                if(idx > QUEENMAXSKINCOUNT - 1 || idx < 0){
                    LogWhenIdxError();
                    return false;
                }
                return QueenSkinOpened[idx];
            default:
                return false;
        }
    }

    public void LanguageRenew(int langCode){
        language = langCode;
        GameObject cvs = GameObject.Find("Canvas");

        cvs.SetActive(false);
        cvs.SetActive(true);

        return;
    }

    public void SetBGMVolume(float f){
        BGMVolume = f;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = f;

        return;
    }

    public void SetSoundEffectVolume(float f){
        soundEffectVolume = f;

        return;
    }

    public void SetHighScore(int i){
        highScore = i;

        return;
    }

    public int GetHighScore(){
        return highScore;
    }

    public void SetGuideEffect(bool b){
        guideEffect = b;

        return;
    }

    public bool GetGuideEffect(){
        return guideEffect;
    }

    private static void LogWhenIdxError(){
        Debug.Log("SceneController: IdxError");

        return;
    }

    private static void LogWhenNoMoney(){
        Debug.Log("SceneController: No Money");

        return;
    }
}
