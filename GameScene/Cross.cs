using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : GameItem
{
    public override void OnGetItem(){
        transform.parent.GetComponent<EnemyLine>().DeleteAllEnemyOnThisLine(); // 가로 제거
        
        for(int i = 2; i < transform.parent.parent.childCount; i++){
            Transform targetTransform = transform.parent.parent.GetChild(i);
            if(targetTransform == null){
                continue;
            }
            EnemyLine EL = targetTransform.GetComponent<EnemyLine>();

            if(EL == null){
                continue;
            }

            Enemy e = EL.ReturnEnemy(posx);
            if(e != null){
                Destroy(e.gameObject);
            }
        }

        Destroy(gameObject);

        return;
    }

    void OnDestroy(){
        EnemySpawner.ItemList.Remove(this);
    }
}
