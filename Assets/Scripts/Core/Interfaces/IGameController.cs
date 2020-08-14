using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void SetActiveLevelData(LevelData value);

    LevelData GetActiveLevelData();

    LevelData GetLevelData(int index);

    int GetLevelsCount();

    void OpenMenu();
}