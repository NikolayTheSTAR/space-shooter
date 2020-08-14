using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuButtonInWin : MonoBehaviour, IButton
{
    #region Private

    [Inject] private IWin win;

    #endregion // Private

    public void Event()
    {
        win.OpenMenu();
    }
}