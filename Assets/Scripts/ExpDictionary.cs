using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New dictionary", menuName = "dict")]
public class ExpDictionary: ScriptableObject
{

    [SerializeField]
    private List<string> keysList = new List<string>();
    public List<string> KeysList
    {
        get { return keysList; }
        set { keysList = value; }
    }

    [SerializeField]
    private List<Sprite> valuesList = new List<Sprite>();
    public List<Sprite> ValuesList
    {
        get { return valuesList; }
        set { valuesList = value; }
    }

    private Dictionary<string, Sprite> dictionaryData = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> DictionaryData
    {
        get { return dictionaryData; }
        set { dictionaryData = value; }
    }

    public Sprite this[string index]
    {
        get => dictionaryData[index];
        set => dictionaryData[index] = value;
    }

    public void Awake()
    {
        try
        {

            for (int i = 0; i < keysList.Count; i++)
            {
                dictionaryData.Add(keysList[i], valuesList[i]);
            }
            Debug.Log("Compiled the dictionary");

        }
        catch (Exception)
        {
            Debug.LogError("KeysList.Count is not equal to ValuesList.Count. It shouldn't happen!");
        }

    }

    public void OnEnable()
    {

        Debug.Log("totototo");
        try
        {

            for (int i = 0; i < keysList.Count; i++)
            {
                dictionaryData.Add(keysList[i], valuesList[i]);
            }
            Debug.Log("Compiled the dictionary");

        }
        catch (Exception)
        {
            Debug.LogError("KeysList.Count is not equal to ValuesList.Count. It shouldn't happen!");
        }

    }

    public void Add(string key, Sprite data)
    {
        dictionaryData.Add(key, data);
        keysList.Add(key);
        valuesList.Add(data);
    }

    public void Remove(string key)
    {
        valuesList.Remove(dictionaryData[key]);
        keysList.Remove(key);
        dictionaryData.Remove(key);

    }
    public bool ContainsKey(string key)
    {
        return DictionaryData.ContainsKey(key);
    }

    public bool ContainsValue(Sprite data)
    {
        return DictionaryData.ContainsValue(data);
    }

    public void Clear()
    {
        DictionaryData.Clear();
        keysList.Clear();
        valuesList.Clear();
    }
}
