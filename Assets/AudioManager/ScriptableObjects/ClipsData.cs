using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioThibaultCastelli
{
    public abstract class ClipsData : ScriptableObject
    {
        #region Variables
        [TextArea] public string description;

        [Header("COMPONENTS")]
        public List<AudioClip> clips;
        public AudioMixerGroup mixerGroup;
        [Space]

        [Header("INFOS")]
        public bool playOnAwake;
        public bool loop;
        public bool mute;
        public bool bypassEffects;
        public bool bypassReverbZones;
        [Space]

        [Header("SPECS")]
        [Range(0, 256)] public int priority = 128;

        [HideInInspector] public AudioSource source;
        #endregion

        #region Functions
        // Initialize the audio source when playing (cf Audio Players)
        public virtual void SetAudioSource()
        {
            if (source == null)
            {
                Debug.LogError($"ERROR : There is no source for '{name}'.");
                return;
            }
            if (clips.Count == 0)
            {
                Debug.LogError($"ERROR : There is no audio clip for '{name}'.");
                return;
            }

            source.clip = clips[Random.Range(0, clips.Count)];
            source.outputAudioMixerGroup = mixerGroup;

            source.playOnAwake = playOnAwake;
            source.loop = loop;
            source.mute = mute;
            source.bypassEffects = bypassEffects;
            source.bypassReverbZones = bypassReverbZones;

            source.priority = priority;
        }
        #endregion
    }
}
