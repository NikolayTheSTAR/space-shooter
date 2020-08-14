using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fight;
using Zenject;

public class Player : MonoBehaviour, IPlayer
{
    #region Private

    [SerializeField] private PlayerData data;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletCreateDot;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Animator anim;

    [Inject] private Control.IJoystick joystick;
    [Inject] private IBulletController bullets;
    [Inject] private Sound.IAudioController audioController;
    [Inject] private Health.IHeartsController hearts;
    [Inject] private ILevelController level;

    private Transform tran;
    private Status status;

    [Range(0, 1)] private float lastMotorSpeed;
    private float time;

    private float maxSpeed;
    private float maxAudioPitch;
    private float minAudioPitch;
    private float shotTime;

    #endregion // Private

    #region Unity Methodes

    void Start()
    {
        Init();
        SetData(data);
    }

    void Update()
    {
        if (status != Status.Die)
        {

#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                SetStatus(Status.Moving);

                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.D)) Move(Control.Direction.UpRight);
                    else if (Input.GetKey(KeyCode.A)) Move(Control.Direction.UpLeft);
                    else Move(Control.Direction.Up);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.D)) Move(Control.Direction.DownRight);
                    else if (Input.GetKey(KeyCode.A)) Move(Control.Direction.DownLeft);
                    else Move(Control.Direction.Down);
                }
                else if (Input.GetKey(KeyCode.D)) Move(Control.Direction.Right);
                else if (Input.GetKey(KeyCode.A)) Move(Control.Direction.Left);
            }
            else if (joystick.GetStatus() != Control.Joystick.Status.Active && status != Status.FirstSleep) SetStatus(Status.Sleep);

#endif

            UpdateAudioPitch();
            UpdateShotTime();
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Damage();
            col.GetComponent<IEnemy>().Explosion();
        }
        else if (col.CompareTag("EnemyBullet"))
        {
            Damage();
            level.CreateExplosion(transform.position);
            Destroy(col.gameObject);
        }
    }

    #endregion // Unity Methodes

    #region Set

    public void SetStatus(Status value)
    {
        status = value;
    }

    private void SetData(PlayerData value)
    {
        if (value)
        {
            data = value;

            maxSpeed = data.MaxSpeed;
            maxAudioPitch = data.MaxAudioPitch;
            minAudioPitch = data.MinAudioPitch;
            shotTime = data.ShotTime;
        }
    }

    #endregion // Set

    #region Update

    private void UpdateAudioPitch()
    {
        audio.pitch = ((maxAudioPitch - minAudioPitch) * lastMotorSpeed) + minAudioPitch;

        lastMotorSpeed = 0;
    }

    private void UpdateShotTime()
    {
        if (status == Status.Sleep)
        {
            time += Time.deltaTime;

            if (time >= shotTime)
            {
                time = 0;
                Shot();
            }
        }
    }

    #endregion // Update

    #region Move

    public void Move(Control.MoveVector vector)
    {
        float step = Time.deltaTime * maxSpeed;

        tran.Translate(new Vector2(vector.x * step, vector.y * step));

        if (tran.position.x > LevelController.PLAYER_CRIT_DOT_X) tran.position = new Vector2(LevelController.PLAYER_CRIT_DOT_X, tran.position.y);
        else if (tran.position.x < -LevelController.PLAYER_CRIT_DOT_X) tran.position = new Vector2(-LevelController.PLAYER_CRIT_DOT_X, tran.position.y);

        if (tran.position.y > LevelController.PLAYER_CRIT_DOT_Y) tran.position = new Vector2(tran.position.x, LevelController.PLAYER_CRIT_DOT_Y);
        else if (tran.position.y < -LevelController.PLAYER_CRIT_DOT_Y) tran.position = new Vector2(tran.position.x, -LevelController.PLAYER_CRIT_DOT_Y);

        lastMotorSpeed = Mathf.Abs(Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y)));
    }

    public void Move(Control.Direction direction)
    {
        float step = Time.deltaTime * maxSpeed;
        float axisStep = Mathf.Abs(Mathf.Sqrt((step * step) / 2));

        switch(direction)
        {
            case Control.Direction.Up:
                tran.Translate(new Vector2(0, step));
                break;

            case Control.Direction.Down:
                tran.Translate(new Vector2(0, -step));
                break;

            case Control.Direction.Right:
                tran.Translate(new Vector2(step, 0));
                break;

            case Control.Direction.Left:
                tran.Translate(new Vector2(-step, 0));
                break;

            case Control.Direction.UpRight:
                axisStep = Mathf.Abs(Mathf.Sqrt((step * step) / 2));
                tran.Translate(new Vector2(axisStep, axisStep));
                break;

            case Control.Direction.UpLeft:
                axisStep = Mathf.Abs(Mathf.Sqrt((step * step) / 2));
                tran.Translate(new Vector2(-axisStep, axisStep));
                break;

            case Control.Direction.DownRight:
                axisStep = Mathf.Abs(Mathf.Sqrt((step * step) / 2));
                tran.Translate(new Vector2(axisStep, -axisStep));
                break;

            case Control.Direction.DownLeft:
                axisStep = Mathf.Abs(Mathf.Sqrt((step * step) / 2));
                tran.Translate(new Vector2(-axisStep, -axisStep));
                break;
        }

        if (tran.position.x > LevelController.PLAYER_CRIT_DOT_X) tran.position = new Vector2(LevelController.PLAYER_CRIT_DOT_X, tran.position.y);
        else if (tran.position.x < -LevelController.PLAYER_CRIT_DOT_X) tran.position = new Vector2(-LevelController.PLAYER_CRIT_DOT_X, tran.position.y);

        if (tran.position.y > LevelController.PLAYER_CRIT_DOT_Y) tran.position = new Vector2(tran.position.x, LevelController.PLAYER_CRIT_DOT_Y);
        else if (tran.position.y < -LevelController.PLAYER_CRIT_DOT_Y) tran.position = new Vector2(tran.position.x, -LevelController.PLAYER_CRIT_DOT_Y);

        lastMotorSpeed = 1;
    }

    #endregion // Move

    private void Init()
    {
        tran = transform;
    }
    
    public void Shot()
    {
        bullets.CreateBullet(bullet, bulletCreateDot ? bulletCreateDot.position : tran.position);
    }
    
    private void Damage()
    {
        if (anim) anim.SetTrigger("Damage");
        if (data) audioController.SetAudioEffect(data.DamageSound);

        hearts.Reduction();
    }

    public void Die()
    {
        status = Status.Die;
        Destroy(gameObject);
    }

    public enum Status
    {
        FirstSleep, // корабль в покое
        Sleep, // корабль в покое, автоматически делаются выстрелы
        Moving, // корабль в движении
        Die // корабль разрушен
    }   
}