using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    Inventory player_invo;
    private GameObject curItem;
    public TextMeshProUGUI inspectName;
    public TextMeshProUGUI inspectdesc;
    public Item cur_item_data;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemAmtMarker;
    public Menu playerMenuState;
    public Transform popUp;

    public void setInvo(Inventory i)
    {
        player_invo = i;
        //RefreshInventory();
    }
    public void hideMenu()
    {
        this.enabled = false;
    }
    public void Refresh()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;

        float cellSize = 64;
        foreach(ItemSlot i in player_invo.item_list)
        {
            RectTransform ItemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);
            ItemSlotRectTransform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);
            x++;
            if(x > 4)
            {
                x = 0;
                y++;
            }
            PickUp slot = ItemSlotRectTransform.GetComponent<PickUp>();
            slot.amt = i.amount;
            slot.item = i.item;
            Transform item_btn = ItemSlotRectTransform.GetChild(0);
            Button btn = item_btn.GetComponent<Button>();
            btn.onClick.AddListener(
                delegate{
                    playerMenuState.popUp = this.popUp.gameObject;
                    setCurItem(ItemSlotRectTransform.gameObject, slot.item);
                    popUp.gameObject.SetActive(true);
                    playerMenuState.isAtItemSelected();
                    deactivateAllItems();
                });
            item_btn.GetChild(0).GetComponent<Image>().sprite = i.item.icon;
            Transform icon = item_btn.GetChild(1);
            if (i.amount > 1)
                icon.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.amount.ToString();
            else
                icon.gameObject.SetActive(false);
        }

    }
    public void setCurItem(GameObject curItem, Item data)
    {
        this.curItem = curItem;
        this.cur_item_data = data;
    }
    public void deactivateAllItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Transform item_btn = child.GetChild(0);
            Button btn = item_btn.GetComponent<Button>();
            btn.interactable = false;
            
            
        }
    }
    public void activateAllItems()
    {
        this.curItem = null;
        this.cur_item_data = null;
        foreach (Transform child in itemSlotContainer)
        {
            Transform item_btn = child.GetChild(0);
            Button btn = item_btn.GetComponent<Button>();
            btn.interactable = true;
        }
    }
    public void setDesc()
    {
        if (this.curItem = null)
            return;
        inspectName.text = cur_item_data.item_name;
        inspectdesc.text = cur_item_data.desc;
    }
}
