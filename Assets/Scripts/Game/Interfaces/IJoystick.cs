﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public interface IJoystick
    {
        Joystick.Status GetStatus();
    }
}