using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public class Walking : MonoBehaviour, PlayerState
{
    public int playerId;
    //private bool inMotion = false;
    private Vector2 moveDir;
    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        if(player.IsSitting())
        {
            player.act = ACT.SITTING;
            OnExit(mach);
            return;
        }
        /*if(inMotion)
            return;
        */
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
                OnExit(mach);
                break;
        }
    }
    public void OnExit(StateMachine mach)
    {
        Player p = mach.player;
        p.player_animator.SetInteger("x", 0);
        p.player_animator.SetInteger("y", 0);
        p.rgb2d.velocity = Vector2.zero;
        p.player_animator.enabled = false;
        p.setDirectionSprite();
        mach.footstepsSoundFX.enabled = false;
        mach.UpdateAct();
    }
    // Update is called once per frame
    public void UpdateState(StateMachine mach)
    {
        /* (inMotion)
            return;
        */
        Player player = mach.player;
        if (player.act != ACT.WALKING)
            return;
        switch (player.dir)
        {
            case DIRECTION.LEFT:
                moveDir= new Vector2(-2,0);
                break;
            case DIRECTION.RIGHT:
                moveDir = new Vector2(2, 0);
                break;
            case DIRECTION.UP:
                moveDir = new Vector2(0, 2);
                break;
            case DIRECTION.DOWN:
                moveDir = new Vector2(0, -2);
                break;
        }
        //inMotion = true;
        //StartCoroutine(GridMovement(player));
        player.rgb2d.velocity= moveDir * player.speed;
    }

    private IEnumerator GridMovement(Player p)
    {
        Vector3 startPos = p.transform.position;
        Vector3 targeTPos = moveDir;
        Vector3 finalPos = startPos + (targeTPos);
        float inTime = 2f;
        float elapsedTime = 0;
        while (elapsedTime < inTime)
        {
            p.transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / inTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        p.transform.position = finalPos;
        //inMotion = false;
    }
}
