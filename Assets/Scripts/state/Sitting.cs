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
    private bool getOff =false;

    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        float x = 0;
        float y = 0;
        DIRECTION sitDir = mach.player.dir;
        switch (sitDir)
        {
            case DIRECTION.DOWN:
            case DIRECTION.UP:
                y = Input.GetAxisRaw("Vertical");
                player.player_animator.SetInteger("y", (int)y);
                break;
            case DIRECTION.LEFT:
            case DIRECTION.RIGHT:
                x = Input.GetAxisRaw("Horizontal");
                player.player_animator.SetInteger("x", (int)x);
                break;
        }
        switch(sitDir)
        {
            case DIRECTION.UP:
                    getOff = y > 0;
                break;
            case DIRECTION.DOWN:
                    getOff = y < 0;
                break;
            case DIRECTION.LEFT:
                    getOff = x < 0;
                break;
            case DIRECTION.RIGHT:
                    getOff = x > 0;
                break;

        }
        //OnExit(mach);
    }
    public void OnExit(StateMachine mach)
    {
        Debug.Log("Exiting sitting and now walking");
        Player p = mach.player;
        //p.rgb2d.velocity = Vector2.zero;
        p.act = ACT.WALKING;
        p.isSitting = false;
        getOff = false;
        mach.UpdateAct();
    }

    public void UpdateState(StateMachine mach)
    {
        if(getOff)
        {
            Player player = mach.player;
            player.isSitting = false;
            switch (player.dir)
            {
                case DIRECTION.LEFT:
                    moveDir = new Vector2(-1, 0);
                    break;
                case DIRECTION.RIGHT:
                    moveDir = new Vector2(1, 0);
                    break;
                case DIRECTION.UP:
                    moveDir = new Vector2(0, 1);
                    break;
                case DIRECTION.DOWN:
                    moveDir = new Vector2(0, -1);
                    break;
            }
            player.rgb2d.velocity = moveDir * player.speed;
            OnExit(mach);
        }
    }
}
