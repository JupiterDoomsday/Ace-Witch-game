using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayableAsset[] cutscenes;
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private GameObject GameUI;
    [SerializeField]
    private Animator playerAnimator;
    private bool isPlaying = false;

 
    public bool IsPlaying()
    {
        return isPlaying;
    }

    [YarnCommand("PlayAndWaitCutscene")]
    public Coroutine PlayAndWaitCutscene(int index, bool hidden)
    {
        if (index >= cutscenes.Length)
            return null;
        if (hidden)
        {
            GameUI.SetActive(false);
        }
        isPlaying = true;
        playerAnimator.enabled = true;
        return StartCoroutine(PlayCutscene(index, hidden));
    }

    IEnumerator PlayCutscene(int i, bool hidden)
    {
        if (i >= cutscenes.Length)
            yield break;
        director.playableAsset = cutscenes[i];
        director.Play();
        while (isPlaying)
        {
            if (director.state == PlayState.Playing)
                yield return null;
            else
            {
                if (hidden)
                {
                    GameUI.SetActive(true);
                }
                isPlaying = false;
            }
        }
    }

    //this play an animation
    [YarnCommand("playAndWait")]
    public Coroutine PlayAndWait(GameObject gameObject, string anim)
    {
        if (gameObject == null)
            return null;

        Animator actorAnimator = gameObject.GetComponent<Animator>();
        if (actorAnimator)
        {
            isPlaying = true;
            GameUI.SetActive(false);
            int hash = Animator.StringToHash("Base Layer." + anim);
            return StartCoroutine(PlayAnimationAndWait(actorAnimator, hash));
        }
        return null;
    }

    IEnumerator PlayAnimationAndWait(Animator actorAnimator, int hash)
    {
        actorAnimator.Play(hash);
        //a flag that lets us know to ignore the "default" state at the start of playing an animtion clip
        bool hitFrameZero = true;
        while (isPlaying)
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
                isPlaying = false;
                playerAnimator.enabled = false;
            }
        }
    }

}