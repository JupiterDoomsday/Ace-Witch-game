using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Npc
{
    //this represents how much the shopkeeper loves the current trade
    public float favor;
    public float sway;
    public List<Item> item_stock;
    public List<Item> trade_stock;
    public Dictionary<string,int> adorations;
   
    public void AddItem(Item i)
    {
        item_stock.Add(i);
    }
    public bool willTrade()
    {
        return false;
    }
    public List<string> favItems = new List<string>();
    public List<int> favor_val = new List<int>();


    public override void OnBeforeSerialize()
    {
        favItems.Clear();
        favor_val.Clear();
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in expressions)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
        foreach (var kvp in adorations)
        {
            favItems.Add(kvp.Key);
            favor_val.Add(kvp.Value);
        }
    }

    public override void OnAfterDeserialize()
    {
        adorations = new Dictionary<string, int>();
        expressions = new Dictionary<string, Sprite>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);

        for (int i = 0; i != Math.Min(favItems.Count, favor_val.Count); i++)
            adorations.Add(favItems[i], favor_val[i]);
    }
}
