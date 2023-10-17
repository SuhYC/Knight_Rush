using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkinAnimation : MonoBehaviour
{
    private float timeCount;
    public int flexibleParameter = 8;

    void OnEnable(){
        Init();
    }

    void Update()
    {
        IdleAnimation();
    }

    void Init(){
        timeCount = Random.Range(0.0f,0.9f);
    }

    void IdleAnimation(){
        timeCount += Time.deltaTime;

        while(timeCount >= 1f){
            timeCount -= 1f;
        }

        Vector2 vec; // scale
        Vector2 vec2; // position
        if(timeCount < 0.5f){
            vec = new Vector2(1f + timeCount / 2 / flexibleParameter, 1f - timeCount / flexibleParameter);
            vec2 = new Vector2(0f, 384f * (1f - timeCount / flexibleParameter) - 384f);
        }
        else{
            vec = new Vector2((1f + 1f / 2 / flexibleParameter) - timeCount / 2 / flexibleParameter, (1f - 1f / flexibleParameter) + timeCount / flexibleParameter);
            vec2 = new Vector2(0f, 384f * ((1f - 1f / flexibleParameter) + timeCount / flexibleParameter) - 384f);
        }

        transform.localScale = vec;
        transform.localPosition = vec2;
    }
}
