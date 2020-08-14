using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float maxSpeed = 7;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    [SerializeField] private float maxAudioPitch = 2;

    public float MaxAudioPitch
    {
        get
        {
            return maxAudioPitch;
        }
    }

    [SerializeField] private float minAudioPitch = 1;

    public float MinAudioPitch
    {
        get
        {
            return minAudioPitch;
        }
    }

    [SerializeField] private float shotTime = 0.25f;

    public float ShotTime
    {
        get
        {
            return shotTime;
        }
    }

    [SerializeField] private AudioClip damageSound;

    public AudioClip DamageSound
    {
        get
        {
            return damageSound;
        }
    }

}