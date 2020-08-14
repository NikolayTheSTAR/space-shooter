using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class Explosion : MonoBehaviour
    {
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}