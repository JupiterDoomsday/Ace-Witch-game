using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
  
   public void handleInput(Player player)
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        if (axisX >= -1 && axisX < 0)
            player.dir = DIRECTION.LEFT;
        else if (axisX <= 1 && axisX > 0)
            player.dir = DIRECTION.RIGHT;
        else if (axisY <= 1 && axisY > 0)
            player.dir = DIRECTION.UP;
        else if (axisY >= -1 && axisY < 0)
            player.dir = DIRECTION.DOWN;
        if (axisX == -1 || axisX == 1 || axisY == 1 || axisY == -1)
        {
            player.act = ACT.WALKING;
            player.UpdateAct();
        }
    }
    public void OnExit(Player player)
    {
        player.UpdateAct();
    }
    // Update is called once per frame
    public void UpdateState(Player player)
    {
    }
}
