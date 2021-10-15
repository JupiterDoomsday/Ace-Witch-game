using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestDatabase : ScriptableObject
{
    [SerializeField]
    List<Quest> playerQuests;

    public void AcceptQuest(int questId)
    {
        playerQuests[questId].SetToActive();
    }

    public void DeactivateQuest(int questId)
    {
        playerQuests[questId].SetToInactive();
    }

    public Quest GetQuest(int id)
    {
        return playerQuests[id];
    }

    public List<Quest> GetQuestList()
    {
        return playerQuests;
    }
}
