using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int index;

    public int Index
    {
        get
        {
            return index;
        }
    }

    [SerializeField] private float spawnTime = 1;

    public float SpawnTime
    {
        get
        {
            return spawnTime;
        }
    }

    [SerializeField] private List<GameObject> enemyObjects = new List<GameObject>();

    public List<GameObject> EnemyObjects
    {
        get
        {
            return enemyObjects;
        }
    }

    [SerializeField] private GameObject explosionObject;

    public GameObject ExplosionObject
    {
        get
        {
            return explosionObject;
        }
    }

    [SerializeField] private int neededScore = 5000;

    public int NeededScore
    {
        get
        {
            return neededScore;
        }
    }
}