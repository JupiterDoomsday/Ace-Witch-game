using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
    public QuestManager questSystem;
    //private GameObject m_curItem;
    //public GameObject desc;
    public TextMeshProUGUI inspectName;
    public TextMeshProUGUI inspectdesc;
    public GameObject popup;
    public Transform questSlotContainer;
    public Transform questSlotTemplate;
    public Transform taskSlotContainer;
    public Transform taskSlotTemplate;
    public Menu playerMenuState;
    //private Quest m_curQuestInspect;

    // Start is called before the first frame update
    public void RefreshQuestList()
    {
        foreach (Transform child in questSlotContainer)
        {
            if (child == questSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int y = 0;
        foreach (int i in questSystem.playerQuests)
        {
            Quest curQuest = questSystem.getQuest(i);
            if (curQuest == null)
                continue;
            if (curQuest.getState() == Quest_State.INACTIVE || curQuest.getState() == Quest_State.COMPLETE)
                continue;
            RectTransform questSlotRectTransform = Instantiate(questSlotTemplate, questSlotContainer).GetComponent<RectTransform>();
            questSlotRectTransform.gameObject.SetActive(true);
            questSlotRectTransform.anchoredPosition = new Vector2(100, y * 75);
            y++;
            Transform q_btn = questSlotRectTransform.GetChild(0);
            Button btn = q_btn.GetComponent<Button>();
            TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
            label.text = curQuest.title;

            btn.onClick.AddListener(
                delegate {
                    playerMenuState.popUp = this.popup;
                    popup.SetActive(true);
                    setCurQuest(curQuest);
                    ActivateAllQuestTask(curQuest.GetCurTaskDesc());
                    playerMenuState.isAtQuestSelected();
                    deactivateAllItems();
                });
        }
        
    }
    public void deactivateAllItems()
    {
        foreach (Transform child in questSlotContainer)
        {
            Transform q_btn = child.GetChild(0);
            Button btn = q_btn.GetComponent<Button>();
            btn.interactable = false;
        }
    }
    public void setCurQuest(Quest data)
    {
        inspectName.text = data.title;
        inspectdesc.text = data.desc;
    }
    public void activateAllItems()
    {
        foreach (Transform child in questSlotContainer)
        {
            Transform q_btn = child.GetChild(0);
            Button btn = q_btn.GetComponent<Button>();
            btn.interactable = true;
        }
    }
    private void ActivateAllQuestTask(string desc)
    {
            RectTransform taskSlotRectTransform = Instantiate(taskSlotTemplate, taskSlotContainer).GetComponent<RectTransform>();
            taskSlotRectTransform.gameObject.SetActive(true);
            taskSlotRectTransform.anchoredPosition = new Vector2(100, 0);
            Transform q_img = taskSlotRectTransform.GetChild(1);
            TextMeshProUGUI label = q_img.GetComponent<TextMeshProUGUI>();
            label.text = desc;
    }
    public void DeactivateAllQuestTask()
    {
        foreach (Transform child in taskSlotContainer)
        {
            Destroy(child.gameObject);
        }
    }
    /*public void setQuestDesc()
    {
        if (this.m_curItem = null)
            return;
        inspectName.text = m_curQuestInspect.title;
        inspectdesc.text = m_curQuestInspect.desc;
    }*/
}
