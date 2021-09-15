using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SFXTC
{
    [CreateAssetMenu(fileName = "Default SFX Event", menuName = "Audio/SFX Event")]
    public class SFXEvent : ScriptableObject
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

        [HideInInspector] public bool useRandomVolume;
        [HideInInspector] public float volume = 1f;
        [MinMaxRange(0, 1)]
        [HideInInspector] public RangedFloat randomVolume;

        [HideInInspector] public bool useRandomPan;
        [HideInInspector] [Range(-1f, 1f)] public float pan = 0f;
        [MinMaxRange(-1, 1)]
        [HideInInspector] public RangedFloat randomPan;

        [HideInInspector] public bool useRandomPitch;
        [HideInInspector] [Range(0f, 2f)] public float pitch = 1f;
        [MinMaxRange(0, 2)]
        [HideInInspector] public RangedFloat randomPitch;

        [HideInInspector] public bool useRandomReverbZoneMix;
        [HideInInspector] [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
        [MinMaxRange(0, 1.1f)]
        [HideInInspector] public RangedFloat randomReverbZoneMix;

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

            if (useRandomVolume)
                source.volume = Random.Range(randomVolume.minValue, randomVolume.maxValue);
            else
                source.volume = volume;

            if (useRandomPan)
                source.panStereo = Random.Range(randomPan.minValue, randomPan.maxValue);
            else
                source.panStereo = pan;

            if (useRandomPitch)
                source.pitch = Random.Range(randomPitch.minValue, randomPitch.maxValue);
            else
                source.pitch = pitch;

            if (useRandomReverbZoneMix)
                source.reverbZoneMix = Random.Range(randomReverbZoneMix.minValue, randomReverbZoneMix.maxValue);
            else
                source.reverbZoneMix = reverbZoneMix;
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

            if (useRandomVolume)
                source.volume = Random.Range(randomVolume.minValue, randomVolume.maxValue);
            else
                source.volume = volume;

            if (useRandomPan)
                source.panStereo = Random.Range(randomPan.minValue, randomPan.maxValue);
            else
                source.panStereo = pan;

            if (useRandomPitch)
                source.pitch = Random.Range(randomPitch.minValue, randomPitch.maxValue);
            else
                source.pitch = pitch;

            if (useRandomReverbZoneMix)
                source.reverbZoneMix = Random.Range(randomReverbZoneMix.minValue, randomReverbZoneMix.maxValue);
            else
                source.reverbZoneMix = reverbZoneMix;

            source.Play();
        }

        // Fonctions to use with event system or to use if you have the reference of the object
        public void Play() { SFXLocator.GetSFXPlayer().Play(name); }
        public void PlayDelayed(float delay) { SFXLocator.GetSFXPlayer().PlayDelayed(name, delay); }
        public void PlayScheduled(double time) { SFXLocator.GetSFXPlayer().PlayScheduled(name, time); }
        public void Stop() { SFXLocator.GetSFXPlayer().Stop(name); }
        public void Pause() { SFXLocator.GetSFXPlayer().Pause(name); }
        public void UnPause() { SFXLocator.GetSFXPlayer().UnPause(name); }
        #endregion
    }
}
