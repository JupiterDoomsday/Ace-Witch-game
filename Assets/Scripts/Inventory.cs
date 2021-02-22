using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> item_list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Item getItem(int id)
    {
        return item_list.Find(item => item.id == id);
    }
    public void AddItem(Item item)
    {
            item_list.Add(item);
    }
}
