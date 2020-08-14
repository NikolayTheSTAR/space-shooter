using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public interface IEnemy
    {
        Vector3 position
        {
            get;
        }

        Vector2 Direction
        {
            get;
        }

        void Translate(Vector2 translation);

        void Explosion();

        void ExitFromMap();
    }
}