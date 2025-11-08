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
    [SerializeField]
    [TextArea(2, 10)]
    private List<string> taskDescs;
    private bool m_isComplete = false;
    public int questId;

    [SerializeField]
    private Quest_State state;
    [SerializeField]
    private int curTask = 0;

    public void SetToActive()
    {
        this.state = Quest_State.ACTIVE;
    }
    public void SetToInactive()
    {
        this.state = Quest_State.INACTIVE;
    }
    public string GetTaskDesc(int i)
    {
        return taskDescs[i];
    }
    public void SetTaskStep(int i)
    {
        int limit = taskDescs.Count - 1;
        if (i > limit)
            Debug.Log("SetTaskStep: ERROR step is too big");
        
        curTask = i;

        if (curTask == limit) //check to see if the task is completed
        {
            m_isComplete = true;
            state = Quest_State.COMPLETE;
        }
            
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
    public int taskNum;
    [SerializeField]
    public int reqTask;
    public int target_id;

    public bool triggerTask(int prevTask)
    {
        return prevTask == reqTask;
    }
}
