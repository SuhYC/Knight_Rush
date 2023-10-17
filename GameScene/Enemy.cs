using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    protected int enemy_position_x;
    protected int enemy_position_y;
    protected GameObject obj;
    protected GameObject HPBar;
    protected string obj_type;

    protected int max_health_point;
    protected int health_point;
    protected int attact_point;
    protected bool attacking;

    protected bool ready = false;
    protected string gameMode;

    private static GameObject damageText_pre_obj;
    private static GameObject critdamageText_pre_obj;
    private static GameObject HPBar_pre_obj;
    private static GameObject cvs;
    private static Transform HPBarGroup;

    public bool test = false;

    public void Init(){
        transform.position = new Vector2((float)enemy_position_x * PlayerKnight.BLOCK, ((float)enemy_position_y - 0.7f) * PlayerKnight.BLOCK + 0.2f);//new Vector2((float)enemy_position_x * PlayerKnight.BLOCK, (float)(enemy_position_y - 1) * PlayerKnight.BLOCK + 0.2f);
        
        if(gameMode == "adventure"){
            if(HPBar_pre_obj == null){
                HPBar_pre_obj = Resources.Load("Prefabs/HealthPointPanel") as GameObject;
            }
            if(HPBar == null){
                HPBar = MonoBehaviour.Instantiate(HPBar_pre_obj, transform.position, Quaternion.identity);

                if(cvs == null){
                    cvs = GameObject.Find("Canvas");
                }
                if(HPBarGroup == null){
                    HPBarGroup = cvs.transform.Find("HPBarGroup");
                }
                HPBar.transform.SetParent(HPBarGroup);
                HPBar.transform.localScale = new Vector3(1f,1f,1f);
                
            }
        }
        
        return;
    }

    void Update(){
        if(ready){ // 생성 단계를 완전히 마쳐 정상적인 상태
            if(attacking){
                ShowMove(8f);
            }
            else{
                ShowMove(16f);
            }

            if(enemy_position_y < PlayerKnight.GetPosition().y){
                OnEndLine();
            }

        }
    }

    public void ShowMove(float float_parameter){
        if(attacking){
            if(Vector3.Distance(transform.position, new Vector3((float)enemy_position_x * PlayerKnight.BLOCK, ((float)enemy_position_y - 0.7f) * PlayerKnight.BLOCK + 0.2f, 0f)) < 0.01f){
                if(PlayerKnight.DecreaseHP(attact_point)){ // false 반환시 게임오버
                    // 서바이벌 모드는 다시 맨 윗줄로 올려야함.
                }
                EnemySpawner.ES.SoundPlay("Attacked");
                PlayerKnight.canAtt.Remove(this);
                attacking = false;
            }
        }

        Vector2 enemy_position = new Vector2((float)enemy_position_x * PlayerKnight.BLOCK, ((float)enemy_position_y - 0.7f) * PlayerKnight.BLOCK + 0.2f);
        transform.position = Vector2.Lerp(transform.position, enemy_position, Time.deltaTime * float_parameter);

        if(gameMode == "adventure"){
            Vector2 under_position = new Vector2(transform.position.x, transform.position.y - 0.85f);
            if(test){
                //under_position = new Vector2(transform.position.x - 0.6f, transform.position.y);
                //HPBar.transform.rotation = Quaternion.Euler(0f,0f,90f);
            }
            else{
                //HPBar.transform.rotation = Quaternion.Euler(0f,0f,0f);
            }
            HPBar.transform.position = under_position;
        }
    }

    public void EnemyMove(int position_x, int position_y){
        enemy_position_x = position_x;
        enemy_position_y = position_y;
        return;
    }

    public void EnemyMoveX(int x){
        enemy_position_x = x;

        return;
    }

    public void EnemyMoveY(int y){
        enemy_position_y = y;
        
        return;
    }

    public void InitData(string str, int hp){
        obj_type = str;
        attacking = false;
        health_point = hp;

        ready = true;

        return;
    }

    public void Attacked(int damage, bool crit){
        health_point -= damage;
        ShowDamage(damage, crit);

        if(health_point <= 0){
            if(gameMode == "adventure"){
                ScoreBoard.PlusKillScore(attact_point * 2);
            }
            Destroy(gameObject);
            return;
        }

        if(gameMode == "adventure"){
            HPBar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(150f * (float)(max_health_point - health_point)/max_health_point, HPBar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
            HPBar.transform.GetChild(0).localPosition = new Vector2(75f - HPBar.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x / 2f, 0f);
        }

        // 나중에 서바이벌 모드는 위로 보내야됨

        return;
    }

    private static void ShowDamage(int damage, bool crit){
        if(damageText_pre_obj == null || critdamageText_pre_obj == null){
            damageText_pre_obj = Resources.Load("Prefabs/DamageText") as GameObject;
            critdamageText_pre_obj = Resources.Load("Prefabs/DamageText2") as GameObject;
        }
        GameObject txt;
        if(crit){
            txt = MonoBehaviour.Instantiate(critdamageText_pre_obj, PlayerKnight.player.transform.position,Quaternion.identity);
        }
        else{
            txt = MonoBehaviour.Instantiate(damageText_pre_obj, PlayerKnight.player.transform.position,Quaternion.identity);
        }
        
        if(cvs == null){
            cvs = GameObject.Find("Canvas");
        }

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

    public Vector2 GetPosition(){
        return new Vector2(enemy_position_x,enemy_position_y);
    }

    protected void OnEndLine(){
        if(gameMode == "score"){
            Destroy(gameObject);
        }
        else if(gameMode == "adventure"){
                    EnemySpawner.EnemyLineList[EnemySpawner.EnemyLineList.Count - 1].Add(this);
                    EnemyMoveY(EnemySpawner.GetDistance() - 1);
                    this.enabled = true;
                    Init();
        }

        return;
    }

    public virtual bool CanAttack(){
        LogWhenNoOverrideFunctionOnCanAttack();

        return false;
    }

    public void SetAttack(){
        this.attacking = true;

        return;
    }

    protected void OnDestroy(){
        EnemySpawner.EnemyList.Remove(this);
        PlayerKnight.canAtt.Remove(this);

        if(gameMode == "adventure"){
            Destroy(HPBar);
        }
        
        return;
    }

    protected void LogWhenAttacking(){
        Debug.Log("Enemy: Attacking");

        return;
    }

    protected void LogWhenNotDefinitedType(){
        Debug.Log("Enemy: There is No Type About" + obj_type);

        return;
    }
    
    protected void LogWhenNoOverrideFunctionOnCanAttack(){
        Debug.Log("Enemy: No OverrideFunction On CanAttack");

        return;
    }
    private static void LogWhenNoDamageSkin(){
        Debug.Log("Enemy: There is No Damage Skin.");

        return;
    }

    private static void LogWhenCantFindTMPInDamageSkin(){
        Debug.Log("Enemy: There is No TMP On Damage Skin.");

        return;
    }

    public void SetGameMode(string str){
        gameMode = str;

        return;
    }

    public void SetAttackPoint(int damage){
        if(damage < 1){
            attact_point = 1;
            return;
        }
        attact_point = damage;

        return;
    }

    public void SetHPPoint(int hp){
        if(gameMode == "score"){
            max_health_point = 1;
            health_point = 1;
            return;
        }
        max_health_point = hp;
        health_point = hp;

        return;
    }
}
