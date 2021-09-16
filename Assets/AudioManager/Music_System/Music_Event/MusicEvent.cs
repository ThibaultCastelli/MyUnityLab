using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MusicTC
{
    public enum LayerType
    {
        Additive,
        Single
    }

    [CreateAssetMenu(fileName = "Default Music Event", menuName = "Audio/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        // Get audio layers
        public AudioClip[] musicLayers;

        // Get audio mixer
        public AudioMixerGroup mixerGroup;

        // Get LayerType (additive or single)
        public LayerType layerType = LayerType.Additive;

        [Range(0, 10)] public int startLayer;

        // Get fade out / in / layer time
        [Range(0.1f, 20f)] public float fadeInTime;
        [Range(0.1f, 20f)] public float fadeOutTime;
        [Range(0.1f, 20f)] public float fadeLayerTime;

        // AudioSource infos
        public bool playOnAwake;
        public bool loop;
        public float defaultVolume;

        // METHODS CALL ON MUSICMANAGER
        // PlayImmediately method

        // PlayFade method
        public void PlayFade()
        {

        }
        // StopImmediately method

        // StopFade method
    }
}
