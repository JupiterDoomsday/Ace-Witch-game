using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ACT
{
    IDLE,
    WALKING,
    FLYING,
    INSPECTING,
    TALKING,
    SITTING,
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
    public Inventory invo;
    static Walking walkingState;
    static Idle idleState;
    public Talking talkingState;
    public Sprite profile;
    public Dictionary<string,Sprite> expressions;
    public Sprite[] sitting = new Sprite[3];
    public Sprite[] dirSprites= new Sprite[4];
    PlayerState curState = null;
    public SpriteRenderer spriteRender;
    public int speed=4;
    public int sittingTime;
    public Animator player_animator;
    public Rigidbody2D rgb2d;
    public float x;
    public float y;


    // Start is called before the first frame update
    private void Awake()
    {
        invo = new Inventory();
    }
    void Start()
    {
        idleState = new Idle();

        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);

        walkingState = new Walking();
        curState = idleState;
        talkingState.player = this;
        //x = -7.98f;
        //y = -2.03f;

    }
    //handle the change of states
    void Update() {
        curState.handleInput(this);
        curState.UpdateState(this);
    }
    //this function handles 2D character movement based on the direction sate
    public void move()
    {
        
        switch (dir)
        {
            case DIRECTION.LEFT:
                x += -1 * Time.deltaTime * speed;
                break;
            case DIRECTION.RIGHT:
                x += 1 * Time.deltaTime * speed;
                break;
            case DIRECTION.UP:
                y += 1 * Time.deltaTime * speed;
                break;
            case DIRECTION.DOWN:
                y += -1 * Time.deltaTime * speed; 
                break;
        }
        rgb2d.MovePosition(new Vector2(x, y));
    }
    //this function handles 2D character movement based on the direction sate
    public void movetemp(){
        float dirX = 0;
        float dirY = 0;
        Vector3 offset = Vector3.zero;
        switch (dir) {
            case DIRECTION.LEFT:
                //x += -1 * Time.deltaTime * speed;
                offset = new Vector3(transform.position.x -.625f, transform.position.y);
                dirX = -1f * Time.deltaTime * speed;
                break;
            case DIRECTION.RIGHT:
                //x += 1 * Time.deltaTime * speed;
                offset = new Vector3(transform.position.x + .625f, transform.position.y);
                dirX = 1f * Time.deltaTime * speed;
                break;
            case DIRECTION.UP:
                offset = new Vector3(transform.position.x, transform.position.y + 1.69f);
                dirY = 1f * Time.deltaTime * speed;
                break;
            case DIRECTION.DOWN:
                offset = new Vector3(transform.position.x, transform.position.y - 1.69f);
                dirY = -1f * Time.deltaTime * speed;
                break;
        }
        //generate the ray where we walk to
        Vector3 moveDir = new Vector3(dirX , dirY).normalized;
        //check if you can move
        if (checkRayHit('n', moveDir,offset))
            return;
        //check if we can ONLY move horizontally
        if (checkRayHit('x', moveDir, offset))
            return;
        //check if we can ONLY move Vertically
        checkRayHit('y', moveDir, offset);

    }

    //this allows us to edit the vector to check and modify our viewing ray several times without repetitive code
    private bool checkRayHit(char c,Vector3 moveDir,Vector3 pos)
    {
        Vector3 targetDir;
        switch (c)
        {
            case 'x':
                targetDir = new Vector3(moveDir.x, 0f).normalized;

                break;
            case 'y':
                targetDir = new Vector3(0f,moveDir.y).normalized;
                break;
            default:
                targetDir = moveDir;
                break;
        }
        RaycastHit2D ray = Physics2D.Raycast(pos, targetDir, Time.deltaTime * speed);
        if (ray.collider == null || ray.collider.isTrigger)
        {
            transform.position += targetDir * Time.deltaTime * speed;
            return true;
        }
        return false;      
    }
    //is the bones for handeling the talking event
    public void isTalking()
    {
        act = ACT.TALKING;
        curState.OnExit(this);
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
            case ACT.SITTING:
                break;
        }
    }
    public void ExitStateRightAway()
    {
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