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
    PlayerState curState = null;
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
        this.handleInput();
        curState.UpdateState(this);
    }
   /* private void FixedUpdate()
    {
        curState.UpdateState(this);
    }*/

    // Update is called once per frame
    void handleInput() {
        //check if player is walking
        switch (act) {
            case ACT.IDLE:
                curState = idleState;
                curState.handleInput(this);
                break;

            case ACT.WALKING:
                curState = walkingState;
                curState.handleInput(this);
                break;
            case ACT.SITTING:
                //set marji to stim after a certain ammount of time has passed while sitting
                if (Time.time - sittingTime == 50)
                {
                    sittingTime = 0;
                    act = ACT.STIMMING;
                }
                if (Input.anyKeyDown)
                {
                    //if you press a key while sitting you get off
                    act = ACT.IDLE;
                    //play getting off chair animation
                }
                break;
            case ACT.STIMMING:
                if (Input.anyKeyDown)
                    act = ACT.SITTING;
                break;
        }

    }
    public void move()
    {
        switch (dir) {
            case DIRECTION.LEFT:
                x += -1 * Time.deltaTime * speed;
                //x += -0.125f;
                break;
            case DIRECTION.RIGHT:
                x += 1 * Time.deltaTime * speed;
                //x+=0.125f;
                break;
            case DIRECTION.UP:
                y += 1 * Time.deltaTime * speed;
                //y+= 0.125f;
                break;
            case DIRECTION.DOWN:
                y += -1 * Time.deltaTime * speed;
                //y+= -0.125f;
                break;
        }
        transform.position = new Vector2(x , y);

    }

}