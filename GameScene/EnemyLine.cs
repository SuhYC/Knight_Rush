using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : MonoBehaviour
{
    private Enemy[] EnemyList = new Enemy[11];
    private int itemPosition; // -1~10, -1:null 0~10:valid
    private GameItem GI;
    private int lifeOfLine;
    private static GameObject stage;

    void Awake(){
        if(stage == null){
            stage = GameObject.Find("Stage");
        }
    }

    void Start(){
        transform.parent = stage.transform;
    }

    public void Init(){
        lifeOfLine = 0;

        for(int i = 0; i<11; i++){
            EnemyList[i] = null;
        }
    }

    public void Add(Enemy e){
        if(Reposition(e)){
            e.transform.parent = transform;
            e.Init();
        }
        else{
            Destroy(e);
        }

        return;
    }

    public void SetItem(GameItem I){
        if(I == null){
            itemPosition = -1;
            return;
        }

        GI = I;

        int posx = Random.Range(-5,6);
        itemPosition = posx + 5;
        I.transform.parent = this.transform;
        I.SetPosx(posx);
    }

    public void DeleteAllEnemyOnThisLine(){
        for(int i = 0; i<11; i++){
            if(EnemyList[i] != null){
                Destroy(EnemyList[i].gameObject);
                EnemyList[i] = null;
            }
        }

        return;
    }

    private bool Reposition(Enemy e){
        int posx;

        for(int i = 0; i<11; i++){ // 10회의 리롤 후에도 위치를 잡지 못하면 소환하지 않음
            posx = Random.Range(-5,6);
            if(EnemyList[posx + 5] == null && posx + 5 != itemPosition){
                EnemyList[posx + 5] = e;
                e.EnemyMoveX(posx);
                return true;
            }
        }

        return false;
    }

    public void MoveLine(){
        if(lifeOfLine++ > 8){
            Destroy(gameObject);
        }
        GetComponent<Renderer>().sortingOrder = lifeOfLine;

        Enemy e;
        for(int i = 0; i < 11; i++){
            e = EnemyList[i];
            if(e != null){
                e.transform.GetComponent<Renderer>().sortingOrder = lifeOfLine;
            }
        }

        return;
    }

    public Enemy ReturnEnemy(int posx){
        return EnemyList[posx+5];
    }
    
    public GameItem ReturnItem(){
        return GI;
    }

    void OnDestroy(){
        EnemySpawner.EnemyLineList.Remove(this);
        for(int i = 0; i<11; i++){
            if(EnemyList[i] != null){
                if(EnemySpawner.GetGameMode() == "score"){
                    Destroy(EnemyList[i].gameObject);
                }
                else if(EnemySpawner.GetGameMode() == "adventure"){
                    
                }
            }
        }
    }

    private void LogWhenNoEnemyObjectOnList(){
        Debug.Log("EnemyLine: NO Enemy Object on List");

        return;
    }
    
}
