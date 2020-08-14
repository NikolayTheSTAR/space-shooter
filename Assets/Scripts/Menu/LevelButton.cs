using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelButton : MonoBehaviour, IButton
{
    #region Private

    [SerializeField] private int index;
    [SerializeField] private Animator anim;

    [Inject] private IMenu menu;
    [Inject] private IGameController game;
    [Inject] private IDataController dataController;

    private Status status;

    #endregion // Private

    #region Unity Mehtodes

    void Start()
    {
        UpdateStatus();
        UpdateButton();
    }

    void OnMouseDown()
    {
        Event();
    }

    #endregion // Unity Methodes

    #region Update

    private void UpdateStatus()
    {
        if (index <= dataController.GetLastCompletedLevel()) status = Status.Complete;
        else if (index == dataController.GetLastCompletedLevel() + 1) status = Status.Open;
        else status = Status.Close;
    }

    private void UpdateButton()
    {
        if (anim) anim.SetInteger("Status", (int)status);
    }

    #endregion // Update

    public void Event()
    {
        switch(status)
        {
            case Status.Open:
            case Status.Complete:
                menu.OpenLevel(game.GetLevelData(index - 1));
                break;
        }
    }

    private enum Status
    {
        Close,
        Open,
        Complete
    }
}