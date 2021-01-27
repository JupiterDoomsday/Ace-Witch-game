using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Npc : MonoBehaviour
{
    public ACT act;
    public DIRECTION dir;
    public Sprite Profile;
    public string Name;
    public YarnProgram script;
    public DialogueRunner runner;
    public AudioSource srcAudi;
    public AudioClip audio;
    // Start is called before the first frame update
    void Start()
    {
        runner.Add(script);
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
    public void PlayAudio()
    {
        if (audio == null)
            return;

        srcAudi.PlayOneShot(audio, .7f);
    }

    private void OnTriggerStay2D(Collider2D ot)
    {
        if (ot.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("trigger event");
                act = ACT.TALKING;
                runner.StartDialogue();
                this.PlayAudio();
                //player.isTalking(this);
            }
        }
    }
}
