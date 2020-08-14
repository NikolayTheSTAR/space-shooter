using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Menu : MonoBehaviour, IMenu, ISceneTranslator
{
    #region Private

    [SerializeField] private Animator anim;

    [Inject] private IGameController game;

    #endregion // Private

    #region Properties

    public string NextSceneName
    {
        get;
        set;
    }

    public Animator TransitionAnim
    {
        get
        {
            return anim;
        }
    }

    #endregion // Properties

    public void OpenLevel(LevelData level)
    {
        game.SetActiveLevelData(level);
        StartTransitionTo("Game");
    }

    #region SceneTransition

    public void StartTransitionTo(string sceneName)
    {
        if (TransitionAnim) TransitionAnim.SetTrigger("Transition");
        NextSceneName = sceneName;
    }

    public void Transition()
    {
        SceneManager.LoadScene(NextSceneName);
    }

    #endregion // SceneTransition
}