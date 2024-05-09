using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    public Animator transition;
    public RectTransform pos;
    public GameObject ui;
    public GameObject dialouge_ui;
    public GameObject canvas;
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
        dialouge_ui.SetActive(false);
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
        canvas.SetActive(false);
        ui.SetActive(false);
        dialouge_ui.SetActive(true);
    }
}
