using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fight;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController, ISceneTranslator
{
    #region Private

    [SerializeField] private Animator anim;

    [Inject] private Sound.IAudioController audioController;
    [Inject] private Score.IScoreController score;
    [Inject] private IBulletController bullets;
    [Inject] private IGameController game;
    [Inject] private IPlayer player;
    [Inject] private IDataController dataController;

    private LevelData data;
    private int index;
    private float spawnTime;
    private List<GameObject> enemyObjects = new List<GameObject>();
    private GameObject explosionObject;
    private int neededScore;
    private float time;
    private List<IEnemy> enemies = new List<IEnemy>();

    private const float ENEMY_CRIT_DOT_X = 3f;
    private const float ENEMY_CRIT_DOT_Y = 6.5f;
    public const float PLAYER_CRIT_DOT_X = 2.75f;
    public const float PLAYER_CRIT_DOT_Y = 5f;

    #endregion // Private
    
    #region Properties

    public string NextSceneName
    {
        get;
        set;
    }

    public Animator TransitionAnim
    {
        get
        {
            return anim;
        }
    }

    #endregion // Properties

    #region Unity Methodes

    void Start()
    {
        SetData(game.GetActiveLevelData());
    }

    void Update()
    {
        TrySpawnEnemy();
        MoveEnemies();
        CheckEnemiesExit();
    }

    #endregion // Unity Methodes

    private void SetData(LevelData value)
    {
        if (value != null)
        {
            data = value;

            index = data.Index;
            spawnTime = data.SpawnTime;
            enemyObjects = data.EnemyObjects;
            explosionObject = data.ExplosionObject;
            neededScore = data.NeededScore;

            score.SetNeededScore(neededScore);
        }
    }

    private void TrySpawnEnemy()
    {
        try
        {
            time += Time.deltaTime;

            if (time > spawnTime)
            {
                enemies.Add(Instantiate(enemyObjects[Random.Range(0, enemyObjects.Count)], new Vector3(Random.Range(-ENEMY_CRIT_DOT_X, ENEMY_CRIT_DOT_X), ENEMY_CRIT_DOT_Y, transform.position.z), Quaternion.identity, transform).GetComponent<IEnemy>());
                time = 0;
            }
        }
        catch
        {
            Debug.Log("Enemy is not created");
        }
    }

    private void CheckEnemiesExit()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].position.y < -ENEMY_CRIT_DOT_Y)
            {
                enemies[i].ExitFromMap();
                enemies.RemoveAt(i);
                i--;
            }
        }
    }

    private void MoveEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            try
            {
                enemies[i].Translate(enemies[i].Direction * Time.deltaTime);
            }
            catch
            {
                enemies.RemoveAt(i);
                i--;
            }
        }
    }

    public void AddScore(int value)
    {
        score.AddScore(value);
    }

    #region Win/Lose

    public void Lose()
    {
        player.Die();
        StartTransitionTo("Lose");
    }

    public void Win()
    {
        Debug.Log("Win");

        if (index > dataController.GetLastCompletedLevel()) dataController.SetLastCompletedLevel(index);

        if (index < game.GetLevelsCount()) game.SetActiveLevelData(game.GetLevelData(index));
        
        StartTransitionTo("Win");
    }

    #endregion // Win/Lose

    #region SceneTransition

    public void StartTransitionTo(string sceneName)
    {
        if (TransitionAnim) TransitionAnim.SetTrigger("Transition");
        NextSceneName = sceneName;
    }

    public void Transition()
    {
        SceneManager.LoadScene(NextSceneName);
    }

    #endregion // SceneTransition
    
    #region Create

    public void CreateBullet(GameObject bulletObject, Vector3 position)
    {
        bullets.CreateBullet(bulletObject, position);
    }
    
    public void CreateExplosion(Vector2 position)
    {
        if (explosionObject) Instantiate(explosionObject, position, Quaternion.identity, transform);
    }

    public void CreateEnemySound(AudioClip clip)
    {
        audioController.SetAudioEffect(clip);
    }

    #endregion // Create
}