using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
using Yarn.Unity;

public class QuestManager : MonoBehaviour, IDataPersistence
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
    [YarnCommand("RemQuest")]
    public void RemoveQuest(int questId)
    {
        playerQuests.Remove(questId);
    }
    [YarnCommand("SetTask")]
    public void SetTask(int questId, int step)
    {
        data.SetStep(questId, step);
    }
    public Quest getQuest(int id)
    {
        return data.GetQuest(id);
    }

    [YarnCommand("AddQuest")]
    public void addQuestToPlayer(int i)
    {
        AddQuest(i);
    }

    public void LoadData(GameData save)
    {
        Debug.Log("Load Quest Manager Data");
        //this.transform.position = data.playerPosition;
        //this.dir = DIRECTION.UP;
        for (int i = 0; i < save.quest.Length; i++)
        {
            if (save.quest[i] == 0)
                continue;

            playerQuests.Add(i);
        }
    }

    public void SaveData(ref GameData save)
    {
        for( int i=0; i< playerQuests.Count; i++)
        {
            int quest = playerQuests[i];
            if (quest <= 20)
                continue;

            save.quest[quest] = 1;
        }
    }
}
