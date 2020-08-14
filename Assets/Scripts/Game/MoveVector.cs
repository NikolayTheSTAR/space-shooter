using UnityEngine;

namespace Control
{
    // класс представляющий Vector2, оси которого принимают значения от -1 до 1. Используется для расчёта скоости объектов.
    public class MoveVector
    {
        [Range(-1, 1)] [SerializePrivateVariables] public float x;
        [Range(-1, 1)] public float y;

        public MoveVector(float valueX, float valueY)
        {
            x = valueX;
            y = valueY;        
        }

        public MoveVector(Vector2 basedVector, float maxDistance)
        {
            try
            {   
                x = basedVector.x / maxDistance;
                y = basedVector.y / maxDistance;
            }
            catch
            {
                Debug.Log("Не удалось расчитать MoveVector");
            }
        }
    }
}