using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Quest_State
    {
        INACTIVE,
        ACTIVE,
        COMPLETE
    }

    public enum Task_Target
    {
        ITEM,
        NPC,
        AREA
    }
    [CreateAssetMenu]
    public class Quest : ScriptableObject
    {
        public string oracle;
        public string title;
        [TextArea(10, 15)]
        public string desc;
        private bool m_isComplete = false;
        public int questId;
        [SerializeField]
        public List<QuestTask> taskList;

        [SerializeField]
        private Quest_State state;
        private int curTask = 0;

        public void SetToActive()
        {
            this.state = Quest_State.ACTIVE;
        }
        public void SetToInactive()
        {
            this.state = Quest_State.INACTIVE;
        }
        public string GetCurTaskDesc()
        {
            return taskList[curTask].desc;
        }
       public bool isQuestCompleted()
        {
            return m_isComplete;
        }
        public void Complete()
        {
            m_isComplete = true;
        }
        public int checkCurrentTask()
        {
            return curTask;
        }
        public Quest_State getState()
        {
            return this.state;
        }
    }

    [System.Serializable]
    public class QuestTask
    {
        [SerializeField]
        [TextArea(2, 10)]
        public string desc;
        [SerializeField]
        public int taskNum;
        [SerializeField]
        public int reqTask;
        public int target_id;

        public bool triggerTask(int prevTask)
        {
            return prevTask == reqTask;
        }
    }
