using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelController
{
    void CreateExplosion(Vector2 position);

    void Lose();

    void Win();
}