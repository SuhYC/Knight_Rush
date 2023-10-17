using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerKnight : MonoBehaviour
{
    private static int player_position_x;
    private static int player_position_y;
    public static PlayerKnight player;
    public const float BLOCK = 1.5f;//0.96875f;
    const int MAX_COLUMN_NUMBER = 11;
    private static int calPhase; // 0이면 이동가능 1이면 공격판정중 2이면 방어판정중
    private static bool gameover;

    public static List<Enemy> canAtt;

    private static GameObject gameOverPanel;
    private static GameObject damageText_pre_obj;
    private static GameObject cvs;

    private static int health_point;
    private static int max_health_point;
    private static int attack_point;
    private static int critrate_point;
    private static float critdam_point;
    private static Vector2 prevPosition;

    void Awake()
    {
        damageText_pre_obj = Resources.Load("Prefabs/DamageText3") as GameObject;
        cvs = GameObject.Find("Canvas");
    }

    void Start()
    {
        if(player == null)
        {
            player = this;
        }

        if(!SceneController.MainManager.GetGuideEffect()){
            HideGuideEffect();
        }

        canAtt = new List<Enemy>();
        player_position_x = 0;
        player_position_y = 0;
        calPhase = 0;
        gameover = false;

        if(EnemySpawner.GetGameMode() == "score"){
            PlayerHPBarPanel.instance.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(!gameover){
            if(calPhase != 0)
            {
                ShowMove(8f);
            }
            else if(calPhase == 0)
            {
                ShowMove(16f);
            }
        }
    }

    public static void Init(string str, SceneController.PlayerAttribute PA){
        if(str == "score"){
            health_point = 1;
            EnemySpawner.SetGameMode("score");
            attack_point = 141;
        }
        else if(str == "adventure"){
            string skinName = SceneController.PAttribute.knightskin;
            int currentSkinNum;

            if(skinName.Length == 6){
                currentSkinNum = 0;
            }
            else{
                if(!Int32.TryParse(skinName.Substring(6,1), out currentSkinNum)){
                    LogWhenSkinNumberError();
                }
            }

            int skinLevel = SceneController.GetSkinLevel(currentSkinNum);
            health_point = AbilityText.CalHealthPoint(currentSkinNum);
            max_health_point = AbilityText.CalHealthPoint(currentSkinNum);
            EnemySpawner.SetGameMode("adventure");
            attack_point = AbilityText.CalAttPoint(currentSkinNum);
            critrate_point = AbilityText.CalCritRate(currentSkinNum);
            critdam_point = AbilityText.CalCritDam(currentSkinNum);
        }
    }

    public void MoveLeft()
    {
        if(player_position_x < -1 * (MAX_COLUMN_NUMBER / 2) + 2)
        {
            LogWhenInvalidMove();
            return;
        }
        if(calPhase != 0)
        {
            LogWhenMoving();
            return;
        }

        if(SceneController.MainManager.GetGuideEffect()){
            HideGuideEffect();
        }

        prevPosition = new Vector2(player_position_x, player_position_y);

        player_position_x -= 2;
        player_position_y += 1;
        calPhase = 1;
    }

    public void MoveRight()
    {
        if(player_position_x > (MAX_COLUMN_NUMBER / 2) - 2)
        {
            LogWhenInvalidMove();
            return;
        }
        if(calPhase != 0)
        {
            LogWhenMoving();
            return;
        }

        if(SceneController.MainManager.GetGuideEffect()){
            HideGuideEffect();
        }
        
        prevPosition = new Vector2(player_position_x, player_position_y);

        player_position_x += 2;
        player_position_y += 1;
        calPhase = 1;
    }

    public void MoveFrontLeft()
    {
        if(player_position_x < -1 * (MAX_COLUMN_NUMBER / 2) + 1)
        {
            LogWhenInvalidMove();
            return;
        }
        if(calPhase != 0)
        {
            LogWhenMoving();
            return;
        }

        if(SceneController.MainManager.GetGuideEffect()){
            HideGuideEffect();
        }
        
        prevPosition = new Vector2(player_position_x, player_position_y);

        player_position_x -= 1;
        player_position_y += 2;
        calPhase = 1;
    }

    public void MoveFrontRight()
    {
        if(player_position_x > (MAX_COLUMN_NUMBER / 2) - 1)
        {
            LogWhenInvalidMove();
            return;
        }
        if(calPhase != 0)
        {
            LogWhenMoving();
            return;
        }

        if(SceneController.MainManager.GetGuideEffect()){
            HideGuideEffect();
        }
        
        prevPosition = new Vector2(player_position_x, player_position_y);

        player_position_x += 1;
        player_position_y += 2;
        calPhase = 1;
    }

    private void ShowMove(float float_parameter) // 애니메이션이 추가되면 이 부분이 수정되어야함.
    {
        Vector2 player_position = new Vector2((float)player_position_x * BLOCK, ((float)player_position_y - 0.5f) * BLOCK);
        transform.position = Vector2.Lerp(transform.position, player_position, Time.deltaTime * float_parameter);
        
        CameraMove.CamMove(transform.position);
        if(canAtt.Count > 0){
            return;
        }

        if(calPhase != 0 && Vector3.Distance(transform.position, new Vector3((float)player_position_x * BLOCK, ((float)player_position_y - 0.5f) * BLOCK, 0f)) < 0.01f)
        {
            Enemy enemyOnPlayer = EnemySpawner.ReturnEnemyOnPlayer();
            if(calPhase == 1 && enemyOnPlayer != null){
                EnemySpawner.ES.SoundPlay("Attack");
                int r = UnityEngine.Random.Range(1,101);
                if(r > critrate_point){ // 1~100중에 뽑는데 지정확률값보다 크면 크리x (0퍼면 1~100 모두 미발동, 100퍼면 1~100 모두 발동)
                    enemyOnPlayer.Attacked(attack_point, false);
                }
                else{
                    enemyOnPlayer.Attacked((int)(attack_point * critdam_point), true);
                }
            }
            GameItem ItemOnPlayer =EnemySpawner.ReturnItemOnPlayer();
            if(calPhase == 1 && ItemOnPlayer != null){
                ItemOnPlayer.OnGetItem();
            }

            calPhase = 2;

            canAtt = EnemySpawner.CalCulateEnemyAttack();
            if(canAtt.Count > 0){
                EnemySpawner.ShowAttack(canAtt);
                return;
            }
            calPhase = 0;
            

            if(SceneController.MainManager.GetGuideEffect()){
                ShowGuideEffect();
            }
            
            PositionResetAfterMove();
        }
    }

    private void PositionResetAfterMove()
    {
        EnemySpawner.EnemyMove();
        return;
    }

    private void LogWhenInvalidMove()
    {
        Debug.Log("PlayerKnight: Unable to Move");

        return;
    }

    private void LogWhenMoving()
    {
        Debug.Log("PlayerKnight: Moving");

        return;
    }

    public static Vector2 GetPosition()
    {
        Vector2 player_position = new Vector2(player_position_x, player_position_y);
        return player_position;
    }

    public static Vector2 GetPrevPosition()
    {
        return prevPosition;
    }

    public static bool DecreaseHP(int damage){ // false를 반환시 게임오버,true를 반환 시 게임 이어서 진행
        health_point -= damage;
        ShowDamage(damage);

        if(EnemySpawner.GetGameMode() == "adventure"){
            PlayerHPBarPanel.instance.Renew((float)health_point / (float)max_health_point);
        }

        if(health_point <= 0){
            EnemySpawner.SetGameOver();
            return false;
        }

        return true;
    }

    public static void ShowDamage(int damage){
        if(damageText_pre_obj == null){
            LogWhenNoDamageSkin();
            return;
        }
        GameObject txt = MonoBehaviour.Instantiate(damageText_pre_obj,player.transform.position,Quaternion.identity);

        txt.transform.SetParent(cvs.transform);
        txt.transform.localScale = new Vector3(1f,1f,1f);
        TextMeshProUGUI tmp = txt.GetComponent<TextMeshProUGUI>();

        if(tmp == null){
            LogWhenCantFindTMPInDamageSkin();
            return;
        }

        tmp.text = damage.ToString();

        return;
    }

    private void ShowGuideEffect(){
        player.transform.GetChild(0).gameObject.SetActive(true);

        if(player_position_x < -1 * (MAX_COLUMN_NUMBER / 2) + 2){ // Move Left
            player.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        else{
            player.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

        if(player_position_x < -1 * (MAX_COLUMN_NUMBER / 2) + 1){ // Move FrontLeft
            player.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else{
            player.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }

        if(player_position_x > (MAX_COLUMN_NUMBER / 2) - 2){ // Move Right
            player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else{
            player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if(player_position_x > (MAX_COLUMN_NUMBER / 2) - 1){ // Move FrontRight
            player.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        else{
            player.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }

        return;
    }

    private void HideGuideEffect(){
        player.transform.GetChild(0).gameObject.SetActive(false);

        return;
    }

    private static void LogWhenNoDamageSkin(){
        Debug.Log("PlayerKnight: There is No Damage Skin.");

        return;
    }

    private static void LogWhenCantFindTMPInDamageSkin(){
        Debug.Log("PlayerKnight: There is No TMP On Damage Skin.");

        return;
    }

    private static void LogWhenSkinNumberError(){
        Debug.Log("PlayerKnight: Skin Number Error");

        return;
    }
}
