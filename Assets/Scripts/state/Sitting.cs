using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    [SerializeField]
    public int sittingTime;
    private DIRECTION SittingDir;
    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        float X = 0;
        float Y = 0;
        if (!player.IsSitting())
        {
            player.act = ACT.WALKING;
            return;
        }
        switch(SittingDir)
        {
            case DIRECTION.DOWN:
            case DIRECTION.UP:
                float axisY = Input.GetAxisRaw("Vertical");
                break;
            case DIRECTION.LEFT:
            case DIRECTION.RIGHT:
                float axisX = Input.GetAxisRaw("Horizontal");
                break;
        }
    }
 
}
