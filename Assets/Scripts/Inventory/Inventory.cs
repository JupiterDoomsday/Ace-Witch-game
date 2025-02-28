using System;
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
    public Inventory(Inventory i)
    {
        item_list = new List<ItemSlot>();
        foreach(ItemSlot id in i.item_list)
        {
            item_list.Add(id);
        }
    }
    public ItemSlot getItem(int id)
    {
        return item_list.Find(item => item.item_id == id);
    }
    public void AddItem(int item, int amt)
    {
        ItemSlot target = getItem(item);
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
        {
            target.amount--;
            return null;
        }
          
        else
          item_list.Remove(target);

        return target;
    }
    public bool hasItem(int id)
    {
        foreach(ItemSlot i in item_list)
        {
            if (i.item_id == id)
                return true;
        }
        return false;
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
