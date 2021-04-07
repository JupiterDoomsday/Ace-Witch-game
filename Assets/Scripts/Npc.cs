using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
//this is an enum that represents the relationship of an NPC and
public enum SOCIAL_STANDING
{
    COMARADE,
    FAMILY,
    BEST_FIREND,
    AQUINTANCE,
    RIVAL
};

public class Npc : MonoBehaviour
{
    public ACT act;
    public SOCIAL_STANDING player_relationship;
    public DIRECTION dir;

    public Dictionary<string,Sprite> expressions;
    public Sprite Profile;
    public string Name;
    public  string startNode;
    public YarnProgram startScript;
    public DialogueRunner runner;
    public AudioClip audioSFX;

    //[YarnCommand("facePlayer")]
    //alows us to change where the npc is facing when talking to the player

    private void Start()
    {
        runner.Add(startScript);
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
    public bool corespondingDir(Player marji)
    {
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

    private void OnTriggerStay2D(Collider2D ot)
    {
        if (ot.tag.Equals("Player") && act == ACT.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Player player = ot.GetComponentInParent<Player>();
                if (this.corespondingDir(player))
                {
                    act = ACT.TALKING;
                    runner.StartDialogue(startNode);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        act = ACT.IDLE;
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
