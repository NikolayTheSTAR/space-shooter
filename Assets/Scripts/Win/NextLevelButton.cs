using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NextLevelButton : MonoBehaviour, IButton
{
    #region Private

    [Inject] private IWin win;

    #endregion // Private

    public void Event()
    {
        win.NextLevel();
    }
}