using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioTC
{
    public abstract class ClipsData : ScriptableObject
    {
        #region Variables
        [TextArea] public string description;

        [Header("COMPONENTS")]
        [Tooltip("List of clips to be played.\n Only one will be randomly selected when playing.")]
        public List<AudioClip> clips;

        [Tooltip("Mixer's group that will be assign to the clip.")]
        public AudioMixerGroup mixerGroup;
        [Space]

        [Header("INFOS")]
        [Tooltip("Select if the clip should be playing automatically on start.")]
        public bool playOnAwake;

        [Tooltip("Select if the clip should automatically replay.")]
        public bool loop;

        [Tooltip("Select if the clip should be mute.")]
        public bool mute;

        [Tooltip("Select if the clip should ignore the effects applied to his audio source.")]
        public bool bypassEffects;

        [Tooltip("Select if the clip should ignore the reverb zones")]
        public bool bypassReverbZones;
        [Space]

        [Header("SPECS")]
        [Tooltip("Select the priority of a clip.\nA clip with a low value will have priority on a clip with a high value.\n0 = Highest priority | 256 = Lowest priority")]
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

        // Used only to preview on the inspector
        public virtual void Preview(AudioSource source)
        {
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

        // Fonctions to use with event system or to use if you have the reference of the object
        public void Play() { AudioLocator.GetAudioPlayer().Play(name); }
        public void PlayDelayed(float delay) { AudioLocator.GetAudioPlayer().PlayDelayed(name, delay); }
        public void PlayScheduled(double time) { AudioLocator.GetAudioPlayer().PlayScheduled(name, time); }
        public void Stop() { AudioLocator.GetAudioPlayer().Stop(name); }
        public void Pause() { AudioLocator.GetAudioPlayer().Pause(name); }
        public void UnPause() { AudioLocator.GetAudioPlayer().UnPause(name); }
        #endregion
    }
}
