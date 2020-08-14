using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fight;

public interface IPlayer : IShooter
{
    void Move(Control.MoveVector vector);

    void SetStatus(Player.Status value);

    void Die();
}