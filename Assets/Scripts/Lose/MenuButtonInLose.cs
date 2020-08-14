using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuButtonInLose : MonoBehaviour, IButton
{
    #region Private

    [Inject] private ILose lose;

    #endregion // Private

    public void Event()
    {
        lose.OpenMenu();
    }
}