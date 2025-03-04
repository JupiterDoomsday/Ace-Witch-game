using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunEvent : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private int EventID;
    [SerializeField]
    internal UnityEngine.Events.UnityEvent OnTrigger;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Player player;
    [SerializeField]
    private WarpZone warp;
    private bool isOneShot = true;
    private bool isComplete = false;

    public void Start()
    {
       if(isOneShot && isComplete)
        {
            Destroy(this);
        }
    }

   public void FixedUpdate()
    {
        if( !isComplete && !warp.isRunning())
        {
            OnTrigger.Invoke();
            isComplete = true;
            this.enabled = false;
        }
    }

    public void LoadData(GameData data)
    {
        isComplete = data.eventList[EventID];
    }
    public void SaveData(ref GameData data)
    {
        data.eventList[EventID] = isComplete;
    }

    // Start is called before the first frame update
}
