using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    [CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private float speed;

        public float Speed
        {
            get
            {
                return speed;
            }
        }

        [SerializeField] private int score;

        public int Score
        {
            get
            {
                return score;
            }
        }
    }
}