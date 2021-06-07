using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAnimations : MonoBehaviour
{
    public float transitiontime = 1f;
    public Animator transition;
    public string enterAnimation;
    public string exitAnimation;
    public int width;
    public int height;
    public int x_cord;
    public int y_cord;
    public RectTransform pos;

    public void loadMenu()

    {
        StartCoroutine(PopUpEnter());
    }
    public void exitMenu()

    {
        StartCoroutine(MenuExit());
    }
    IEnumerator PopUpEnter()
    {
        transition.enabled = true;
        transition.SetTrigger(enterAnimation);
        yield return new WaitForSeconds(transitiontime);
        transition.enabled = false;
        pos.anchoredPosition = new Vector2(x_cord, y_cord);
        

    }

    IEnumerator MenuExit()
    {
        transition.enabled = true;
        transition.SetTrigger("exitAnimation");
        yield return new WaitForSeconds(.4f);
        transition.SetTrigger("done");
        transition.enabled = false;

    }
}
