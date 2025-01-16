using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Item/ItemDataBase", order = 1)]
public class ItemDataBase : ScriptableObject
{
    private static ItemDataBase _instance;
    public static ItemDataBase Instance { get { return _instance; } }
    [SerializeField]
    List<Item> items;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    public Item GetItem(int id)
    {
        if( id > items.Count)
        {
            return null;
        }
        return items[id];
    }

    public Sprite GetIcon(int id)
    {
        return items[id].icon;
    }
}
