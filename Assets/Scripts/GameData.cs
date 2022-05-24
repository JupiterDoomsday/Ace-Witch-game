using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int room = 0;
    public float[] position = { 0, 0 , 0};
    public int[,] quest = new int[20,2];
    public int[] invo = new int[30];

    public void saveRoom(int sceneId)
    {
        room = sceneId;
    }
    public void savePosition(Vector3 pos)
    {
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
    }

    public void ResetData()
    {
        room = 0;

    }
}


