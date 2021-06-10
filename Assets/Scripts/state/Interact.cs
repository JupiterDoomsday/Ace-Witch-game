using System.Collections;
using System.Collections.Generic;
using state;
using UnityEngine;

public class Interact : PlayerState { 

    private RaycastHit2D hit;
    public float dist = 4f;
    public void handleInput(Player player)
    {
        switch (player.dir)
        {
            case DIRECTION.UP:
                hit = Physics2D.Raycast(player.transform.position, Vector2.up);
                break;
            case DIRECTION.DOWN:
                hit = Physics2D.Raycast(player.transform.position, Vector2.down);
                break;
            case DIRECTION.LEFT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.left);
                break;
            case DIRECTION.RIGHT:
                hit = Physics2D.Raycast(player.transform.position, Vector2.right);
                break;
        }
    }

    public void OnExit(Player player)
    {
        hit = new RaycastHit2D();
        player.UpdateAct();
    }

    public void UpdateState(Player player)
    {
        if (hit.collider != null && hit.distance < 2f)
        {
            switch(hit.collider.tag)
            {
                case "npc":
                    Debug.Log("You Hit NPC!");
                    Npc target = hit.collider.GetComponentInParent<Npc>();
                    if (target.startScript != null && target.corespondingDir(player))
                    {
                        player.isTalking(target);
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
            OnExit(player);
    }
}
