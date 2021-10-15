using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public class Idle : PlayerState
{
    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        if (Input.GetKey(KeyCode.Z))
            {
            player.act = ACT.MENU;
            OnExit(mach);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.act = ACT.INTERACTING;
            OnExit(mach);
            mach.handleInputNow(); //get the latest input RIGHT AWAY;
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
        else
            return;

        player.act = ACT.WALKING;
        OnExit(mach);

    }
    public void OnExit(StateMachine player)
    {
        player.UpdateAct();
    }
    // Update is called once per frame
    public void UpdateState(StateMachine player)
    {
    }
}
