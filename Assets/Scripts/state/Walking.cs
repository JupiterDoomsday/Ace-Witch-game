using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public class Walking : MonoBehaviour, PlayerState 
{
    public int playerId;
    public Player player;
    public Vector3 moveDir;
    private Vector3 targetPos, ogPos;
    private bool isMoving = false;
    public void handleInput(StateMachine mach)
    {
        //Player player = mach.player;
        if(player.IsSitting())
        {
            player.act = ACT.SITTING;
            OnExit(mach);
            return;
        }

        if(!isMoving)
        {
            float axisX = Input.GetAxisRaw("Horizontal");
            float axisY = Input.GetAxisRaw("Vertical");
            player.player_animator.SetInteger("y", (int)axisY);
            switch (axisX)
            {
                case -1:
                    player.dir = DIRECTION.LEFT;
                    player.player_animator.SetInteger("x", (int)axisX);
                    player.player_animator.SetInteger("y", 0);
                    moveDir = new Vector3(-2, 0, 0);
                    return;

                case 1:
                    player.dir = DIRECTION.RIGHT;
                    player.player_animator.SetInteger("x", (int)axisX);
                    player.player_animator.SetInteger("y", 0);
                    moveDir = new Vector3(2, 0, 0);
                    return;
            }
            switch (axisY)
            {
                case -1:
                    player.dir = DIRECTION.DOWN;
                    player.player_animator.SetInteger("y", (int)axisY);
                    player.player_animator.SetInteger("x", 0);
                    moveDir = new Vector3(0, -2, 0);
                    return;
                case 1:
                    player.dir = DIRECTION.UP;
                    player.player_animator.SetInteger("y", (int)axisY);
                    player.player_animator.SetInteger("x", 0);
                    moveDir = new Vector3(0, 2, 0);
                    return;
            }
            player.player_animator.SetInteger("y", 0);
            player.player_animator.SetInteger("x", 0);
            player.act = ACT.IDLE;
            OnExit(mach);
        }
    }
    public void OnExit(StateMachine mach)
    {
        Player p = mach.player;
        p.rgb2d.velocity = Vector2.zero;
        p.player_animator.enabled = false;
        p.setDirectionSprite();
        mach.footstepsSoundFX.enabled = false;
        mach.UpdateAct();
    }
    // Update is called once per frame
    public void UpdateState(StateMachine mach)
    {
        Player player = mach.player;
        if (player.act != ACT.WALKING)
            return;
        player.rgb2d.velocity = moveDir * player.speed;
    }

    IEnumerator GridMovement()
    {
        isMoving = true;
        ogPos = player.transform.position;
        targetPos = ogPos + moveDir;
        float interpolateTime = 0;

        while(interpolateTime < 2.0f)
        {
            Vector3 newPos = Vector3.Lerp(ogPos, targetPos, interpolateTime / 2.0f);
            player.rgb2d.MovePosition(newPos);
            interpolateTime += Time.deltaTime;
            yield return null;
        }
        player.rgb2d.MovePosition(targetPos);
        isMoving = false;
    }
}
