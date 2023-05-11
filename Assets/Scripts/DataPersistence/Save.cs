using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save 
{
    public string scenename;
    public int player_y;
    public int player_x;
    public List<Item> playerInvo = new List<Item>();
    public List<int> npcPos = new List<int>();
    public List<int> itemPos = new List<int>();
    public List<Item> inSceneItems = new List<Item>();

}
