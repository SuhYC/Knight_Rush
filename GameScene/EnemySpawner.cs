using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner ES;

    public const int NUMBER_OF_INIT_LINE = 10;

    public static List<Enemy> EnemyList = new List<Enemy>();
    public static List<GameItem> ItemList = new List<GameItem>();
    public static List<EnemyLine> EnemyLineList = new List<EnemyLine>();
    private static int bishopCount;
    private static int rookCount;
    private static int queenCount;
    private static string gameMode;
    private static int level;
    private static int distance;

    private static GameObject Pawn_pre_obj;
    private static GameObject Knight_pre_obj;
    private static GameObject Bishop_pre_obj;
    private static GameObject Rook_pre_obj;
    private static GameObject Queen_pre_obj;

    private static GameObject Bomb_pre_obj;
    private static GameObject Cross_pre_obj;

    private static GameObject LineWhite_pre_obj;
    private static GameObject LineBlack_pre_obj;
    private static Transform stage;

    private static GameObject gameOverPanel;
    
    private AudioSource AS;
    private AudioClip attackSound;
    private AudioClip attackedSound;

    void Awake(){
        if(ES == null){
            ES = this;
        }
        else if (ES != this){
            Destroy(gameObject);
        }

        LineWhite_pre_obj = Resources.Load("Prefabs/LineWhite") as GameObject;
        LineBlack_pre_obj = Resources.Load("Prefabs/LineBlack") as GameObject;
        Bomb_pre_obj = Resources.Load("Prefabs/Bomb") as GameObject;
        Cross_pre_obj = Resources.Load("Prefabs/Cross") as GameObject;
        stage = GameObject.Find("Stage").transform;

        gameOverPanel = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;

        AS = GetComponent<AudioSource>();
        attackSound = Resources.Load("SoundEffect/Attack") as AudioClip;
        attackedSound = Resources.Load("SoundEffect/Attacked") as AudioClip;

        SoundEffectSetting(SceneController.MainManager.soundEffectVolume);
    }

    void Start(){
        GameObject obj = MonoBehaviour.Instantiate(Knight_pre_obj);
        obj.transform.parent = stage;
        PlayerKnight prevKnight = PlayerKnight.player;
        PlayerKnight.player = obj.AddComponent<PlayerKnight>();
        //Destroy(prevKnight.gameObject);

        for(int i = 0; i < NUMBER_OF_INIT_LINE; i++){
            NewLine();
        }
    }

    public static void Init(SceneController.PlayerAttribute PA){
        Knight_pre_obj = Resources.Load("Prefabs/" + PA.knightskin) as GameObject;
        Pawn_pre_obj = Resources.Load("Prefabs/" + PA.pawnskin) as GameObject;
        Bishop_pre_obj = Resources.Load("Prefabs/" + PA.bishopskin) as GameObject;
        Rook_pre_obj = Resources.Load("Prefabs/" + PA.rookskin) as GameObject;
        Queen_pre_obj = Resources.Load("Prefabs/" + PA.queenskin) as GameObject;

        if(EnemyList.Count != 0){
            for(int i = EnemyList.Count - 1; i >= 0; i--){
                Enemy e = EnemyList[i];

                Destroy(e);
            }
            EnemyList.Clear();
        }

        if(ItemList.Count != 0){
            for(int i = ItemList.Count - 1; i >= 0; i--){
                GameItem I = ItemList[i];

                Destroy(I);
            }
            ItemList.Clear();
        }

        bishopCount = 50;
        rookCount = 0;
        queenCount = 0;
        level = 1;
        distance = 0;

        return;
    }

    public static Enemy ReturnEnemyOnPlayer(){
        Vector2 vec = PlayerKnight.GetPosition();
        Vector2 vec2;
        for(int i = 0; i<EnemyList.Count; i++){
            vec2 = EnemyList[i].GetPosition();
            if(vec.x == vec2.x && vec.y == vec2.y){
                return EnemyList[i];
            }
        }

        return null;
    }

    public static GameItem ReturnItemOnPlayer(){
        Vector2 vec = PlayerKnight.GetPosition();
        Vector2 vec2;
        for(int i = 0; i<ItemList.Count; i++){
            vec2 = ItemList[i].GetPosition();
            if(vec.x == vec2.x && vec.y == vec2.y){
                return ItemList[i];
            }
        }

        return null;
    }
    
    public static List<Enemy> CalCulateEnemyAttack(){
        List<Enemy> ans = new List<Enemy>();

        for(int i = 0; i<EnemyList.Count; i++){
            if(EnemyList[i].CanAttack()){
                ans.Add(EnemyList[i]);
            }
        }

        return ans;
    }

    public static void EnemyMove(){// 호출될 때마다 턴이 흘러가니 해당 메소드에서 카운트를 증가.
        if(EnemyList.Count == 0){
            LogWhenNoEnemy();
            return;
        }
        while(distance < PlayerKnight.GetPosition().y + 10){
            NewLine();
        }
        ScoreBoard.Init();

        return;
    }

    public static void ShowAttack(List<Enemy> Enemy){
        for(int i = 0; i<Enemy.Count; i++){
            Enemy[i].SetAttack();
            Vector2 playerPosition = PlayerKnight.GetPosition();
            Enemy[i].EnemyMove((int)playerPosition.x,(int)playerPosition.y);
        }

        return;
    }

    public static void NewLine(){
        for(int i = 0; i < EnemyLineList.Count; i++){
            EnemyLineList[i].MoveLine();
        }

        GameObject obj;

        if(distance % 2 == 1){
            if(LineBlack_pre_obj == null){
                LogWhenNoModeling();
                return;
            }
            obj = MonoBehaviour.Instantiate(LineBlack_pre_obj);
        }
        else{
            if(LineWhite_pre_obj == null){
                LogWhenNoModeling();
                return;
            }
            obj = MonoBehaviour.Instantiate(LineWhite_pre_obj);
        }

        obj.transform.parent = stage;
        EnemyLine el = obj.AddComponent<EnemyLine>();
        el.Init();
        EnemyLineList.Add(el);
        obj.transform.position = new Vector2(0f,(distance - 1) * 1.5f + 0.2f);

        // ----- Spawn Item -----

        if(distance > 10){
            int itemRandomValue = Random.Range(0,100);
            if(itemRandomValue < 10){
                GameObject Bomb = MonoBehaviour.Instantiate(Bomb_pre_obj);
                Bomb BombScript = Bomb.GetComponent<Bomb>();
                BombScript.SetPosy(distance);

                ItemList.Add(BombScript);
                el.SetItem(BombScript);
            }
            else if(itemRandomValue < 20){
                GameObject Cross = MonoBehaviour.Instantiate(Cross_pre_obj);
                Cross CrossScript = Cross.GetComponent<Cross>();
                CrossScript.SetPosy(distance);

                ItemList.Add(CrossScript);
                el.SetItem(CrossScript);
            }
            else{
                el.SetItem(null);
            }
        }

        // ----- Spawn Enemy -----

        if(distance > 2 && distance % 2 == 0){
            GameObject pawn = SpawnPawn();

            if(pawn == null){
                LogWhenFailToSpawn();
                return;
            }
            Pawn pawnScript = pawn.GetComponent<Pawn>();
            pawnScript.EnemyMoveY(distance);
            obj.GetComponent<EnemyLine>().Add(pawnScript);
        }

        if(distance > 6){
            if(distance % 3 == 0 && queenCount > 100){
                GameObject queen = SpawnQueen();

                if(queen == null){
                    LogWhenFailToSpawn();
                    return;
                }
                Queen queenScript = queen.GetComponent<Queen>();
                queenScript.EnemyMoveY(distance);
                obj.GetComponent<EnemyLine>().Add(queenScript);
                queenCount -= 100;
            }
            else if(distance % 2 == 0 && bishopCount > 100){
                GameObject bishop = SpawnBishop();

                if(bishop == null){
                    LogWhenFailToSpawn();
                    return;
                }
                Bishop bishopScript = bishop.GetComponent<Bishop>();
                bishopScript.EnemyMoveY(distance);
                obj.GetComponent<EnemyLine>().Add(bishopScript);
                bishopCount -= 100;
            }
            else if(rookCount > 100){
                GameObject rook = SpawnRook();

                if(rook == null){
                    LogWhenFailToSpawn();
                    return;
                }
                Rook rookScript = rook.GetComponent<Rook>();
                rookScript.EnemyMoveY(distance);
                obj.GetComponent<EnemyLine>().Add(rookScript);
                rookCount -= 100;
            }
            
        }

        if(distance > 300){
            queenCount += 40;
            bishopCount += 40;
            rookCount += 40;
        }
        else if(distance > 100){
            queenCount += 10;
            bishopCount += 15;
            rookCount += 15;
        }
        else if (distance > 50){
            queenCount += 5;
            bishopCount += 10;
            rookCount += 10;
        }
        else if (distance > 30){
            queenCount += 5;
            bishopCount += 7;
            rookCount += 8;
        }
        else{
            queenCount += 5;
            bishopCount += 5;
            rookCount += 5;
        }

        distance++;

        if(distance > 150){
            level = distance * distance / 150 + 35;
        }
        else if(distance > 100){
            level = distance * 2 - 110;
        }
        else if(distance > 30){
            level = distance - 15;
        }
        else{
            level = distance / 2;
        }

        return;
    }

    public static GameObject SpawnPawn(){
        if(Pawn_pre_obj == null)
        {
            LogWhenNoModeling();
            return null;
        }
        GameObject obj = MonoBehaviour.Instantiate(Pawn_pre_obj);
        obj.AddComponent<Pawn>();
        Pawn e = obj.GetComponent<Pawn>();

        e.InitData("Pawn",100);
        e.SetGameMode(gameMode);
        e.SetAttackPoint(level);
        e.SetHPPoint(level * 5);

        //obj.transform.localScale = new Vector3(0.04f,0.04f,1f);

        EnemyList.Add(e);
        return obj;
    }

    public static GameObject SpawnBishop(){
        if(Bishop_pre_obj == null)
        {
            LogWhenNoModeling();
            return null;
        }
        GameObject obj = MonoBehaviour.Instantiate(Bishop_pre_obj);
        obj.AddComponent<Bishop>();
        Bishop e = obj.GetComponent<Bishop>();

        e.InitData("Bishop",100);
        e.SetGameMode(gameMode);
        e.SetAttackPoint((int)(level * 1.5));
        e.SetHPPoint(level * 10);

        //obj.transform.localScale = new Vector3(0.5f,0.5f,1f);

        EnemyList.Add(e);
        return obj;
    }

    public static GameObject SpawnRook(){
        if(Rook_pre_obj == null)
        {
            LogWhenNoModeling();
            return null;
        }
        GameObject obj = MonoBehaviour.Instantiate(Rook_pre_obj);
        obj.AddComponent<Rook>();
        Rook e = obj.GetComponent<Rook>();

        e.InitData("Rook",100);
        e.SetGameMode(gameMode);
        e.SetAttackPoint((int)(level * 1.2));
        e.SetHPPoint(level * 15);

        //obj.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        EnemyList.Add(e);
        return obj;
    }
    
    public static GameObject SpawnQueen(){
        if(Queen_pre_obj == null)
        {
            LogWhenNoModeling();
            return null;
        }
        GameObject obj = MonoBehaviour.Instantiate(Queen_pre_obj);
        obj.AddComponent<Queen>();
        Queen e = obj.GetComponent<Queen>();

        e.InitData("Queen",100);
        e.SetGameMode(gameMode);
        e.SetAttackPoint(level * 3);
        e.SetHPPoint(level * 20);

        //obj.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        EnemyList.Add(e);
        return obj;
    }

    public static void SetGameOver(){
        if(gameOverPanel != null){
            if(gameMode == "score"){
                int rew;
                if(distance < 20){
                    rew = distance - NUMBER_OF_INIT_LINE;
                }
                else if (distance < 50){
                    rew = distance * distance / 25;
                }
                else if (distance < 60){
                    rew = distance * distance / 30 + 50;
                }
                else{
                    rew = distance * 3;
                }
                SceneController.SetReward(rew);
            }
            else if(gameMode == "adventure"){
                SceneController.SetReward(ScoreBoard.GetKillScore());
            }
            gameOverPanel.SetActive(true);
        }
        PlayerKnight.player.gameObject.SetActive(false);
        return;
    }

    public static int GetDistance(){
        return distance;
    }
    
    public static string GetGameMode(){
        return gameMode;
    }

    public void SoundPlay(string str){
        switch(str){
            case "Attack":
                AS.clip = attackSound;
                break;
            case "Attacked":
                AS.clip = attackedSound;
                break;
        }
        AS.Play();

        return;
    }

    public void SoundEffectSetting(float f){
        AS.volume = f;

        return;
    }

    private static void LogWhenNoEnemy(){
        Debug.Log("EnemySpawner: There is NO Enemy");

        return;
    }

    private static void LogWhenNoModeling(){
        Debug.Log("EnemySpawner: There is No Modeling.");

        return;
    }

    private static void LogWhenFailToSpawn(){
        Debug.Log("EnemySpawner: Fail to Spawn");

        return;
    }

    public static void SetGameMode(string str){
        gameMode = str;
        return;
    }
}
