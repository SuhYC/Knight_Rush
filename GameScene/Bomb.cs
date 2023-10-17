using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : GameItem
{
    public override void OnGetItem(){
        int thisLineNumber = transform.parent.GetSiblingIndex();

        for(int i = thisLineNumber - 1; i < thisLineNumber + 2; i++){
            Transform targetTransform = transform.parent.parent.GetChild(i);

            if(targetTransform == null){
                continue;
            }

            EnemyLine EL = targetTransform.GetComponent<EnemyLine>();

            if(EL == null){
                continue;
            }

            for(int j = posx - 1; j < posx + 2; j++){
                if(j < -5 || j > 5){
                    continue;
                }
                Enemy e = EL.ReturnEnemy(j);

                if(e == null){
                    continue;
                }

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
