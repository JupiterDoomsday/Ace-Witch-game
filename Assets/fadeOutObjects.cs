using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class fadeOutObjects : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Tilemap[] fadeOut; //number of sprite in the scene
    [SerializeField] private Tilemap[] fadeIn;
    [SerializeField] private SpriteRenderer[] fadeOutprops; //number of sprite in the scene
    [SerializeField] private SpriteRenderer[] fadeInprops; //number of sprite in the scene
    [SerializeField] GameObject FadeInSwitch;
    private float curFOA = 1f;
    private float curFIA = 0f;
    private float fadeOutVal= 0f;
    private float fadeInVal = 1f;
    public bool isReverse = false;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            try
            {
                StartCoroutine(AlterAlpha(curFOA, curFIA, fadeInVal, fadeOutVal, fadeTime, isReverse));
            }
            catch (System.Exception) { Debug.Log("No Renders is found"); }
        }
    }

    private void OnDisable()
    {
        FadeInSwitch.SetActive(true);
        StopAllCoroutines();
    }

    private IEnumerator AlterAlpha(float curFOA, float curFIA, float reqFIAlpha, float reqFOAlpha, float fadeDelay, bool isFadeReverse) 
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeDelay)
        {
            float newFadeOutAlpha = Mathf.Lerp(curFOA, reqFOAlpha, t);
            float newFadeInAlpha = Mathf.Lerp(curFIA, reqFIAlpha, t);
            if (isFadeReverse)
            {
                float temp = newFadeInAlpha;
                newFadeInAlpha = newFadeOutAlpha;
                newFadeOutAlpha = temp;
            }

            int curPropIndex = 0;
            int propSize = fadeOutprops.Length;
            for (int i = 0; i < fadeOut.Length; i++)
            {
                Tilemap curFadeOut = fadeOut[i];
                Color tmp = curFadeOut.color;
                tmp.a = newFadeOutAlpha;
                curFadeOut.color = tmp;
                if (curPropIndex < propSize)
                {
                    SpriteRenderer prop = fadeOutprops[curPropIndex];
                    tmp = prop.color;
                    tmp.a = newFadeOutAlpha;
                    prop.color = tmp;
                    curPropIndex++;
                }
            }

            curPropIndex = 0;
            propSize = fadeInprops.Length;
            for (int i = 0; i < fadeIn.Length; i++)
            {
                Tilemap curFadeIn = fadeIn[i];
                Color tmp = curFadeIn.color;
                tmp.a = newFadeInAlpha;
                curFadeIn.color = tmp;

                if (curPropIndex < propSize)
                {
                    SpriteRenderer prop = fadeInprops[curPropIndex];
                    tmp = prop.color;
                    tmp.a = newFadeInAlpha;
                    prop.color = tmp;
                    curPropIndex++;
                }
            }

            yield return null;
        }
        gameObject.SetActive(false);
    }


}
