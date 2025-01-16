using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    // Start is called before the first frame update
    public int item_id;
    public int amount;
    public ItemSlot(int id)
    {
        item_id = id;
        amount = 1;
    }
    public ItemSlot(int item, int amt)
    {
        item_id = item;
        amount = amt;
    }
}
