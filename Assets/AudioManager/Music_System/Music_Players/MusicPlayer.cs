using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicTC
{
    public class MusicPlayer : MonoBehaviour
    {
        // Get musicEvent
        public MusicEvent musicEvent;

        // AudioSource for each layer
        AudioSource[] audioSources;

        // Get Coroutines

        // Create audio sources for each layer
        private void Awake()
        {
            SetAudioSources();
        }

        // PlayImmediately method

        // PlayFade method

        // StopImmediately method

        // StopFade method

        // FadeInCoroutine

        // FadeOutCoroutine

        void SetAudioSources()
        {
            for (int i = 0; i < MusicManager.Instance.MaxLayerCount; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = musicEvent.musicLayers[i];
                source.loop = musicEvent.loop;
                source.volume = musicEvent.defaultVolume;
            }
        }
    }
}
