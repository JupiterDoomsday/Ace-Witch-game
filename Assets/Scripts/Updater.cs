using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomUpdate
{
    public enum UpdateType { Update, FixedUpdate, LateUpdate, FinalUpdate}
    public class Updater : MonoBehaviour
    {
        public static Updater Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<Updater>();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static Updater instance;
        private List<IUpdate> UpdateQueue = new List<IUpdate>();
        private List<IUpdate> FixedUpdateQueue = new List<IUpdate>();
        private List<IUpdate> FinalUpdateQueue = new List<IUpdate>();
        private List<IUpdate> LateUpdateQueue = new List<IUpdate>();
            //add queues
        private List<IUpdate> UpdateAddQueue = new List<IUpdate>();
        private List<IUpdate> FixedUpdateAddQueue = new List<IUpdate>();
        private List<IUpdate> FinalUpdateAddQueue = new List<IUpdate>();
        private List<IUpdate> LateUpdateAddQueue = new List<IUpdate>();
            //remove queue
        private List<IUpdate> UpdateRemovalQueue = new List<IUpdate>();
        private List<IUpdate> FixedUpdateRemovalQueue = new List<IUpdate>();
        private List<IUpdate> FinalUpdateRemovalQueue = new List<IUpdate>();
        private List<IUpdate> LateUpdateRemovalQueue = new List<IUpdate>();
        //adds a new queu to be processed
        public void RegisterUpdate(IUpdate script, UpdateType type)
        {
            switch (type)
            {
                case (UpdateType.Update):
                    UpdateAddQueue.Add(script);
                    break;
                case (UpdateType.FixedUpdate):
                    FixedUpdateAddQueue.Add(script);
                    break;
                case (UpdateType.FinalUpdate):
                    FinalUpdateAddQueue.Add(script);
                    break;
                case (UpdateType.LateUpdate):
                    LateUpdateAddQueue.Add(script);
                    break;
            }
        }
        //removes an old queed update inot the removal queue
        public void UnregisterUpdate(IUpdate script, UpdateType type)
        {
            switch (type)
            {
                case (UpdateType.Update):
                    UpdateRemovalQueue.Add(script);
                    break;
                case (UpdateType.FixedUpdate):
                    FixedUpdateRemovalQueue.Add(script);
                    break;
                case (UpdateType.FinalUpdate):
                    FinalUpdateRemovalQueue.Add(script);
                    break;
                case (UpdateType.LateUpdate):
                    LateUpdateRemovalQueue.Add(script);
                    break;
            }
        } //takes exsisting objects and adds them to the queue
        private void Awake()
        {
            Instance = this;
            MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
            foreach(MonoBehaviour script in scripts)
            {
                if(script is IUpdate)
                {
                    if ((script as IUpdate).NeedsUpdate)
                        UpdateQueue.Add((IUpdate) script);
                    if ((script as IUpdate).NeedsFixedUpdate)
                        FixedUpdateQueue.Add((IUpdate)script);
                    if ((script as IUpdate).NeedsLateUpdate)
                        LateUpdateQueue.Add((IUpdate)script);
                    if ((script as IUpdate).NeedsFinalUpdate)
                        FinalUpdateQueue.Add((IUpdate)script);

                }
            }
        }
        void Update()
        {
            if(UpdateAddQueue.Count > 0)
            {
                for( int i = UpdateAddQueue.Count -1; i >= 0; i--)
                {
                    UpdateQueue.Add(UpdateAddQueue[i]);
                    UpdateAddQueue.Remove(UpdateAddQueue[i]);
                }
            }
            if (UpdateRemovalQueue.Count > 0)
            {
                for (int i = UpdateAddQueue.Count - 1; i >= 0; i--)
                {
                    UpdateQueue.Remove(UpdateRemovalQueue[i]);
                    UpdateRemovalQueue.Remove(UpdateRemovalQueue[i]);
                }
            }
            foreach (IUpdate queued in UpdateQueue)
            {
                queued.PerformUpdate(Time.deltaTime);
            }
        }
    } 
}
