using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int room;
    public Vector3 playerPosition;
    public int[] quest;
    public int[] questStep;
    public int[] invo;
    public int[] itemAmt;
    public bool[] eventList;
    public bool opening;
    public bool bathroomOpen;
    public bool drinkWater;

    public GameData()
    {
        room = 0;
        playerPosition = Vector3.zero;
        bathroomOpen = false;
        drinkWater = false;
        quest = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        itemAmt = new int[30] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        invo = new int[30] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        eventList = new bool[10];
        opening = true;
    }
}


