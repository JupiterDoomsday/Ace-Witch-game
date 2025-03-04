using System.Collections;
using System.Collections.Generic;
using CustomeInteractables;
using state;
using UnityEngine;

public class Interact :  PlayerState 
{ 

    private GameObject curIteractable;

    public void handleInput(StateMachine mach)
    {
        Player player = mach.player;
        player.CheckCollisions();
        /*float axisX = Input.GetAxisRaw("Horizontal");
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
        }*/

    }

    public void OnExit(StateMachine mach)
    {
        mach.player.ResetCollision();
        mach.player.act = ACT.IDLE;
        mach.UpdateAct();
    }

    public void UpdateState(StateMachine mach)
    {
        Player player = mach.player;
        Collider2D target = mach.player.GetCollision();
        if (target)
        {
            switch(target.tag)
            {
                case "npc":
                    Debug.Log("You Hit NPC!");
                    Npc actor = target.GetComponentInParent<Npc>();
                    if (actor.CorrespondingDirection(player))
                    {
                        Debug.Log("You Hit: " + actor.Name);
                        OnExit(mach);
                        mach.isTalking(actor);
                        return;
                    }
                    break;

                case "item":
                    Debug.Log("You Hit ITEM!");
                        PickUp pickup = target.GetComponentInParent<PickUp>();
                        player.invo.AddItem(pickup.item, pickup.amt);
                        pickup.gameObject.SetActive(false);
                    break;
                case "talkingItem":
                    TalkableItem item = target.GetComponentInParent<TalkableItem>();
                    if (item.CorrespondingDirection(player))
                    {
                        player.act = ACT.TALKING;
                        OnExit(mach);
                        mach.PlayYarnScript(item.Talk());
                        return;
                    }
                    break;
                case "itemPuzzle":
                    ItemReqPuzzle puzzle = target.GetComponent<ItemReqPuzzle>();
                    if(puzzle.CorrespondingDirection(player))
                    {
                        OnExit(mach);
                        puzzle.Interacting(player);
                        return;
                    }
                    break;
            }
        }
            OnExit(mach);
    }
}
