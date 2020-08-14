using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    [CreateAssetMenu(menuName = "Data/Joystick", fileName = "JoystickData")]
    public class JoystickData : ScriptableObject
    {
        [SerializeField] private Sprite fonSprite;

        public Sprite FonSprite
        {
            get
            {
                return fonSprite;
            }
        }

        [SerializeField] private Sprite topSprite;

        public Sprite TopSprite
        {
            get
            {
                return topSprite;
            }
        }
    }
}