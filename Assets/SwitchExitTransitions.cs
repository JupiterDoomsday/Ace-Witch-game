using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchExitTransitions : MonoBehaviour
{
    [SerializeField] GameObject FadeInTrigger;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Reset FadeIn");
            FadeInTrigger.SetActive(true);
        }
    }
}
