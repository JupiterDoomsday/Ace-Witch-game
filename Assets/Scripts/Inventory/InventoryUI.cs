using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    Inventory player_invo;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public Transform itemAmtMarker;
    public Transform popUp;
    private void Awake()
    {
        itemSlotContainer = transform.Find("inventoryPanel");
        itemSlotTemplate = transform.Find("invo_slot");
    }

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
        foreach(Item i in player_invo.item_list)
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
            ItemSlotRectTransform.Find("icon").GetComponent <Image>().sprite = i.icon;
            if(i.amount > 1)
            {
                Transform icon = itemSlotTemplate.Find("amount");
                icon.gameObject.SetActive(true);
                icon.GetComponent<TextMeshPro>().text += i.amount;
            }
        }
    }
}
