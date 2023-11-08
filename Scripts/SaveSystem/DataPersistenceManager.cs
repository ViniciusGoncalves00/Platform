using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    private string _fileName = "arachnid.arachnid";

    private MyPlayerData _playerData;
    
    private List<IDataPersistence> _dataPersistence;
    
    private FileHandler _fileHandler;
    
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Data Persistence Manager in the scene.");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        var path = Path.Combine(Application.persistentDataPath, _fileName);
        _fileHandler = new FileHandler(path);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _dataPersistence = FindAllDataPersistenceObjects();
        LoadGame();
    }
    
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
    
    public void NewGame()
    {
        _playerData = FindObjectOfType<Player>().GetComponent<MyPlayerData>();;
    }
    
    public void LoadGame()
    {
        _playerData = _fileHandler.Load();
        
        if (_playerData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistence)
        {
            dataPersistenceObj.LoadData(_playerData);
        }
    }
    
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in _dataPersistence)
        {
            dataPersistenceObj.SaveData(ref _playerData);
        }
        
        _fileHandler.Save(_playerData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        return FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>()
            .ToList();
    }
}
