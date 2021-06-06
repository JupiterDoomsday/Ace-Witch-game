using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string targetScene;
    public Animator transition;
    public float transitiontime = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            loadNextLevel();
    }

    public void loadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(targetScene);

    }
}
