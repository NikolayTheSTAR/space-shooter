using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataController
{
    int GetLastCompletedLevel();

    void SetLastCompletedLevel(int value);
}