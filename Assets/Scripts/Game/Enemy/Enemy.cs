using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        #region Private

        [SerializeField] protected EnemyData data;

        protected LevelController level;

        #endregion // Private

        #region Properties

        public Vector3 position
        {
            get
            {
                return transform.position;
            }
        }

        protected Vector2 direction;
        public Vector2 Direction
        {
            get
            {
                return direction;
            }
        }

        #endregion // Properties

        #region Unity Methodes

        void Start()
        {
            Init();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("PlayerBullet"))
            {
                Explosion();
                Destroy(col.gameObject);
            }
        }

        #endregion // Unity Methodes

        public void Translate(Vector2 translation)
        {
            transform.Translate(translation);
        }

        public void ExitFromMap()
        {
            Destroy(gameObject);
        }

        public void Explosion()
        {
            if (level)
            {
                level.CreateExplosion(transform.position);
                level.AddScore(data.Score);
            }
            Destroy(gameObject);
        }

        protected virtual void Init()
        {
            direction = new Vector2(0, -data.Speed);
            level = transform.GetComponentInParent<LevelController>();
        }
    }
}