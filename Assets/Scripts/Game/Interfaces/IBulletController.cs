using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletController
{
    void CreateBullet(GameObject bulletObject, Vector3 position);
}