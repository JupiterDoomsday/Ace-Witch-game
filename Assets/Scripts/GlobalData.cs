using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SHOPKEEPER_TYPE
{
    ZAC,
    HATCHLINGS,
};
public class GlobalData : MonoBehaviour
{
    public static GlobalData Instance;
    public Inventory playerInvo = new Inventory();
    private Inventory zacShop;
    // Start is called before the first frame update
    void Awake()
    {
       if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
       else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void copyShopKeeperInvo(SHOPKEEPER_TYPE type, Inventory invo)
    {
        switch (type)
        {
            case SHOPKEEPER_TYPE.ZAC:
                zacShop = new Inventory(invo);
                break;
        }
    }
    public Inventory getPlayerInventory()
    {
        return playerInvo;
    }
    public Inventory getShopKeeperInventory(SHOPKEEPER_TYPE type)
    {
        switch (type)
        {
            case SHOPKEEPER_TYPE.ZAC:
                return zacShop;
            default:
                return null;
        }
    }
    public void copyPlayerInvo(Inventory invo)
    {
        playerInvo = new Inventory(invo);
    }
}
