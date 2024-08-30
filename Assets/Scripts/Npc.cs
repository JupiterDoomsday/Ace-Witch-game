using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using state;
using UnityEngine.Playables;
//this is an enum that represents the relationship of an NPC and
public enum SOCIAL_STANDING
{
    COMARADE,
    AQUINTANCE,
    FAMILY,
    BEST_FIREND,
};
public enum NPC_TYPE
{
    SHOPKEEPER,
    NPC,
    JATT,
    ENEMIE
};
public class Npc : MonoBehaviour
{
    public ACT act;
    public NPC_TYPE type;
    public SOCIAL_STANDING player_relationship;
    public DIRECTION dir;

    public Dictionary<string,Sprite> expressions;
    public Sprite Profile;
    private bool dirMoves = false;
    public int speed;
    [SerializeField]
    private Sprite[] defaultSprites = new Sprite[1];
    [SerializeField]
    private PlayableAsset introCutscene;
    public string Name;
    public string startNode;
    //public DialogueRunner runner;
    public Animator actor_animator;

    //[YarnCommand("facePlayer")]
    //alows us to change where the npc is facing when talking to the player

    private void Start()
    {
        act = ACT.IDLE;
        //render.sprite = defaultSprites[0];
        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    public void resetNPCDir(Player marji)
    {
        switch (marji.dir)
        {
            case (DIRECTION.UP):
                this.dir = DIRECTION.DOWN;
                break;
            case (DIRECTION.DOWN):
                this.dir = DIRECTION.UP;
                break;
            case (DIRECTION.LEFT):
                this.dir = DIRECTION.RIGHT;
                break;
            case (DIRECTION.RIGHT):
                this.dir = DIRECTION.LEFT;
                break;

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
    public void SetNPCDirection(DIRECTION direction)
    {
        this.dir = direction;
    }
    public bool corespondingDir(Player marji)
    {
        if (dirMoves)
        {
            resetNPCDir(marji);
            return true;
        }
            
        switch (this.dir)
        {
            case (DIRECTION.UP):
                return (marji.dir == DIRECTION.DOWN);
            case (DIRECTION.DOWN):
                return marji.dir == DIRECTION.UP;
            case (DIRECTION.LEFT):
                return marji.dir == DIRECTION.RIGHT;
            case (DIRECTION.RIGHT):
                return this.dir == DIRECTION.LEFT;
            default:
                return false;
        }
    }
    public string speak()
    {
        return startNode;
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


    public virtual void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in expressions)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public virtual void OnAfterDeserialize()
    {
        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    public Sprite ChangeDefaultSprite(int i)
    {
        if (i >= defaultSprites.Length)
        {
            Debug.Log("int is too big for sprite");
            return null;
        }
        Debug.Log("changing render sprite");

        return defaultSprites[i];
    }
    public PlayableAsset GetCutscene()
    {
        return introCutscene;
    }

    public void PlayNPCAnimation(string anim)
    {
        actor_animator.SetTrigger(anim);
    }


}
