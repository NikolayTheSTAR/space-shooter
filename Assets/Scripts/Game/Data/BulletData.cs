using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    [CreateAssetMenu(menuName = "Data/Bullet", fileName = "BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private float speed = 10;

        public float Speed
        {
            get
            {
                return speed;
            }
        }

        [SerializeField] private AudioClip shotSound;

        public AudioClip ShotSound
        {
            get
            {
                return shotSound;
            }
        }

        [SerializeField] private Control.Direction direction;

        public Control.Direction Direction
        {
            get
            {
                return direction;
            }
        }
    }
}