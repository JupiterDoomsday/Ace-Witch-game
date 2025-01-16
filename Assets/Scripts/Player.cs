using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
using Yarn.Unity;

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
    public Inventory invo = new();
    private int selectedItem = -1;
    public Sprite profile;
    public Dictionary<string,Sprite> expressions;
    public Sprite[] sitting = new Sprite[4];
    public Sprite[] dirSprites= new Sprite[4];
    public SpriteRenderer spriteRender;
    public int speed=4;
    public bool isSitting = false;
    public Animator player_animator;
    public Rigidbody2D rgb2d;

    private void Awake()
    {
        expressions = new Dictionary<string, Sprite>();
        invo.AddItem(0, 1);
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

    //These functions set up the player
    public void SetSelectedItem(int newItem)
    {
        selectedItem = newItem;
    }
    public void UnselectItem()
    {
        selectedItem = -1;
    }

    public int GetSelectedItem()
    {
        return selectedItem;
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
        dir = chairDir;
    }
    public void SetSitting(string s)
    {
        isSitting = true;
        switch (s.ToUpper())
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

    [YarnCommand("SetPosition")]
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    [YarnCommand("SetDirection")]
    public void SetSpriteDirection(string direction, bool isSitting)
    {
        if (isSitting)
        {
            SetSitting(direction);
        }
        else
        {
            SetDirection(direction);
        }
        setDirectionSprite();
    }

    private IEnumerator MoveActor(string direction, int amt, float speed)
    {
        SetDirection(direction);
        Vector3 moveDir = new Vector3(0, 0, 0);
        player_animator.enabled = true;
        switch (direction)
        {
            case "UP":
                moveDir.y = -1;
                player_animator.SetInteger("y", 1);
                break;
            case "DOWN":
                moveDir.y = 1;
                player_animator.SetInteger("y", -1);
                break;
            case "LEFT":
                moveDir.x = -1;
                player_animator.SetInteger("x", -1);
                break;
            case "RIGHT":
                moveDir.x = 1;
                player_animator.SetInteger("x", 1);
                break;

        }
        Vector3 startPos = transform.position;
        Vector3 finalPos = startPos + (moveDir * amt);
        float inTime = speed * amt;
        float elapsedTime = 0;
        while (elapsedTime < inTime)
        {
            transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / inTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        player_animator.SetInteger("y", 0);
        player_animator.SetInteger("x", 0);
        player_animator.enabled = false;
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
