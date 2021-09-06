using UnityEngine;

namespace AudioThibaultCastelli
{
    [CreateAssetMenu(fileName = "Default Simple Clips Data", menuName = "Clips Data/Simple Clips Data")]
    public class SimpleClipsData : ClipsData
    {
        #region Variables
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-1f, 1f)] public float pan = 0f;
        [Range(0f, 2f)] public float pitch = 1f;
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
        #endregion
    }
}
