using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Npc
{
    //this represents how much the shopkeeper loves the current trade
    public float favor;

   public List<Item> item_stock;

    public void AddItem(Item i)
    {
        item_stock.Add(i);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
