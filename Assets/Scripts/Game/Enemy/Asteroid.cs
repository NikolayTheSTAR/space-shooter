using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class Asteroid : Enemy
    {
        #region Private

        private const float SPEED_DEVIATION_RANGE = 0.2f;

        #endregion Private

        protected override void Init()
        {
            direction = new Vector2(Random.Range(-SPEED_DEVIATION_RANGE, SPEED_DEVIATION_RANGE), -data.Speed + Random.Range(-SPEED_DEVIATION_RANGE, SPEED_DEVIATION_RANGE));
            level = transform.GetComponentInParent<LevelController>();
        }
    }
}