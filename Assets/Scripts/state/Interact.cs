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
       // player.UpdateAct();
    }

    public void UpdateState(Player player)
    {
        //float area = Vector2.Distance(player.transform.position, hit.transform.position);
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
                        target.speak();
                        player.UpdateAct();
                        hit = new RaycastHit2D();
                        return;
                        
                    }
                    break;

                case "item":
                    Debug.Log("You Hit ITEM!");
                    Item pickup = hit.collider.GetComponentInParent<Item>();
                    hit = new RaycastHit2D();
                    break;

            }
        }
            player.act = ACT.IDLE;
            player.UpdateAct();
    }
}
