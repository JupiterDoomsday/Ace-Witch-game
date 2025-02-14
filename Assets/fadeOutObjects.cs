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

    /// This is a function call if we need to Fade these objects on command instead of a collision trigger
    public void FadeOnCommand()
    {
        StartCoroutine(AlterAlpha(curFOA, curFIA, fadeInVal, fadeOutVal, fadeTime, isReverse));
    }

    private void OnDisable()
    {
        /*if(isReverse)
        {
            FadeIOItems(fadeOutprops, fadeOut, 1f); //change fadeOut props and Tilemap
            FadeIOItems(fadeInprops, fadeIn, 0f); // change fadeIn props and Tilemaps
        }
        else
        {
            FadeIOItems(fadeOutprops, fadeOut, 0f); //change fadeOut props and Tilemap
            FadeIOItems(fadeInprops, fadeIn, 1f); // change fadeIn props and Tilemaps
        }*/
        FadeInSwitch.SetActive(true);
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
            FadeIOItems(fadeOutprops, fadeOut, newFadeOutAlpha); //change fadeOut props and Tilemap
            FadeIOItems(fadeInprops, fadeIn, newFadeInAlpha); // change fadeIn props and Tilemaps
            yield return null;
        }
        gameObject.SetActive(false);
    }

    //helper function to fade in all of the items in a list
    private void FadeIOItems(SpriteRenderer[] props, Tilemap[] tiles, float newAlpha)
    {
        int curPropIndex = 0;
        int propSize = props.Length;
        for (int i = 0; i < tiles.Length; i++)
        {
            Tilemap curFadeOut = tiles[i];
            Color tmp = curFadeOut.color;
            tmp.a = newAlpha;
            curFadeOut.color = tmp;
            if (curPropIndex < propSize)
            {
                SpriteRenderer prop = props[curPropIndex];
                tmp = prop.color;
                tmp.a = newAlpha;
                prop.color = tmp;
                curPropIndex++;
            }
        }
        //If there is "more" props than tilemaps go through the rest of the array to alter it
        for(int i = curPropIndex; i < propSize; i++)
        {
            SpriteRenderer prop = props[curPropIndex];
            Color tmp = prop.color;
            tmp.a = newAlpha;
            prop.color = tmp;
            curPropIndex++;
        }
    }
}
