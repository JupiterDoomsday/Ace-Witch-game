using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public enum ACT
{
    IDLE,
    WALKING,
    FLYING,
    INSPECTING,
    TALKING,
    INTERACTING,
    SITTING,
    PAUSE,
    STIMMING,
    INCANTATION,
    COLLIDE,
    SNEAK,
};
public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
};
public class  Player : MonoBehaviour
{
    public ACT act;
    public DIRECTION dir;
    public MenuTransition menu;
    public GameObject canvas;
    public GameObject menu_panel;
    public Inventory invo;
    static Walking walkingState;
    static Idle idleState;
    static Interact interactState;
    public Talking talkingState;
    public Sprite profile;
    [SerializeField] private InventoryUI invoUI;
    public Dictionary<string,Sprite> expressions;
    public Sprite[] sitting = new Sprite[3];
    public Sprite[] dirSprites= new Sprite[4];
    private PlayerState curState = null;
    public SpriteRenderer spriteRender;
    public int speed=4;
    public int sittingTime;
    public Animator player_animator;
    public Rigidbody2D rgb2d;

    // Start is called before the first frame update
    private void Awake()
    {
        invo = new Inventory();
        invoUI.setInvo(invo);
        idleState = new Idle();
        idleState.menu = this.menu;
        idleState.Canvas = this.canvas;
        idleState.menu_panel = this.menu_panel;

        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);

        walkingState = new Walking();
        interactState = new Interact();
        curState = idleState;
        talkingState.player = this;
    }
    private void FixedUpdate()
    {
        curState.UpdateState(this);
    }
    //handle the change of states
    void Update() {
        curState.handleInput(this);
    }

    //is the bones for handeling the talking event
    public void isTalking(Npc npc)
    {
        act = ACT.TALKING;
        curState.OnExit(this);
        npc.speak();
    }
    //this resets the player state to idle
    public void setIdle()
    {
        act = ACT.IDLE;
      
    }
    /*
     * this function allows us to change the curAct pointer
     * to update to its respective PlayerState object 
     * after we chang the Players ACT Enum
     */
    public void UpdateAct()
    {
        switch (act)
        {
            case ACT.IDLE:
                curState = idleState;
                break;
            case ACT.WALKING:
                curState = walkingState;
                player_animator.enabled = true;
                break;
            case ACT.TALKING:
                curState = talkingState;
                break;
            case ACT.INTERACTING:
                curState = interactState;
                break;
            case ACT.SITTING:
                break;
        }
    }
    public void handleInputNow()
    {
        curState.handleInput(this);
    }
    public void ExitStateRightAway()
    {
        this.act = ACT.IDLE;
        curState.OnExit(this);
    }

    public Sprite getExpression(string exp)
    {
        if (expressions.ContainsKey(exp))
            return expressions[exp];
        else
            return null;
    }
    public List<string> _keys = new List<string>();
    public List<Sprite> _values = new List<Sprite>();
    private object idelState;

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in expressions)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        expressions = new Dictionary<string, Sprite>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }
}