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
            item_list.Add(item);
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
