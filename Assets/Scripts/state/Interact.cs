using System.Collections;
using System.Collections.Generic;
using state;
using UnityEngine;

public class Interact : PlayerState { 

    private RaycastHit2D hit;
    public float dist = 10f;

    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        switch (player.dir)
        {
            case DIRECTION.UP:
                hit = Physics2D.Raycast(player.transform.position, Vector2.up, dist, LayerMask.GetMask("npc", "item"));
                break;
            case DIRECTION.DOWN:
                hit = Physics2D.Raycast(player.transform.position, Vector2.down, dist, LayerMask.GetMask("npc", "item"));
                break;
            case DIRECTION.LEFT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.left, dist, LayerMask.GetMask("npc", "item"));
                break;
            case DIRECTION.RIGHT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.right, dist, LayerMask.GetMask("npc", "item"));
                break;
        }
    }

    public void OnExit(StateMachine player)
    {
        player.player.setIdle();
        hit = new RaycastHit2D();
        player.UpdateAct();
    }

    public void UpdateState(StateMachine mach)
    {
        Player player = mach.player;
        if (hit.collider != null && hit.distance < 2f)
        {
            switch(hit.collider.tag)
            {
                case "npc":
                    Debug.Log("You Hit NPC!");
                    Npc target = hit.collider.GetComponentInParent<Npc>();
                    if (target.startScript != null && target.corespondingDir(player))
                    {
                        player.act = ACT.TALKING;
                        mach.UpdateAct();
                        mach.isTalking(target);
                        return;
                    }
                    break;

                case "item":
                    Debug.Log("You Hit ITEM!");
                        PickUp pickup = hit.collider.GetComponentInParent<PickUp>();
                        player.invo.AddItem(pickup.item, pickup.amt);
                        pickup.gameObject.SetActive(false);
                        //hit.collider.enabled = false;
                    break;
            }
        }
            player.act = ACT.IDLE;
            OnExit(mach);
    }
}
