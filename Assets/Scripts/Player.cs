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
    static Talking talkingState;
    public Sprite[] dirSprites= new Sprite[4];
    PlayerState curState = null;
    public SpriteRenderer spriteRender;
    public int speed=4;
    public int sittingTime;
    public float x;
    public float y;
    

    // Start is called before the first frame update
    void Start()
    {
        idleState = new Idle();
        walkingState = new Walking();
        curState = idleState;
        x = -7.98f;
        y = -2.03f;

    }
    void Update() {
        curState.handleInput(this);
        curState.UpdateState(this);
    }
    public void move(){
        switch (dir) {
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
        transform.position = new Vector2(x , y);

    }
    public void isTalking()
    {
        act = ACT.TALKING;
    }
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
                break;
            case ACT.TALKING:
                break;
            case ACT.SITTING:
                break;
        }
    }
    public void setPlayerDirectionSprite() {
        switch (dir)
        {
            case DIRECTION.LEFT:
                spriteRender.sprite = dirSprites[2];
                break;
            case DIRECTION.RIGHT:
                spriteRender.sprite = dirSprites[1];
                break;
            case DIRECTION.UP:
                spriteRender.sprite = dirSprites[0];
                break;
            case DIRECTION.DOWN:
                spriteRender.sprite = dirSprites[3];
                break;
        }
    }

}