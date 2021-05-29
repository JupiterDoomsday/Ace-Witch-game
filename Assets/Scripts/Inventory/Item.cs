using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ITEM_TYPE
{
    CONSUMABLE,
    PUZZLE,
    TOOL,
    LORE
};
public class Item
{
    public int id;
    public int amount;
    public bool tradeable;
    public List<string> wordKeys;
    public string item_name;
    public ITEM_TYPE type;
    public string desc;
    public Sprite icon;
    public Item(string n, ITEM_TYPE t)
    {
        this.item_name = n;
        this.type = t;
        this.amount = 1;
    }
    public Item(string n, ITEM_TYPE t, int amt)
    {
        this.item_name = n;
        this.type = t;
        this.amount = amt;
    }
}
