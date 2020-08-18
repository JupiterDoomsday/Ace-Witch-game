using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    public void handleInput(Player player)
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        if (axisX == -1)
            player.dir = DIRECTION.LEFT;
        else if (axisX == 1)
            player.dir = DIRECTION.RIGHT;
        else if (axisY == -1)
            player.dir = DIRECTION.DOWN;
        else if (axisY == 1)
            player.dir = DIRECTION.UP;
        else
        {
            player.act = ACT.IDLE;
            player.UpdateAct();
        }
        player.setPlayerDirectionSprite(); 
    }

    // Update is called once per frame
    public void UpdateState(Player player)
    {
            player.move();
    }
}
