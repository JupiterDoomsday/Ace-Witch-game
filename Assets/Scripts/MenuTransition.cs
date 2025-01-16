using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    public Animator transition;
    public RectTransform pos;
    public GameObject ui;
    public float transitiontime = 1f;
    public void loadMenu()

    {
        StartCoroutine(MenuEnter());
    }
    public void exitMenu()

    {
        StartCoroutine(MenuExit());
    }
    IEnumerator MenuEnter()
    {
        transition.enabled = true;
        transition.SetTrigger("menu_down");
        yield return new WaitForSeconds(transitiontime);
        pos.anchoredPosition = new Vector2(-250, 3);
        transition.enabled = false;

    }

    IEnumerator MenuExit() { 
        transition.enabled = true;
        transition.SetTrigger("menu_up");
        yield return new WaitForSeconds(transitiontime);
        transition.SetTrigger("done");
        transition.enabled = false;
        ui.SetActive(false);
    }
}
