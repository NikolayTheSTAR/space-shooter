using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class ShipEnemy : Enemy, IShooter
    {
        #region Private

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform bulletCreateDot;
        [SerializeField] private float shotTime = 0.5f;
        
        private const float SPEED_DEVIATION_RANGE = 0.2f;

        private float time;

        #endregion // Private

        #region Unity Methodes

        void Update()
        {
            time += Time.deltaTime;

            if (time > shotTime)
            {
                Shot();
                time = 0;
            }
        }

        #endregion // Unity Methodes

        protected override void Init()
        {
            shotTime = shotTime + Random.Range(-SPEED_DEVIATION_RANGE, SPEED_DEVIATION_RANGE);

            direction = new Vector2(0, -data.Speed);
            level = transform.GetComponentInParent<LevelController>();
        }

        public void Shot()
        {
            level.CreateBullet(bullet, bulletCreateDot ? bulletCreateDot.position : position);
        }
    }
}