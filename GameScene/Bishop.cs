using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Enemy
{
    public override bool CanAttack(){
        Vector2 PlayerPosition = PlayerKnight.GetPosition();
        int indexOfLine = EnemySpawner.EnemyLineList.IndexOf(transform.parent.GetComponent<EnemyLine>());
        Vector2 prevPosition = PlayerKnight.GetPrevPosition();

        if(enemy_position_y > prevPosition.y + 5){
            return false; // 시야 밖에서 죽이는 일 방지
        }

        for(int i = 1; enemy_position_x + i < 6 && enemy_position_y - i >= PlayerPosition.y; i++){
            if(PlayerPosition.x == enemy_position_x + i && enemy_position_y - i == PlayerPosition.y){ // 우하단 대각 방향으로 방해물 없이
                return true;
            }

            if(indexOfLine - i >= 0 && EnemySpawner.EnemyLineList[indexOfLine - i].ReturnEnemy(enemy_position_x + i) != null){ // 방해물 있으면 반복중단
                break;
            }
        }

        for(int i = 1; enemy_position_x - i > -6 && enemy_position_y - i >= PlayerPosition.y; i++){
            if(PlayerPosition.x == enemy_position_x - i && enemy_position_y - i == PlayerPosition.y){ // 좌하단
                return true;
            }

            if(indexOfLine - i >= 0 && EnemySpawner.EnemyLineList[indexOfLine - i].ReturnEnemy(enemy_position_x - i) != null){ // 방해물
                break;
            }
        }
        return false;
    }
}
