﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public class Walking : PlayerState
{
    public int playerId;
    public Player player;
    public Vector2 moveDir;
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
                OnExit(player);
                break;
        }

    }


    //this function gets called when the walking animation breaks and we're at the idel state
    //the play is suposed to be facing the last direction they're facing
    //set it up in walking state so that this gets called once
    //instead of being called each frame at the idle state
    public void setDirection(Player p) {
        switch (p.dir)
        {
            case DIRECTION.LEFT:
                p.spriteRender.sprite = p.dirSprites[0];
                break;
            case DIRECTION.RIGHT:
                p.spriteRender.sprite = p.dirSprites[1];
                break;
            case DIRECTION.UP:
                p.spriteRender.sprite = p.dirSprites[2];
                break;
            case DIRECTION.DOWN:
                p.spriteRender.sprite = p.dirSprites[3];
                break;
        }
 
    }
    public void OnExit(Player p)
    {
        p.rgb2d.velocity = Vector2.zero;
        p.player_animator.enabled = false;
        setDirection(p);
        p.setIdle();
        p.UpdateAct();
    }
    // Update is called once per frame
    public void UpdateState(Player player)
    {
        
        switch (player.dir)
        {
            case DIRECTION.LEFT:
                moveDir= new Vector2(-1,0);
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
        player.rgb2d.velocity= moveDir * player.speed;
    }
}