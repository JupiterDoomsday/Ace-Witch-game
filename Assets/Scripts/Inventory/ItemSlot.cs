using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    // Start is called before the first frame update
    public int item_id;
    public int amount;
    public Item item;
    public ItemSlot(Item item)
    {
        item_id = item.id;
        amount = 1;
        this.item = item;
    }
    public ItemSlot(Item item, int amt)
    {
        item_id = item.id;
        amount = amt;
        this.item = item;
    }
}
