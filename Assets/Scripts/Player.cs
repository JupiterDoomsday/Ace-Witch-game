using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public enum ACT
{
    IDLE,
    WALKING,
    MENU,
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
public class  Player : MonoBehaviour, IDataPersistence
{
    public ACT act;
    public DIRECTION dir;
    public Inventory invo;
    public Sprite profile;
    public Dictionary<string,Sprite> expressions;
    public Sprite[] sitting = new Sprite[4];
    public Sprite[] dirSprites= new Sprite[4];
    public SpriteRenderer spriteRender;
    public int speed=4;
    public bool isSitting = false;
    public Animator player_animator;
    public Rigidbody2D rgb2d;
    private DIRECTION sittingDir;

    private void Awake()
    {
        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    
    //this resets the player state to idle
    public void setIdle()
    {
        act = ACT.IDLE;
    }

    //this function gets called when the walking animation breaks and we're at the idel state
    //the play is suposed to be facing the last direction they're facing
    //set it up in walking state so that this gets called once
    //instead of being called each frame at the idle state
    public void setDirectionSprite()
    {
        if(isSitting)
        {
            switch (dir)
            {
                case DIRECTION.UP:
                    spriteRender.sprite = sitting[3];
                    break;
                case DIRECTION.LEFT:
                    spriteRender.sprite = sitting[0];
                    break;
                case DIRECTION.RIGHT:
                    spriteRender.sprite = sitting[1];
                    break;
                case DIRECTION.DOWN:
                    spriteRender.sprite = sitting[2];
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case DIRECTION.LEFT:
                    spriteRender.sprite = dirSprites[0];
                    break;
                case DIRECTION.RIGHT:
                    spriteRender.sprite = dirSprites[1];
                    break;
                case DIRECTION.UP:
                    spriteRender.sprite = dirSprites[2];
                    break;
                case DIRECTION.DOWN:
                    spriteRender.sprite = dirSprites[3];
                    break;
            }
        }
    }

    public void SetDirection(string direction)
    {
        switch (direction)
        {
            case "UP":
                dir = DIRECTION.UP;
                break;

            case "LEFT":
                dir = DIRECTION.LEFT;
                break;

            case "RIGHT":
                dir = DIRECTION.RIGHT;
                break;

            case "DOWN":
                dir = DIRECTION.DOWN;
                break;
        }
    }

    public void SetSitting(DIRECTION chairDir)
    {
        isSitting = true;
        sittingDir = chairDir;
    }

    public void setTalking()
    {
        act = ACT.TALKING;
    }

    public void IsWalking()
    {
        isSitting = false;
    }

    public bool IsSitting()
    {
        return isSitting;
    }

    public Sprite getExpression(string exp)
    {
        if (expressions.ContainsKey(exp))
            return expressions[exp];
        else
            return null;
    }

    public void PlayAnimation(string aniState)
    {
        player_animator.SetTrigger(aniState);
    }
    /*  --------------------------------------------------------------------------------
     * | This is code to set up a "serilizable" dictionary using serializable lists     |
     *  --------------------------------------------------------------------------------
     */
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

    public bool CanMoveFromSeat()
    {
        return dir == sittingDir;
    }

    public void OnAfterDeserialize()
    {
        expressions = new Dictionary<string, Sprite>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("load data");
        this.transform.position = data.playerPosition;
        this.dir = DIRECTION.UP;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}
