using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class AudioController : MonoBehaviour, IAudioController
    {
        [SerializeField] private AudioSource effectSource;

        public void SetAudioEffect(AudioClip clip)
        {
            if (effectSource)
            {
                effectSource.Stop();
                effectSource.clip = clip;
                effectSource.Play();
            }
        }
    }
}