using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Enemy
{
    public override bool CanAttack(){
        Vector2 PlayerPosition = PlayerKnight.GetPosition();
        
        if((int)PlayerPosition.y == enemy_position_y - 1){
            if((int)PlayerPosition.x == enemy_position_x + 1 || (int)PlayerPosition.x == enemy_position_x - 1){
                return true;
            }
        }
        
        return false;
    }
    
}
