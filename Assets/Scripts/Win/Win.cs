using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour, IWin, ISceneTranslator
{
    #region Private

    [SerializeField] private Animator anim;

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

    public void NextLevel()
    {
        StartTransitionTo("Game");
    }

    public void OpenMenu()
    {
        StartTransitionTo("Menu");
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