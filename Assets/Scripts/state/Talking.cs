using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using state;
using UnityEngine.Playables;
using System;

public class Talking : MonoBehaviour, PlayerState
{
    public DialogueRunner dialogueRunner;
    public CustomLineView lineViewer;
    public Image left;
    public Image right;
    private Player player;
    [SerializeField]
    private GameObject NPCContainer;
    [SerializeField]
    private GameObject playerObject;
    public AudioSource audioSource;
    [SerializeField]
    private GameObject textButton;
    [SerializeField]
    private PlayableDirector timeline;
    [SerializeField]
    private PlayableAsset[] cutscenes;
    [SerializeField]
    private GameObject GameUI;
    public Owl jatt;
    public bool isCutsceneAndWait = false;
    public AudioClip[] textSounds;
    private Dictionary<string, Npc> actors;

    //private Npc curActor;
    public void Start()
    {
        //grab all of the npcs contained in the array and add them to our
        actors = new Dictionary<string, Npc>();
        Npc[] npcs = NPCContainer.GetComponentsInChildren<Npc>();
        foreach (Npc child in npcs)
        {
            actors.Add(child.Name.ToLower(), child);
        }
        player = playerObject.GetComponent<Player>();
    }

    [YarnCommand("Save")]
    public void SaveGame()
    {
        jatt.Save();
    }
    [YarnCommand("SetDirection")]
    public void SetSpriteDirection(string actor, string direction)
    {
        Npc npc;
        if (  actors.TryGetValue(actor.ToLower(), out npc))
        {
            npc.SetDirection(direction);
        }
        else
        {
            player.SetDirection(direction);
            player.setDirectionSprite();
        }
    }
    //this is a coroutine for yarnspinner to run
    [YarnCommand("moveActor")]
    public static IEnumerator MoveActor(GameObject gameObject, string direction, int amt, float speed)
    {

        Npc actor = gameObject.GetComponent<Npc>();
        Player play = gameObject.GetComponent<Player>();
        if (actor)
        {
            actor.SetDirection(direction);
        }
        else
        {
            play.SetDirection(direction);
        }
        Vector3 moveDir = new Vector3(0, 0, 0);
        Animator actorAnimator = gameObject.GetComponent<Animator>();
        actorAnimator.enabled = true;
        switch (direction)
        {
            case "UP":
                moveDir.y = -1;
                actorAnimator.SetInteger("y", 1);
                break;
            case "DOWN":
                moveDir.y = 1;
                actorAnimator.SetInteger("y", -1);
                break;
            case "LEFT":
                moveDir.x = -1;
                actorAnimator.SetInteger("x", -1);
                break;
            case "RIGHT":
                moveDir.x = 1;
                actorAnimator.SetInteger("x", 1);
                break;

        }
        Vector3 startPos = gameObject.transform.position;
        Vector3 finalPos = startPos + (moveDir * amt);
        float inTime = speed * amt;
        float elapsedTime = 0;
        while (elapsedTime < inTime)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / inTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        actorAnimator.SetInteger("y", 0);
        actorAnimator.SetInteger("x", 0);
        if(play)
        {
            actorAnimator.enabled = false;
        }

    }
        //this is a custome action in unity that creates the
        //talking settings for the
    [YarnCommand("PlayAndWaitCutscene")]
    public void PlayAndWaitCutscene(int index, bool hidden)
    {
        if (index >= cutscenes.Length)
            return;
        if (hidden)
        {
            GameUI.SetActive(false);
        }
        isCutsceneAndWait = true;
        player.player_animator.enabled = true;
        StartCoroutine(PlayCutscene(index, hidden));
    }

    [YarnCommand("playAndWait")]
    public void PlayAndWait(GameObject gameObject, string anim)
    {
        Animator actorAnimator = gameObject.GetComponent<Animator>();
        if(actorAnimator)
        {
            isCutsceneAndWait = true;
            GameUI.SetActive(false);
            int hash = Animator.StringToHash("Base Layer." + anim);
            StartCoroutine(PlayAnimationAndWait(actorAnimator, hash));
        }
    }

    IEnumerator PlayAnimationAndWait(Animator actorAnimator, int hash)
    {
        actorAnimator.Play(hash);
        //a flag that lets us know to ignore the "default" state at the start of playing an animtion clip
        bool hitFrameZero = true; 
        while (isCutsceneAndWait)
        {
            AnimatorStateInfo stateInfo = actorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash == hash || hitFrameZero)
            {
                //shut down this flag so we write it to false once
                if (hitFrameZero) 
                    hitFrameZero = false;
                yield return null;
            }
            else
            {
                GameUI.SetActive(true);
                isCutsceneAndWait = false;
                player.player_animator.enabled = false;
                yield break;
            }
        }
    }

  
    public void PlayAnimation(GameObject gameObject, string animation)
    {
        Animator actorAnimator = gameObject.GetComponent<Animator>();
        if(actorAnimator)
        {
            actorAnimator.enabled=true;
            actorAnimator.Play(animation);
        }
    }

    [YarnCommand("cutscene")]
    IEnumerator PlayCutscene(int i, bool hidden)
    {
        if (i >= cutscenes.Length)
            yield break;
        timeline.playableAsset = cutscenes[i];
        timeline.Play();
        while (isCutsceneAndWait)
        {
            if (timeline.state == PlayState.Playing)
                yield return null;
            else
            {
                if(hidden)
                {
                    GameUI.SetActive(true);
                }
                isCutsceneAndWait = false;
            }
        }
    }

    [YarnCommand("profile")]
    public void ShowProfile(string actor, string pos, string emote)
    {
        Sprite witch = null;
        Image target = right;
        bool hasSprite = actors.ContainsKey(actor);
        if (hasSprite == false)
        {
            if(actor.Equals("marji"))
            {
               witch = player.getExpression(emote);
            }
            else
                Debug.Log("ERROR actor doesn't exsist");
        }
        else
        {
            witch = actors[actor].getExpression(emote);
        }

        if (witch == null)
            return;
        //check the position you want the player to be in
        if (pos.Equals("left"))
        {
            target = left;
        }
        target.sprite = witch;
        target.SetNativeSize();
        target.enabled = true;
    }

    [YarnCommand("sound")]
    public void PlayAudio(string actor)
    {
        switch(actor)
        {
            case "jatt":
                audioSource.clip = textSounds[1];
                break;
            case "dani":
                audioSource.clip = textSounds[2];
                break;
            default:
                audioSource.clip = textSounds[0];
                break;
        }
    }


    // Start is called before the first frame update
    public void handleInput(StateMachine player)
    {
        if (isCutsceneAndWait)
            return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            textButton.SetActive(false);
            lineViewer.UserRequestedViewAdvancement();
        }

    }
    public void UpdateState(StateMachine player)
    {
        if (isCutsceneAndWait)
            return;

        if (dialogueRunner.IsDialogueRunning == false)
        {
            OnExit(player);
        }

    }
    public void OnExit(StateMachine mach)
    {
        left.sprite = null;
        right.sprite = null;
        audioSource.clip = textSounds[0];
        mach.player.setIdle();
        mach.UpdateAct();
    }


}
