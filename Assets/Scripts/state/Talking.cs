using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using state;
using UnityEngine.Playables;
using System;

public class Talking : MonoBehaviour, PlayerState
{
    public DialogueRunner dialogueRunner;
    public CustomLineView lineViewer;
    private ACT ExitState = ACT.IDLE;
    public Image left;
    public Image right;
    private Player player;
    [SerializeField]
    private CutsceneManager cutsceneManager;
    [SerializeField]
    private GameObject NPCContainer;
    [SerializeField]
    private GameObject PictureContainer;
    [SerializeField]
    private GameObject playerObject;
    public AudioSource audioSource;
    [SerializeField]
    private GameObject textButton;
    [SerializeField]
    private GameObject GameUI;
    [SerializeField]    
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
        player = playerObject.GetComponent<Player>();
    }

    [YarnCommand("clearProfile")]
    public void ClearSprite(int side)
    {
        switch (side)
        {
            case 0:
                left.enabled = false;
                break;
            case 1:
                right.enabled = false;
                break;
            case 2:
                left.enabled = false;
                right.enabled = false;
                break;
            default:
                break;
        }
    }

    //Helper yarnspinner function to hide the dialouge UI
    [YarnCommand("HideUI")]
    public void HideUI()
    {
        GameUI.SetActive(false);
    }

    //Helper yarnspinner function to show the dialouge UI
    [YarnCommand("ShowUI")]
    public void ShowUI()
    {
        GameUI.SetActive(true);
    }

    [YarnCommand ("ExitState")]
    public void SetExitActState(string s)
    {
        switch(s)
        {
            case "sitting":
                ExitState = ACT.SITTING;
            break;
            case "flying":
            ExitState = ACT.FLYING;
            break;
            default:
            ExitState = ACT.IDLE;
            break;
        }
    }

    [YarnCommand("profile")]
    public void ShowProfile(string actor, string emote)
    {
        Sprite witch = null;
        Image target = right;
        if (actors.ContainsKey(actor))
        {
            witch = actors[actor].getExpression(emote);
            target = left;
        }
        else
        {
            if (actor.Equals("marji"))
            {
                witch = player.getExpression(emote);
            }
            else
                Debug.Log("ERROR actor doesn't exsist");
        }

        if (witch == null)
            return;
        target.sprite = witch;
        target.SetNativeSize();
        target.enabled = true;
    }

    [YarnCommand("showImage")]
    public void ShowImage(bool enable, bool hideProfile)
    {
        if (hideProfile)
            ClearSprite(2);
        PictureContainer.SetActive(enable);
    }


    // Start is called before the first frame update
    public void handleInput(StateMachine player)
    {
        if (cutsceneManager.IsPlaying())
            return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            textButton.SetActive(false);
            lineViewer.UserRequestedViewAdvancement();
        }

    }
    public void UpdateState(StateMachine player)
    {
        if (cutsceneManager.IsPlaying())
            return;

        if (dialogueRunner.IsDialogueRunning == false)
        {
            OnExit(player);
        }

    }
    public void OnExit(StateMachine mach)
    {
        left.sprite = null;
        right.sprite = null;
        mach.player.act = ExitState;
        mach.UpdateAct();
        ExitState = ACT.IDLE;
    }


}
