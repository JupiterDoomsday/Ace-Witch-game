using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField]
    public List<ItemSlot> item_list;

    public Inventory()
    {
        item_list = new List<ItemSlot>();
    }
    public ItemSlot getItem(int id)
    {
        return item_list.Find(item => item.item_id == id);
    }
    public void AddItem(Item item, int amt)
    {
        ItemSlot target = getItem(item.id);
        if (target == null)
            item_list.Add(new ItemSlot(item,amt));
        else
            target.amount+= amt;
    }
    
    public ItemSlot RemoveItem(int id)
    {
        ItemSlot target = getItem(id);
        if (target == null)
            return null;

        if (target.amount > 1)
          target.amount--;
        else
          item_list.Remove(target);

        return target;
    }
    /*
     * public Item getItem(int id)
        {
            return item_list[id];
        }
        public void AddItem(Item item)
        {
                item_list[item.id]=item;
        }
    }*/
}
