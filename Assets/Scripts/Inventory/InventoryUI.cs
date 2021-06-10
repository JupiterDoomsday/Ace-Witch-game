using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    Inventory player_invo;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemAmtMarker;
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
    public void RefreshInventory()
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
            Transform item_btn = ItemSlotRectTransform.GetChild(0);
            item_btn.gameObject.SetActive(true);
            item_btn.GetComponent<Image>().sprite = i.item.icon;
            if (i.amount > 1)
            {
                Transform icon = item_btn.GetChild(1);
                icon.gameObject.SetActive(true);
                icon.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.amount.ToString();
            }
        }
    }
}
