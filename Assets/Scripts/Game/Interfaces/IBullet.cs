using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    AudioClip GetShotSound();

    void Move();
}