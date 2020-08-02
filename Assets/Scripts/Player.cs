using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ACT
{
    IDLE,
    WALKING,
    FLYING,
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
public class Player : MonoBehaviour
{
    public ACT act;
    public DIRECTION dir;
    public int speed;
    public int sittingTime;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate() {
        if (Input.anyKeyDown) {
            handleInput();
        }
    }

    // Update is called once per frame
    void handleInput() {
        //check if player is walking
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Verticle");
        switch (act) {
            case ACT.IDLE:
                if (axisX >= -1 && axisX <0)
                    dir = DIRECTION.LEFT;
                else if(axisX <= 1 && axisX > 0)
                    dir = DIRECTION.RIGHT;
                else if (axisY<= 1 && axisY>0)
                    dir = DIRECTION.UP;
                else if (axisY >= -1 && axisY < 0)
                    dir = DIRECTION.DOWN;
                if (axisX ==-1 || axisX==1 || axisY==1 || axisY==-1)
                    act= ACT.WALKING;
                //check if player is talking
                else if (Input.GetKeyDown("X"))
                    act = ACT.TALKING;
                break;

            case ACT.WALKING:
                if (axisX == -1) 
                    dir = DIRECTION.LEFT;
                else if (axisX == 1)
                   dir = DIRECTION.RIGHT;
                else if (axisY == -1)
                    dir = DIRECTION.DOWN;
                else if (axisY == 1)
                    dir = DIRECTION.UP;
                else
                    act = ACT.IDLE;  
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
        
}