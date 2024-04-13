using System.Collections;
using System.Collections.Generic;
using state;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    static Walking walkingState;
    static Idle idleState;
    public AudioSource footstepsSoundFX;
    [SerializeField] private QuestSystemUI QuestUI;
    [SerializeField] private InventoryUI invoUI;
    private AutomataStack automataStack;
    // public MenuTransition menu;
    static Interact interactState;
    public Talking talkingState;
    public Menu menuState;
    private PlayerState curState = null;
    public Player player;

    public void Start()
    {
        automataStack = new AutomataStack();
        walkingState = new Walking();
        interactState = new Interact();
        idleState = new Idle();
        curState = idleState;
        talkingState.player = player;
        invoUI.setInvo(player.invo);
    }

    //is the bones for handeling the talking event
    public void isTalking(Npc npc)
    {
        talkingState.dialogueRunner.StartDialogue(npc.speak());
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
        curState.UpdateState(this);
    }

    //handle the change of states
    void Update()
    {
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
            curState = walkingState;
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
