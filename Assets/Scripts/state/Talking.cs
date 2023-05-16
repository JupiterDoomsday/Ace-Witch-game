using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using state;

public class Talking : MonoBehaviour, PlayerState
{
    public DialogueRunner dialogueRunner;
    public LineView lineViewer;
    public Image left;
    public Image right;
    public Player player;
    public GameObject NPCContainer;
    public AudioSource audioSource;
    public Owl jatt;
    private Dictionary<string, Npc> actors;
    //private Npc curActor;
    public void Start()
    {
        //grab all of the npcs contained in the array and add them to our
        actors = new Dictionary<string, Npc>();
        Npc[] npcs = NPCContainer.GetComponentsInChildren<Npc>();
        foreach (Npc child in npcs)
        {
            actors.Add(child.Name.ToLower(), child);
        }
        //dialogueRunner.AddCommandHandler("profile", ShowProfile);
        //dialogueRunner.AddCommandHandler("sound", PlayAudio);
        //dialogueRunner.AddCommandHandler("Save", SaveGame);
    }

    [YarnCommand("Save")]
    public void SaveGame()
    {
        jatt.Save();
    }
    
    //this is a custome action in unity that creates the
    //talking settings for the
    [YarnCommand("profile")]
    public void ShowProfile(string actor, string pos, string emote)
    {
        Sprite witch = null;
        Image target = right;
        bool hasSprite = actors.ContainsKey(actor);
        if (hasSprite == false)
        {
            if(actor.Equals("marji"))
            {
               witch = player.getExpression(emote);
            }
            else
                Debug.Log("ERROR actor doesn't exsist");
        }
        else
        {
            witch = actors[actor].getExpression(emote);
        }

        if (witch == null)
            return;
        //check the position you want the player to be in
        if (pos.Equals("left"))
        {
            target = left;
        }
        target.sprite = witch;
        target.SetNativeSize();
        target.enabled = true;
    }

    [YarnCommand("sound")]
    public void PlayAudio(string actor)
    {
        Debug.Log("Finding soundFX");
        bool hasSprite = actors.ContainsKey(actor);
        if (hasSprite == false)
            return;
        Npc npc = actors[actor];
        if (npc.audioSFX == null)
            return;
        audioSource.PlayOneShot(npc.audioSFX, .9f);
    }


    // Start is called before the first frame update
    public void handleInput(StateMachine player)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            lineViewer.UserRequestedViewAdvancement();
        }

    }
    public void UpdateState(StateMachine player)
    {
        if (dialogueRunner.IsDialogueRunning == false)
        {
            OnExit(player);
        }

    }
    public void OnExit(StateMachine player)
    {
        player.UpdateAct();
    }


}
