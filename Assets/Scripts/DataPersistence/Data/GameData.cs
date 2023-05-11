using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int room;
    public Vector3 playerPosition;
    public int[,] quest = new int[20,2];
    public int[] invo;

    public GameData()
    {
        room = 0;
        playerPosition = Vector3.zero;
        invo = new int[30];
    }
}


