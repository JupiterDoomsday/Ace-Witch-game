using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    public void handleInput(Player player)
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        player.player_animator.SetInteger("x", (int)axisX);
        player.player_animator.SetInteger("y", (int)axisY);
        switch(axisX){
            case -1:
                player.dir = DIRECTION.LEFT;
                return;
                
            case 1:
                player.dir = DIRECTION.RIGHT;
                return;
        }
        switch (axisY)
        {
            case -1:
                player.dir = DIRECTION.DOWN;
                return;
            case 1:
                player.dir = DIRECTION.UP;
                return;
            default:
                player.act = ACT.IDLE;
                player.UpdateAct();
                break;
        }

    }

    // Update is called once per frame
    public void UpdateState(Player player)
    {
            player.move();
    }
}
