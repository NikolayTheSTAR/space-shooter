using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    public interface IScoreController
    {
        void AddScore(int value);

        void SetNeededScore(int value);
    }
}