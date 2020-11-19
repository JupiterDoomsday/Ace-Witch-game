using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Npc : MonoBehaviour
{
    public ACT act;
    public DIRECTION dir;
    public YarnProgram script;
    // Start is called before the first frame update
    void Start()
    {
    }

    //[YarnCommand("facePlayer")]
    //alows us to change where the npc is facing when talking to the player
    public void resetNPCDir(Player marji)
    {
        switch (marji.dir)
        {
            case (DIRECTION.UP):
                this.dir = DIRECTION.DOWN;
                break;
            case (DIRECTION.DOWN):
                this.dir = DIRECTION.UP;
                break;
            case (DIRECTION.LEFT):
                this.dir = DIRECTION.RIGHT;
                break;
            case (DIRECTION.RIGHT):
                this.dir = DIRECTION.LEFT;
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            act = ACT.TALKING;
        }
    }
}
