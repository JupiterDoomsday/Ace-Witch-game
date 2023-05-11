using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using state;

public class Owl : Npc
{
    public DataPersistenceManager saveFile;
    // Start is called before the first frame update

    private void Start()
    {
        runner.Add(startScript);
        expressions = new Dictionary<string, Sprite>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            expressions.Add(_keys[i], _values[i]);
    }

    public void Save()
    {
        saveFile.SaveGame();
    }

    public void Load()
    {
        saveFile.LoadGame();
    }
}
