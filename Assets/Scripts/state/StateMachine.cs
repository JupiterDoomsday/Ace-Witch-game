using System.Collections;
using System.Collections.Generic;
using state;
using UnityEngine;
using UnityEngine.Playables;

public class StateMachine : MonoBehaviour
{
    static Walking walkingState;
    static Idle idleState;
    [SerializeField]
    private PlayableDirector timeline;
    private QuestManager m_QManager;
    public AudioSource footstepsSoundFX;
    [SerializeField] private QuestSystemUI QuestUI;
    [SerializeField] private InventoryUI invoUI;
    //private AutomataStack automataStack;
    // public MenuTransition menu;
    static Interact interactState;
    public Talking talkingState;
    public Menu menuState;
    private Sitting sitState = null;
    private PlayerState curState = null;
    private bool isCutscenePlaying = false;
    public Player player;
    private ACT prevPlayerState;

    public void Start()
    {
        walkingState = new Walking();
        interactState = new Interact();
        idleState = new Idle();
        sitState = new Sitting();
        curState = idleState;
        invoUI.setInvo(player.invo);
    }

    //is the bones for handeling the talking event
    public void isTalking(Npc npc)
    {
        
        PlayableAsset introCutscene = npc.GetCutscene();
        if(introCutscene)
        {
            timeline.playableAsset = introCutscene;
            isCutscenePlaying = true;
            StartCoroutine(Cutscene(npc));
        }
        else
        {
            talkingState.dialogueRunner.StartDialogue(npc.speak());
        }
        
    }

    IEnumerator Cutscene(Npc npc)
    {
        timeline.Play();
        while (isCutscenePlaying)
        {
            if (timeline.state == PlayState.Playing)
            {
                yield return null;
            }
            else
            {
                isCutscenePlaying = false;
                talkingState.dialogueRunner.StartDialogue(npc.speak());
                yield break;
            }
        }
    }


    public void SetInteractingObject(GameObject curObject)
    {
        //curInteractable = curObject;
    }

    public GameObject GetInteractingObject()
    {
        //return curInteractable;
        return null;
    }

    private void FixedUpdate()
    {
        //we want to put a "hold on updating anyting if we have cutscenes;
        if (isCutscenePlaying)
            return;

        curState.UpdateState(this);
    }

    //handle the change of states
    void Update()
    {
        //we want to put a "hold on updating anyting if we have cutscenes;
        if (isCutscenePlaying)
            return;

        curState.handleInput(this);
    }

    public void handleInputNow()
    {

        curState.handleInput(this);
    }

    /*
     * this function allows us to change the curAct pointer
     * to update to its respective PlayerState object 
     * after we chang the Players ACT Enum
     */
    public void ExitStateRightAway()
    {
        player.act = ACT.IDLE;
        curState.OnExit(this);
    }
    public void SetPrevState(ACT act)
    {
        prevPlayerState = act;
    }
    public ACT getPlayerPrevState()
    {
        return prevPlayerState;
    }

    public void UpdateAct()
    {
        switch (player.act)
        {
            case ACT.IDLE:
                curState = idleState;
                break;
            case ACT.WALKING:
                curState = walkingState;
                player.player_animator.enabled = true;
                footstepsSoundFX.enabled = true;
                break;
            case ACT.TALKING:
                curState = talkingState;
                break;
            case ACT.INTERACTING:
                curState = interactState;
                break;
            case ACT.SITTING:
                curState = sitState;
                Debug.Log("Set state to Sitting");
                break;
            case ACT.MENU:
                menuState.canvas_objct.SetActive(true);
                menuState.menu_panel.SetActive(true);
                menuState.isAtMainMenu();
                menuState.state = MenuState.IDLE;
                menuState.transition.loadMenu();
                curState = menuState;
                break;
        }
    }
}
