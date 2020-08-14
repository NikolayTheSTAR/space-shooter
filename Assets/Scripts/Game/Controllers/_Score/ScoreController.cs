using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Score
{
    public class ScoreController : MonoBehaviour, IScoreController
    {
        #region Private

        [SerializeField] private Text scoreText;
        [SerializeField] private Text neededScoreText;

        [Inject] private ILevelController level;

        private int scoreCount;
        private int neededScoreCount;

        #endregion // Private

        public void SetNeededScore(int value)
        {
            neededScoreCount = value;
            UpdateTexts();
        }

        public void AddScore(int value)
        {
            scoreCount += value;
            UpdateTexts();

            if (scoreCount >= neededScoreCount) level.Win();
        }

        private void UpdateTexts()
        {
            if (scoreText) scoreText.text = Convert.ToString(scoreCount);
            if (neededScoreText) neededScoreText.text = Convert.ToString(neededScoreCount);
        }
    }
}