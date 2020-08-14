using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneTranslator
{
    string NextSceneName
    {
        get;
        set;
    }

    Animator TransitionAnim
    {
        get;
    }

    void StartTransitionTo(string sceneName);

    void Transition();
}