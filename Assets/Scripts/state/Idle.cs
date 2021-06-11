using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public class Idle : PlayerState
{
    public void handleInput(Player player)
    {
        if (Input.GetKey(KeyCode.Z))
            {
            player.act = ACT.MENU;
            OnExit(player);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.act = ACT.INTERACTING;
            OnExit(player);
            player.handleInputNow(); //get the latest input RIGHT AWAY;
            return;
        }
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        if (axisX == -1)
            player.dir = DIRECTION.LEFT;
        else if (axisX == 1)
            player.dir = DIRECTION.RIGHT;
        else if (axisY == 1)
            player.dir = DIRECTION.UP;
        else if (axisY == -1)
            player.dir = DIRECTION.DOWN;
        if (axisX == -1 || axisX == 1 || axisY == 1 || axisY == -1)
        {
            player.act = ACT.WALKING;
            OnExit(player);
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
