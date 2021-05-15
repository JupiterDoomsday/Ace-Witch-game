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
    public DialogueUI diaUI;
    public Image left;
    public Image right;
    public Player player;
    public GameObject NPCContainer;
    public AudioSource audioSource;
    private Dictionary<string, Npc> actors;
    public void Start()
    {
        //grab all of the npcs contained in the array and add them to our
        actors = new Dictionary<string, Npc>();
        Npc[] npcs = NPCContainer.GetComponentsInChildren<Npc>();
        foreach(Npc child in npcs)
        {
            actors.Add(child.Name,child);
        }
        dialogueRunner.AddCommandHandler("profile", ShowProfile);
        dialogueRunner.AddCommandHandler("sound", PlayAudio);
    }
    
    //this is a custome action in unity that creates the
    //talking settings for the
    [YarnCommand("profile")]
    public void ShowProfile(string [] profile)
    {
        Sprite witch = null;
        Image target = right;
        bool hasSprite = actors.ContainsKey(profile[0]);
        if (hasSprite == false)
        {
            if(profile[0].Equals("marji"))
            {
                Debug.Log("printing out marji");
                if (profile.Length == 3)
                {
                    Debug.Log("Array is 3 printing out " + profile[2]);
                    witch = player.getExpression(profile[2]);
                }  
                else
                    witch = player.profile;
            }
            else
                Debug.Log("ERROR actor doesn't exsist");
        }
        else
        {
            //this is bad styling I know but its 12AM and I'm tired UwU
            if (profile.Length == 3)
            {
                witch = actors[profile[0]].getExpression(profile[2]);
            }
            else
                witch = actors[(profile[0])].Profile;
        }

        if (witch == null)
            return;
        //check the position you want the player to be in
        if (profile[1].Equals("left"))
        {
            target = left;
        }
        target.sprite = witch;
        target.SetNativeSize();
        target.enabled = true;
    }
    [YarnCommand("sound")]
    public void PlayAudio(string[] str)
    {
        Debug.Log("Finding soundFX");
        bool hasSprite = actors.ContainsKey(str[0]);
        if (hasSprite == false)
            return;
        Npc npc = actors[(str[0])];
        if (npc.audioSFX == null)
            return;
        audioSource.PlayOneShot(npc.audioSFX, .9f);
    }


    // Start is called before the first frame update
    public void handleInput(Player player)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Debug.Log("Pressing x to continue");
            diaUI.MarkLineComplete();
            //diaUI.textSpeed = .025f;

        }

    }
    public void UpdateState(Player player)
    {
        if (dialogueRunner.IsDialogueRunning == false)
        {
            OnExit(player);
        }

    }
    public void OnExit(Player player)
    {
        player.act = ACT.IDLE;
        player.UpdateAct();
    }


}
