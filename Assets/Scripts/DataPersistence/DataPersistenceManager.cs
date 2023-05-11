using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gamedata;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found one or more Data Persistence Manager in the Scene");
        }

        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gamedata = new GameData();
    }

    public void LoadGame()
    {
        this.gamedata = dataHandler.Load();
        if (this.gamedata == null)
        {
            Debug.Log("No data was found. Intializing data to defaults");
            NewGame();
        }

        foreach(IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gamedata);
        }
    }

    public void SaveGame()
    {
        Debug.Log("Saveing Game");
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gamedata);
        }

        dataHandler.Save(gamedata);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects); 
    }
}
