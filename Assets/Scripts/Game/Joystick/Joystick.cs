using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Control
{
    // класс представляющий джойстик
    public class Joystick : MonoBehaviour, IJoystick
    {
        #region Private

        [SerializeField] private JoystickData sleepData;
        [SerializeField] private JoystickData activeData;
        [SerializeField] private Image fonImg;
        [SerializeField] private Image topImg;
        [SerializeField] private Transform fonTran;
        [SerializeField] private Transform topTran;

        [Inject] private IPlayer player;

        private Touch fastenedTouch;

        public const float MAX_MOVE_DISTANCE = 125f;
        private const float DELTA_FON_MOVING = 7f;
        private const float MOVE_TO_SLEEP_SPEED = 10f;

        private Status status;

        #endregion // Private
        
        #region Unity Methodes

        void Start()
        {
            SetJoystickImg(sleepData);
        }

        void OnMouseDown()
        {
            SetStatus(Status.Active);
        }

        void OnMouseUp()
        {
            SetStatus(Status.ToSleep);
        }

        void Update()
        {
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawRay(Camera.main.transform.position, ray.direction * 25);

            // Debug.Log("Input.mousePosition = " + Input.mousePosition);

            switch (status)
            {
                case Status.Sleep:

                    // for (int i = 0; i < Input.touchCount; i++)
                    // {
                    //     if (Input.GetTouch(i).phase == TouchPhase.Began)
                    //     {
                    //         fastenedTouch = Input.GetTouch(i);
                    //         Ray ray = new Ray();
                    //     }
                    // }

                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        // debugText.text = "touch.position = " + Camera.main.ScreenToWorldPoint(new Vector3(touch.position.y, touch.position.x));
                    }
                    // else debugText.text = "...";
                    break;

                case Status.Active:
                    UpdateTopPosition();
                    break;
                
                case Status.ToSleep:
                    MoveToSleep();
                    break;
            }
        }

        #endregion // Unity Methodes

        #region Set

        private void SetJoystickImg(JoystickData data)
        {
            try
            {
                fonImg.sprite = data.FonSprite;
                topImg.sprite = data.TopSprite;
            }
            catch
            {
                Debug.Log("Joystick is not updated");
            }
        }

        private void SetStatus(Status nextStatus)
        {
            status = nextStatus;

            switch (status)
            {
                case Status.Sleep:
                    player.SetStatus(Player.Status.Sleep);
                    SetJoystickImg(sleepData);
                    ResetTopAndFonPosition();
                    break;
                
                case Status.Active:
                    player.SetStatus(Player.Status.Moving);
                    SetJoystickImg(activeData);
                    break;
                
                case Status.ToSleep:
                    player.SetStatus(Player.Status.Sleep);
                    SetJoystickImg(sleepData);
                    break;
            }
        }
        
        #endregion // Set
        
        #region Get

        public Status GetStatus()
        {
            return status;
        }

        #endregion // Get

        private void MoveToSleep()
        {
            if (topTran.localPosition.x != 0 || topTran.localPosition.y != 0)
            {
                float step = MOVE_TO_SLEEP_SPEED * Time.deltaTime;

                float ratioXY;

                if (topTran.localPosition.x == 0) ratioXY = 0;
                else if (topTran.localPosition.y == 0) ratioXY = 1;
                else ratioXY = topTran.localPosition.x / topTran.localPosition.y;

                float stepY, stepX;

                stepY = Mathf.Abs(Mathf.Sqrt((step * step) / (ratioXY * ratioXY + 1)));
                stepX = Mathf.Abs(stepY * ratioXY);

                // смещение влево
                if (topTran.localPosition.x > 0)
                {
                    topTran.transform.Translate(new Vector3(-stepX, 0, 0));
                    UpdateFonPosition();

                    if (topTran.localPosition.x < 0) SetStatus(Status.Sleep);
                }

                // смещение вправо
                else if (topTran.localPosition.x < 0)
                {
                    topTran.transform.Translate(new Vector3(stepX, 0, 0));
                    UpdateFonPosition();

                    if (topTran.localPosition.x > 0) SetStatus(Status.Sleep);
                }

                // смещение вниз
                if (topTran.localPosition.y > 0)
                {
                    topTran.transform.Translate(new Vector3(0, -stepY, 0));
                    UpdateFonPosition();

                    if (topTran.localPosition.y < 0) SetStatus(Status.Sleep);
                }

                // смещение вниз
                else if (topTran.localPosition.y < 0)
                {
                    topTran.transform.Translate(new Vector3(0, stepY, 0));
                    UpdateFonPosition();

                    if (topTran.localPosition.y > 0) SetStatus(Status.Sleep);
                }

                player.Move(new MoveVector(topTran.localPosition, MAX_MOVE_DISTANCE));
            }
            else SetStatus(Status.Sleep);
        }

        private void UpdateTopPosition()
        {
            if (topTran && fonTran)
            {
                Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                topTran.position = new Vector3(cursorPos.x, cursorPos.y, topTran.position.z);

                // учёт границ джойстика

                float distance = Mathf.Sqrt((topTran.localPosition.x * topTran.localPosition.x) + (topTran.localPosition.y * topTran.localPosition.y));

                float ratioDistance = Mathf.Abs(distance / MAX_MOVE_DISTANCE);
                float maxTopPosX = Mathf.Abs(topTran.localPosition.x / ratioDistance);
                float maxTopPosY = Mathf.Abs(topTran.localPosition.y / ratioDistance);

                if (topTran.localPosition.x < -maxTopPosX) topTran.localPosition = new Vector2(-maxTopPosX, topTran.localPosition.y);
                else if (topTran.localPosition.x > maxTopPosX) topTran.localPosition = new Vector2(maxTopPosX, topTran.localPosition.y);

                if (topTran.localPosition.y < -maxTopPosY) topTran.localPosition = new Vector2(topTran.localPosition.x, -maxTopPosY);
                else if (topTran.localPosition.y > maxTopPosY) topTran.localPosition = new Vector2(topTran.localPosition.x, maxTopPosY);

                player.Move(new MoveVector(topTran.localPosition, MAX_MOVE_DISTANCE));

                UpdateFonPosition();
            }
        }

        private void UpdateFonPosition()
        {
            fonTran.localPosition = new Vector3(topTran.localPosition.x / DELTA_FON_MOVING, topTran.localPosition.y / DELTA_FON_MOVING, topTran.position.z);
        }

        private void ResetTopAndFonPosition()
        {
            if (topTran && fonTran)
            {
                topTran.localPosition = new Vector3(0, 0, topTran.localPosition.z);
                fonTran.localPosition = new Vector3(0, 0, topTran.localPosition.z);
            }
        }

        private Vector2 ConvertLocalPositionToMoveVector()
        {
            Vector2 buildMoveVector = new Vector2();

            return buildMoveVector;
        }

        public enum Status
        {
            Sleep,
            Active,
            ToSleep
        }   
    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }
}