using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Fight
{
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Private

        [SerializeField] private BulletData data;

        private Transform tran;
        private float speed;
        private Control.Direction direction;

        #endregion // Private
        
        #region Unity Methodes

        void Start()
        {
            Init();
            SetData(data);
        }

        #endregion // Unity Methodes

        private void Init()
        {
            tran = transform;
        }

        public void Move()
        {
            switch(data.Direction)
            {
                case Control.Direction.Up:
                    tran.Translate(new Vector2(0, speed * Time.deltaTime));
                    break;

                case Control.Direction.Down:
                    tran.Translate(new Vector2(0, -speed * Time.deltaTime));
                    break;
            }

            if (tran.position.y > LevelController.PLAYER_CRIT_DOT_Y) Destroy();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void SetData(BulletData value)
        {
            if (value)
            {
                data = value;
                speed = data.Speed;
                direction = data.Direction;
            }
        }

        public AudioClip GetShotSound()
        {
            return data.ShotSound;
        }
    }
}