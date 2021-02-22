using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
//this is an enum that represents the relationship of an NPC and
public enum SOCIAL_STANDING
{
    COMARADE,
    FAMILY,
    BEST_FIREND,
    AQUINTANCE,
    RIVAL
};

public class Npc : MonoBehaviour
{
    public ACT act;
    public SOCIAL_STANDING player_relationship;
    public DIRECTION dir;
    public Sprite Profile;
    public string Name;
    public  string startNode;
    public YarnProgram startScript;
    public DialogueRunner runner;
    public AudioSource srcAudi;
    public AudioClip audio;

    //[YarnCommand("facePlayer")]
    //alows us to change where the npc is facing when talking to the player

    private void Start()
    {
        runner.Add(startScript);
    }
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
    public bool corespondingDir(Player marji)
    {
        switch (this.dir)
        {
            case (DIRECTION.UP):
                return (marji.dir == DIRECTION.DOWN);
            case (DIRECTION.DOWN):
                return marji.dir == DIRECTION.UP;
            case (DIRECTION.LEFT):
                return marji.dir == DIRECTION.RIGHT;
            case (DIRECTION.RIGHT):
                return this.dir == DIRECTION.LEFT;
            default:
                return false;
        }
        //return false;
    }

    public void PlayAudio()
    {
        if (audio == null)
            return;

        srcAudi.PlayOneShot(audio, .7f);
    }

    private void OnTriggerStay2D(Collider2D ot)
    {
        if (ot.tag.Equals("Player") && act == ACT.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Player player = ot.GetComponentInParent<Player>();
                if (this.corespondingDir(player))
                {
                    act = ACT.TALKING;
                    runner.StartDialogue(startNode);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        act = ACT.IDLE;
    }
}
