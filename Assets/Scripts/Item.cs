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
    public bool tradeable;
    public List<string> wordKeys;
    public string item_name;
    public int amount;
    public ITEM_TYPE type;
    public string desc;
    public Sprite icon;
    public Item(string n, ITEM_TYPE t)
    {
        this.item_name = n;
        this.type = t;
    }
}
