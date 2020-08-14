using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour, IGameController
{
    #region Static

    private static int activeLevelIndex;
    private static LevelData activeLevelData;
    private static List<LevelData> Levels
    {
        get;
        set;
    }
    
    #endregion Static

    #region Private

    [SerializeField] private List<LevelData> levels = new List<LevelData>();

    [Inject] private IDataController dataController;

    #endregion // Private

    #region Unity Methodes

    void Start()
    {
        Init();
    }

    #endregion // Unity Methodes

    private void Init()
    {
        if (levels.Count > 0) Levels = levels;
    }

    #region Get

    public LevelData GetActiveLevelData()
    {
        return activeLevelData;
    }

    public LevelData GetLevelData(int index)
    {
        try
        {
            return Levels[index];
        }
        catch
        {
            return null;
        }
    }
    
    public int GetLevelsCount()
    {
        return Levels.Count;
    }

    #endregion // Get

    #region Set

    public void SetActiveLevelData(LevelData value)
    {
        activeLevelData = value;
        activeLevelIndex = activeLevelData.Index;
    }

    #endregion // Set

    // public void LastCompletedLevel(int value)
    // {
    //     if (value <= Levels.Count)
    //     {
    //         dataController.SetLastCompletedLevel(value);
    //         UpdateActiveLevelData();
    //     }
    // }

    public void UpdateActiveLevelData()
    {
        try
        {
            activeLevelData = Levels[dataController.GetLastCompletedLevel()];
        }
        catch
        {
            Debug.Log("ActiveLevel in not updated");
        }
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }    
}