using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTouchEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int EventID;
    [SerializeField]
    private bool isSolved;
    [SerializeField]
    private bool destroyOnComplete;
    [SerializeField]
    private bool turnOffOnComplete;

    [SerializeField]
    internal UnityEngine.Events.UnityEvent OnTouched;
    internal UnityEngine.Events.UnityEvent OnExit;
    [SerializeField]
    private GameObject parentObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(destroyOnComplete || turnOffOnComplete)
        {
            StartCoroutine(RunThenDestroy());
        }
        else
        {
            OnTouched.Invoke();
        }
    }

    IEnumerator RunThenDestroy()
    {
        OnTouched.Invoke();

        for (int i = 0; i < OnTouched.GetPersistentEventCount(); ++i)
        {
            yield return new WaitForSeconds(.1f);
        }

        if(destroyOnComplete)
        {
            DeleteEvent();
        }
        else
        {
            TurnOffEvent();
        }
    }

    public void TurnOffEvent()
    {
        parentObject.SetActive(false);
    }

    public void DeleteEvent()
    {
        Destroy(parentObject, 0.0f);
    }

    // Update is called once per frame
}
