using UnityEngine;

namespace AudioThibaultCastelli
{
    [CreateAssetMenu(fileName = "Default Random Clips Data", menuName = "Clips Data/Random Clips Data")]
    public class RandomClipsData : ClipsData
    {
        #region Variables
        [Space]
        [Range(0f, 1f)] public float minVolume = 1f;
        [Range(0f, 1f)] public float maxVolume = 1f;

        [Space]
        [Range(-1f, 1f)] public float minPan = 0f;
        [Range(-1f, 1f)] public float maxPan = 0f;
        
        [Space]
        [Range(0f, 2f)] public float minPitch = 1f;
        [Range(0f, 2f)] public float maxPitch = 1f;
        
        [Space]
        [Range(0f, 1.1f)] public float minReverbZonMix = 1f;
        [Range(0f, 1.1f)] public float maxReverbZonMix = 1f;
        #endregion

        #region Functions
        // Initialize the audio source when playing (cf Audio Players)
        public override void SetAudioSource()
        {
            base.SetAudioSource();

            source.volume = Random.Range(minVolume, maxVolume);
            source.panStereo = Random.Range(minPan, maxPan);
            source.pitch = Random.Range(minPitch, maxPitch);
            source.reverbZoneMix = Random.Range(minReverbZonMix, maxReverbZonMix);
        }

        // Used only to preview on the inspector
        public override void Preview(AudioSource source)
        {
            base.Preview(source);

            source.volume = Random.Range(minVolume, maxVolume);
            source.panStereo = Random.Range(minPan, maxPan);
            source.pitch = Random.Range(minPitch, maxPitch);
            source.reverbZoneMix = Random.Range(minReverbZonMix, maxReverbZonMix);

            source.Play();
        }
        #endregion
    }
}
