using System.Collections;
using System.Collections.Generic;
using CustomeInteractables;
using state;
using UnityEngine;

public class Interact :  PlayerState 
{ 

    private GameObject curIteractable;
    private RaycastHit2D hit;
    public float dist = 2f;

    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        float axisX = Input.GetAxisRaw("Horizontal");
        switch (player.dir)
        {
            case DIRECTION.UP:
                hit = Physics2D.Raycast(player.transform.position, Vector2.up, dist, LayerMask.GetMask("npc", "item"));
                Debug.DrawRay(player.transform.position, (Vector2.up*dist), Color.green);
                break;
            case DIRECTION.DOWN:
                hit = Physics2D.Raycast(player.transform.position, Vector2.down, dist, LayerMask.GetMask("npc", "item"));
                Debug.DrawRay(player.transform.position, (Vector2.down * dist), Color.green);
                break;
            case DIRECTION.LEFT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.left, dist, LayerMask.GetMask("npc", "item"));
                Debug.DrawRay(player.transform.position, (Vector2.left * dist), Color.green);
                break;
            case DIRECTION.RIGHT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.right, dist, LayerMask.GetMask("npc", "item"));
                Debug.DrawRay(player.transform.position, (Vector2.right * dist), Color.green);
                break;
        }
        
    }

    public void OnExit(StateMachine player)
    {
        hit = new RaycastHit2D();
        player.UpdateAct();
    }

    public void UpdateState(StateMachine mach)
    {
        Player player = mach.player;
        if (hit.collider != null && hit.distance < 1f)
        {
            switch(hit.collider.tag)
            {
                case "npc":
                    Debug.Log("You Hit NPC!");
                    Npc actor = hit.collider.GetComponentInParent<Npc>();
                    if (actor.corespondingDir(player))
                    {
                        Debug.Log("You Hit: " + actor.Name);
                        player.act = ACT.TALKING;
                        OnExit(mach);
                        mach.isTalking(actor);
                        return;
                    }
                    break;

                case "item":
                    Debug.Log("You Hit ITEM!");
                        PickUp pickup = hit.collider.GetComponentInParent<PickUp>();
                        player.invo.AddItem(pickup.item, pickup.amt);
                        pickup.gameObject.SetActive(false);
                        hit.collider.enabled = false;
                    break;
                case "talkingItem":
                Debug.Log("You Hit TALKING ITEM!");
                    TalkableItem item = hit.collider.GetComponentInParent<TalkableItem>();
                        player.act = ACT.TALKING;
                        OnExit(mach);
                        mach.talkingState.dialogueRunner.StartDialogue(item.startNode);
                        return;
                break;  

            }
        }
            player.act = ACT.IDLE;
            OnExit(mach);
    }
}
