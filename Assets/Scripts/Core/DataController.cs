using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class DataController : MonoBehaviour, IDataController
{
    #region Static

    private static bool Started;
    private static int LastCompletedLevel
    {
        get;
        set;
    }

    #endregion // Static

    #region Unity Methodes

    private void Awake()
    {
        Init();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGameState();
    }

    private void OnApplicationQuit()
    {
        SaveGameState();
    }

    #endregion // Unity Methodes

    private void Init()
    {
        TryLoadData();
        Started = true;
    }

    public int GetLastCompletedLevel()
    {
        return LastCompletedLevel;
    }

    public void SetLastCompletedLevel(int value)
    {
        LastCompletedLevel = value;
    }

    #region Save/Load

    public void SaveGameState()
    {
        SaveData data = new SaveData();

        data.Add("LastCompletedLevel", LastCompletedLevel);

        if (Serialization.SaveToBinnary<SaveData>(data)) Debug.Log("Game saved");
        else Debug.Log("Game not saved");
    }

    private void TryLoadData()
    {
        if (!Started)
        {
            SaveData data;

            if (!Serialization.LoadFromBinnary<SaveData>(out data)) return;

            LastCompletedLevel = data.Get<int>("LastCompletedLevel");

            Debug.Log("Game is loaded");
        }
    }

    #endregion //Save/Load
}

[System.Serializable]
public class SaveData
{
    private Dictionary<string, object> container;

    public SaveData()
    {
        container = new Dictionary<string, object>();
    }

    public void Add(string name, object obj)
    {
        container.Add(name, obj);
    }

    public T Get<T>(string name)
    {
        try
        {
            return (T)container[name];
        }
        catch
        {
            return default;
        }
    }
}