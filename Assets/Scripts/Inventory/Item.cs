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
[CreateAssetMenu]
public class Item: ScriptableObject
{
    public int id;
    public bool tradeable;
    public List<string> wordKeys;
    public string item_name;
    public ITEM_TYPE type;
    public string desc;
    public Sprite icon;
    public Item(int id,string n, string desc, ITEM_TYPE type, List<string> descript, Sprite img)
    {
        this.item_name = n;
        this.id = id;
        this.desc = desc;
        this.type = type;
        this.wordKeys = descript;
        this.icon = img;
    }
}
