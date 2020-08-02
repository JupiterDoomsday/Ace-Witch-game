using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : PlayerState
{
    public void handleInput(Player marji)
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        if (axisX == -1)
            marji.dir = DIRECTION.LEFT;
        else if (axisX == 1)
            marji.dir = DIRECTION.RIGHT;
        else if (axisY == -1)
            marji.dir = DIRECTION.DOWN;
        else if (axisY == 1)
            marji.dir = DIRECTION.UP;
        else
            marji.act = ACT.IDLE;
    }

    // Update is called once per frame
    public void FixedUpdate(Player marji)
    {
        marji.move();
    }
}
