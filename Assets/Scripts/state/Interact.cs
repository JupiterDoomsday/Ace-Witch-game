using System.Collections;
using System.Collections.Generic;
using state;
using UnityEngine;

public class Interact : PlayerState { 

    private RaycastHit2D hit;
    public void handleInput(Player player)
    {
        //Vector2 curpos = new Vector2(transform.position.x, (transform.position.y - 1.7f));
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
                        return;
                    }
                    break;
                case "item":
                    Debug.Log("You Hit ITEM!");
                    Item pickup = hit.collider.GetComponentInParent<Item>();
                    break;

            }
        }
            player.act = ACT.IDLE;
            player.UpdateAct();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}