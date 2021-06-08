using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField]
    public List<Item> item_list;

    public Inventory()
    {
        item_list = new List<Item>();
    }
    public Item getItem(int id)
    {
        return item_list.Find(item => item.id == id);
    }
    public void AddItem(Item item)
    {
        Item target = getItem(item.id);
        if (target == null)
            item_list.Add(item);
        else
            target.amount++;
    }
    
    public Item RemoveItem(int id)
    {
        Item target = getItem(id);
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
