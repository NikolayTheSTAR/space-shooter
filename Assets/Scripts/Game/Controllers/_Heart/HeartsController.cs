using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Health
{
    public class HeartsController : MonoBehaviour, IHeartsController
    {
        #region Private

        [SerializeField] private List<HeartIcon> hearts = new List<HeartIcon>();

        [Inject] private ILevelController level;

        [Range(0, MAX_COUNT)]
        private int count = MAX_COUNT;

        private const int MAX_COUNT = 3;

        #endregion // Private

        void Start()
        {
            UpdateHearts();
        }

        public void Reduction()
        {
            count--;
            UpdateHearts();

            if (count == 0) level.Lose();
        }

        private void UpdateHearts()
        {
            try
            {
                for (int i = 0; i < MAX_COUNT; i++)
                {
                    hearts[i].SetStatus(i < count ? HeartIcon.Status.Full : HeartIcon.Status.Empty);
                }
            }
            catch
            {
                Debug.Log("Hearts is not updated");
            }
        }
    }
}