using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Fight
{
    public class BulletController : MonoBehaviour, IBulletController
    {
        #region Private

        [Inject] private Sound.IAudioController audioController;

        private List<IBullet> bullets = new List<IBullet>();

        #endregion // Private

        #region Unity Methodes

        void Update()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                try
                {
                    bullets[i].Move();
                }
                catch
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion // Unity Mehtodes

        public void CreateBullet(GameObject bulletObject, Vector3 position)
        {
            try
            {
                if (bulletObject) bullets.Add(Instantiate(bulletObject, position, Quaternion.identity, transform).GetComponent<IBullet>());

                audioController.SetAudioEffect(bulletObject.GetComponent<IBullet>().GetShotSound());
            }
            catch
            {
                Debug.Log("Bullet is not created");
            }
        }
    }
}