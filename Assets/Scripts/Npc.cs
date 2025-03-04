using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using state;
using UnityEngine.Playables;
using CustomeInteractables;
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
public class Npc : Interactable
{
    public ACT act;
    public NPC_TYPE type;
    public SOCIAL_STANDING player_relationship;

    public Dictionary<string,Sprite> expressions;
    public Sprite Profile;
    public bool dirMoves = false;
    public int speed;
    [SerializeField]
    private Sprite[] defaultSprites = new Sprite[4];
    public string Name;
    public string startNode;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    //public DialogueRunner runner;
    //private Transform transform;
    private Animator actor_animator;

    //[YarnCommand("facePlayer")]
    //alows us to change where the npc is facing when talking to the player
    public Npc()
    {
        interacting = INTERACT_TYPE.TALK;
        act = ACT.IDLE;
    }
    private void Start()
    {
        //render.sprite = defaultSprites[0];
        spriteRenderer = GetComponent<SpriteRenderer>();
        expressions = new Dictionary<string, Sprite>();
        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    public void resetNPCDir(Player marji)
    {
        Sprite render = null;
        switch (marji.dir)
        {
            case (DIRECTION.UP):
                this.dir = DIRECTION.DOWN;
                render = ChangeDefaultSprite(2);
                break;
            case (DIRECTION.DOWN):
                this.dir = DIRECTION.UP;
                render = ChangeDefaultSprite(0);
                break;
            case (DIRECTION.LEFT):
                this.dir = DIRECTION.RIGHT;
                render = ChangeDefaultSprite(1);
                break;
            case (DIRECTION.RIGHT):
                this.dir = DIRECTION.LEFT;
                render = ChangeDefaultSprite(3);
                break;
        }

        if (render)
        {
            spriteRenderer.sprite = render;
        }
    }

    public void SetDirection(string direction)
    {
        Sprite render = null;
        switch (direction)
        {
            case "UP":
                dir = DIRECTION.UP;
                render = ChangeDefaultSprite(0);
                break;

            case "LEFT":
               dir = DIRECTION.LEFT;
                render = ChangeDefaultSprite(3);
                break;

            case "RIGHT":
                dir = DIRECTION.RIGHT;
                render = ChangeDefaultSprite(1);
                break;

            case "DOWN":
                dir = DIRECTION.DOWN;
                render = ChangeDefaultSprite(2);
                break;
        }

        if (render)
        {
            spriteRenderer.sprite = render;
        }
    }
    public void SetNPCDirection(DIRECTION direction)
    {
        this.dir = direction;
    }

    public override bool CorrespondingDirection(Player p)
    {
        if (dirMoves)
        {
            Debug.Log("IN Dir moves");
            resetNPCDir(p);
            return true;
        }
        return base.CorrespondingDirection(p);
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

    public void PlayNPCAnimation(string anim)
    {
        actor_animator.SetTrigger(anim);
    }

    [YarnCommand("SetActorDirection")]
    public void SetSpriteDirection(string direction)
    {
        SetDirection(direction);
    }

    [YarnCommand("moveActor")]
    public IEnumerator MoveActor(string direction, int amt, float speed)
    {
        SetDirection(direction);
        Vector3 moveDir = new Vector3(0, 0, 0);
        actor_animator.enabled = true;
        switch (direction)
        {
            case "UP":
                moveDir.y = -1;
                actor_animator.SetInteger("y", 1);
                break;
            case "DOWN":
                moveDir.y = 1;
                actor_animator.SetInteger("y", -1);
                break;
            case "LEFT":
                moveDir.x = -1;
                actor_animator.SetInteger("x", -1);
                break;
            case "RIGHT":
                moveDir.x = 1;
                actor_animator.SetInteger("x", 1);
                break;

        }
        Vector3 startPos = transform.position;
        Vector3 finalPos = startPos + (moveDir * amt);
        float inTime = (speed/30) * amt;
        float elapsedTime = 0;
        while (elapsedTime < inTime)
        {
            transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / inTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        actor_animator.SetInteger("y", 0);
        actor_animator.SetInteger("x", 0);
        actor_animator.enabled = false;
    }

    [YarnCommand("SetActorPosition")]
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

}
