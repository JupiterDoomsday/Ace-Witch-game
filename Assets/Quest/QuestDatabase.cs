using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestDatabase : ScriptableObject
{
    [SerializeField]
    List<Quest> Quests;

    public void AcceptQuest(int questId)
    {
        if (questId < Quests.Count)
            Quests[questId].SetToActive();
    }

    public void DeactivateQuest(int questId)
    {
        if (questId < Quests.Count)
            Quests[questId].SetToInactive();
    }
    public void SetStep(int questId, int step)
    {
        if (questId < Quests.Count)
            Quests[questId].SetTaskStep(step);
    }


    public string GetDesc(int id)
    {
        if (id >= Quests.Count)
            return null;
       
        return  Quests[id].desc;
    }

    public Quest GetQuest(int id)
    {
        if (id >= Quests.Count)
            return null;
        return Quests[id];
    }

    public List<Quest> GetQuestList()
    {
        return Quests;
    }
}
