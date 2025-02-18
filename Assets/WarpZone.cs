using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpZone : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private DIRECTION reqDir;
    [SerializeField]
    private Vector3 CameraPosition;
    [SerializeField]
    private Vector2 SetPosition;
    [SerializeField]
    private GameObject player;
    private Player marji;
    [SerializeField]
    private Animator crossFade;
    private bool isWarping = false;
    private bool isFadingIn = false;
    [SerializeField]
    private StateMachine mach;
    bool isPlaying = false;
    private int fadeInHash;
    private int fadeOutHash;
    // Start is called before the first frame update
    void Start()
    {
        marji = player.GetComponent<Player>();
        isWarping = false;
        isFadingIn = false;
        fadeInHash = Animator.StringToHash("Base Layer.fade_in_faster");
        fadeOutHash = Animator.StringToHash("Base Layer.fade_out_faster");
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && marji.dir == reqDir)
        {
            isPlaying = true;
            isWarping = true;
            mach.ExitStateRightAway();
            mach.enabled = false;
            StartCoroutine(PlayAndWait(crossFade, fadeInHash));
        }
    }

    public void FixedUpdate()
    {
        if (isPlaying)
            return;
        
        if (isWarping)
        {
            Debug.Log("Playing Fade out");
            mainCamera.transform.position = CameraPosition;
            player.transform.position = SetPosition;
            isWarping = false;
            isFadingIn = true;
            isPlaying = true;
            StartCoroutine(PlayAndWait(crossFade, fadeOutHash));
        }
        if( isFadingIn)
        {
            mach.enabled = true;
            isFadingIn = false;
        }
    }


    IEnumerator PlayAndWait(Animator actorAnimator, int hash)
    {
        actorAnimator.Play(hash);
        bool hitFrameZero = true;
        //a flag that lets us know to ignore the "default" state at the start of playing an animtion clip
        while (isPlaying)
        {
            AnimatorStateInfo stateInfo = actorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash == hash || hitFrameZero)
            {
                if (hitFrameZero)
                    hitFrameZero = false;
                yield return null;
                //shut down this flag so we write it to false once
            }
            else
            {
                isPlaying = false;
            }
        }
    }
}
