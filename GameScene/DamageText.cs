using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private Vector2 start_position;
    private float last_time;
    private float random_pivot;
    private TextMeshProUGUI tmpro;

    void Start()
    {
        last_time = 1f;
        random_pivot = Random.Range(-0.50f,-1.00f);
        start_position = transform.position;
        tmpro = GetComponent<TextMeshProUGUI>();

        if(tmpro == null){
            LogWhenNoTextMeshPro();
        }
    }

    void Update()
    {
        acting();
    }

    private void acting(){
        last_time -= Time.deltaTime;

        if(last_time < 0){
            Destroy(gameObject);
        }

        Color col = tmpro.color;
        col.a = last_time/1f;
        tmpro.color = col;

        float positionx = start_position.x + 3.0f - last_time * 3;
        float positiony = start_position.y + random_pivot * (positionx - start_position.x) * (positionx - start_position.x - 3);
        Vector2 cal_position = new Vector2(positionx,positiony);

        transform.position = Vector2.Lerp(transform.position, cal_position, 0.1f);
    }

    private void LogWhenNoTextMeshPro(){
        Debug.Log("DamageText: NO TMP");

        return;
    }
}
