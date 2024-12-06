using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //Set this calss to be a singleton
    private static QuestManager _instance;
    public static QuestManager Instance { get { return _instance; } }
    [SerializeField]
    private QuestDatabase data;
    public List<int> playerQuests;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void AddQuest(int questId)
    {
        playerQuests.Add(questId);
        data.AcceptQuest(questId);
    }
    public void RemoveQuest(int questId)
    {
        playerQuests.Remove(questId);
    }
    public void nextTask(int questId)
    {
        data.nextStep(questId);
    }
    public Quest getQuest(int id)
    {
        return data.GetQuest(id);
    }
}
