using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class Talking : MonoBehaviour, PlayerState
{
    public DialogueRunner dialogueRunner;
    public DialogueUI diaUI;
    public Image left;
    public Image right;
    public Player player;
    public AudioSource audioSource;
    //[SerializeField]
    public List<Npc> actors;
    public void Start()
    {
        dialogueRunner.AddCommandHandler("profile", ShowProfile);
        dialogueRunner.AddCommandHandler("sound", PlayAudio);
    }
    

    private Npc findActor(string str)
    {
        //Debug.Log("Finding actor");
        foreach (Npc key in actors)
        {
            if (str.Equals(key.Name))
                return key;
        }
        return null;
    }
    //this is a custome action in unity that create the talking settings for the
    [YarnCommand("profile")]
    public void ShowProfile(string [] profile)
    {
        Debug.Log("Finding profile");
        Sprite witch = findActor(profile[0]).Profile;
        if (witch == null)
        {
            Debug.Log("ERROR actor doesn't exsist");
            return;
        }
        //if (pos.Equals("left"))
        left.sprite = witch;
        left.SetNativeSize();
        //left.IsActive();
        left.enabled = true;
        if (profile.Length == 2)
        {
            if((witch= findActor(profile[1]).Profile) != null)
            {
                right.sprite = witch;
                right.enabled = true; ;
            }
                
        }
        else
        {
            right.sprite = player.profile;
            right.enabled = true;
        }
        Debug.Log("Set it right");
        right.SetNativeSize();
    }
    [YarnCommand("sound")]
    public void PlayAudio(string[] str)
    {
        Debug.Log("Finding soundFX");
        Npc npc = findActor(str[0]);
        if (npc == null)
            return;
        if (npc.audio == null)
            return;

        audioSource.PlayOneShot(npc.audio, .9f);
    }


    // Start is called before the first frame update
    public void handleInput(Player player)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressing x to continue");
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
