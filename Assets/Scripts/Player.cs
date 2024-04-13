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
    public Sprite[] sitting = new Sprite[3];
    public Sprite[] dirSprites= new Sprite[4];
    public SpriteRenderer spriteRender;
    public int speed=4;
    public int sittingTime;
    public Animator player_animator;
    public Rigidbody2D rgb2d;

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

    public void SetSitting()
    {
        act = ACT.SITTING;
        player_animator.SetTrigger("sit");
    }

    public void setTalking()
    {
        act = ACT.TALKING;
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
