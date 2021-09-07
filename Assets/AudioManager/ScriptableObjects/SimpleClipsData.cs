using UnityEngine;

namespace AudioTC
{
    [CreateAssetMenu(fileName = "Default Simple Clips Data", menuName = "Clips Data/Simple Clips Data")]
    public class SimpleClipsData : ClipsData
    {
        #region Variables
        [Tooltip("Select the volume of the clip.\n0 = no sound | 1 = full sound")]
        [Range(0f, 1f)] public float volume = 1f;

        [Tooltip("Select the pan (earing from left or right) of the clip.\n-1 = left | 0 = center | 1 = right")]
        [Range(-1f, 1f)] public float pan = 0f;

        [Tooltip("Select the pitch (lower or higher frequency) of the clip.\n0 = low | 1 = normal | 2 = high")]
        [Range(0f, 2f)] public float pitch = 1f;

        [Tooltip("Select how much the reverb zone affect the clip.\n0 = no reverb | 1 = normal")]
        [Range(0f, 1.1f)] public float reverbZonMix = 1f;
        #endregion

        #region Functions
        // Initialize the audio source when playing (cf Audio Players)
        public override void SetAudioSource()
        {
            base.SetAudioSource();

            source.volume = volume;
            source.panStereo = pan;
            source.pitch = pitch;
            source.reverbZoneMix = reverbZonMix;
        }

        // Used only to preview on the inspector
        public override void Preview(AudioSource source)
        {
            base.Preview(source);

            source.volume = volume;
            source.panStereo = pan;
            source.pitch = pitch;
            source.reverbZoneMix = reverbZonMix;

            source.Play();
        }
        #endregion
    }
}
