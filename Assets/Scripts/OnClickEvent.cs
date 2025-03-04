using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
    [SerializeField]
    private int EventID;
    [SerializeField]
    private bool isSolved;
    [SerializeField]
    private bool destroyOnComplete;
    [SerializeField]
    private bool turnOffOnComplete;

    [SerializeField]
    internal UnityEngine.Events.UnityEvent OnClick;
    [SerializeField]
    private GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator RunThenDestroy()
    {
        OnClick.Invoke();

        for (int i = 0; i < OnClick.GetPersistentEventCount(); ++i)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (destroyOnComplete)
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
}
