using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Npc
{
    //this represents how much the shopkeeper loves the current trade
    public float favor;
    public SHOPKEEPER_TYPE shop_type;
    public float sway;
    
    public Inventory goods;
    public Dictionary<string,int> adorations;

    public void Start()
    {
        goods = GlobalData.Instance.getShopKeeperInventory(shop_type);
    }

    private int getPersonalValue(Item i) {
        int sum = 0;
        foreach (string key in i.wordKeys)
        {
            if (adorations.ContainsKey(key))
                sum += adorations[key];
        }
        return sum;
    }
    public bool willTrade(Inventory stock, Inventory trade)
    {
        int stock_sum = checkInvoValue(stock);
        int barter_sum = checkInvoValue(trade);
        //check to see if it apeaes the shopkeeper
        if (barter_sum >= stock_sum)
            return true;
        return false;
    }
    public List<string> favItems = new List<string>();
    public List<int> favor_val = new List<int>();

    public int checkInvoValue(Inventory stock)
    {
        int sum = 0;
        foreach (ItemSlot i in stock.item_list)
        {
            sum += getPersonalValue(i.item) * i.amount;
        }
        return sum;
    }
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
    public void saveShopKeeper()
    {
        GlobalData.Instance.copyShopKeeperInvo(shop_type, goods);
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
