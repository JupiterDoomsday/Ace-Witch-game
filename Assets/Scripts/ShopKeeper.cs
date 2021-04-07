using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Npc
{
    //this represents how much the shopkeeper loves the current trade
    public float favor;
    public float sway;
    public List<Item> item_stock;
    public List<Item> trade;

    public void AddItem(Item i)
    {
        item_stock.Add(i);
    }
    public bool willTrade()
    {
        return false;
    }
}
