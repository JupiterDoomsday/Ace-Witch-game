using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ITEM_TYPE
{
    CONSUMABLE,
    PUZZLE,
    TRADEABLE,
    TOOL,
    LORE
};
public class Item
{
    public int id;
    public string item_name;
    public int amount;
    public ITEM_TYPE type;
    public string desc;
    public Sprite icon;
    public Item(string n, ITEM_TYPE t, string sprite_name)
    {
        this.item_name = n;
        this.type = t;
    }
}
