using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save 
{
    public string scenename;
    public int player_y;
    public int player_x;
    public List<int> playerInvo = new List<int>();
    public bool openingPlayed = false;

}
