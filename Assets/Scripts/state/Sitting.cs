using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;

public class Sitting : PlayerState
{
    [SerializeField]
    public int sittingTime;
    private DIRECTION SittingDir;
    private Vector2 moveDir;

    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        float x = 0;
        float y = 0;
        switch(SittingDir)
        {
            case DIRECTION.DOWN:
            case DIRECTION.UP:
                y = Input.GetAxisRaw("Vertical");
                break;
            case DIRECTION.LEFT:
            case DIRECTION.RIGHT:
                x = Input.GetAxisRaw("Horizontal");
                break;
        }
        if (x == -1 && player.dir != DIRECTION.LEFT)
            return;
        else if (x == 1 && player.dir != DIRECTION.RIGHT)
            return;
        else if (y == 1 && player.dir != DIRECTION.UP)
            return;
        else if (y == -1 && player.dir != DIRECTION.DOWN)
            return;
        else if(x == 0 && y==0)
            return;
        OnExit(mach);
    }
    public void OnExit(StateMachine mach)
    {
        mach.player.act = ACT.WALKING;
        mach.UpdateAct();
    }

    public void UpdateState(StateMachine mach)
    {

    }
}
