using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public int posx; // -5~+5
    public int posy;

    public virtual void OnGetItem(){

    }

    public void SetPosx(int x){
        if(x > 5 || x < -5){
            LogWhenInvalidPosition();
            return;
        }
        posx = x;

        transform.localPosition = new Vector2(posx * PlayerKnight.BLOCK * 2f,0f); // EnemyLine의 Scale이 0.5라 이렇게 설정.

        return;
    }

    public void SetPosy(int y){
        posy = y;

        return;
    }

    public Vector2 GetPosition(){
        return new Vector2((float)posx, (float)posy);
    }

    protected void LogWhenInvalidPosition(){
        Debug.Log("GameItem: Invalid Position");

        return;
    }
}
