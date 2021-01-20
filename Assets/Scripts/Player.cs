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

    static Walking walkingState;
    static Idle idleState;
    //public Talking talkingState;
    public Sprite profile;
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
    void Start()
    {
        idleState = new Idle();
        walkingState = new Walking();
        curState = idleState;
        //x = -7.98f;
        //y = -2.03f;

    }
    //handle the change of states
    void Update() {
        curState.handleInput(this);
        curState.UpdateState(this);
    }
    //this function handles 2D character movement based on the direction sate
    public void moveOriginalFunct()
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
        transform.position = new Vector2(x, y);
    }
    //this function handles 2D character movement based on the direction sate
    public void move(){
        float dirX = 0;
        float dirY = 0;
        Vector3 offset= new Vector3(transform.position.x, transform.position.y + -.6f);
        switch (dir) {
            case DIRECTION.LEFT:
                //x += -1 * Time.deltaTime * speed;
                dirX = -1f * Time.deltaTime * speed;
                break;
            case DIRECTION.RIGHT:
                //x += 1 * Time.deltaTime * speed;
                dirX = 1f * Time.deltaTime * speed;
                break;
            case DIRECTION.UP:
                dirY = 1f * Time.deltaTime * speed;
                break;
            case DIRECTION.DOWN:
                offset = new Vector3(transform.position.x, transform.position.y + -2);
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
                //curState = null;
                break;
            case ACT.SITTING:
                break;
        }
    }
    

}