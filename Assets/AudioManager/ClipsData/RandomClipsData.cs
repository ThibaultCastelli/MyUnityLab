using UnityEngine;

namespace AudioTC
{
    [CreateAssetMenu(fileName = "Default Random Clips Data", menuName = "Clips Data/Random Clips Data")]
    public class RandomClipsData : ClipsData
    {
        #region Variables
        [Tooltip("Select the volume of the clip.\nA random value will be chosen each it will be played.\n0 = no sound | 1 = full sound")]
        [MinMaxRange(0, 1)]
        [SerializeField] RangedFloat volume;

        [Tooltip("Select the pan (earing from left or right) of the clip.\nA random value will be chosen each it will be played.\n-1 = left | 0 = center | 1 = right")]
        [MinMaxRange(-1, 1)]
        [SerializeField] RangedFloat pan;

        [Tooltip("Select the pitch (lower or higher frequency) of the clip.\nA random value will be chosen each it will be played.\n0 = low | 1 = normal | 2 = high")]
        [MinMaxRange(0, 2)]
        [SerializeField] RangedFloat pitch;

        [Tooltip("Select how much the reverb zone affect the clip.\nA random value will be chosen each it will be played.\n0 = no reverb | 1 = normal")]
        [MinMaxRange(0, 1.1f)]
        [SerializeField] RangedFloat reverbZoneMix;
        #endregion

        #region Functions
        // Initialize the audio source when playing (cf Audio Players)
        public override void SetAudioSource()
        {
            base.SetAudioSource();

            source.volume = Random.Range(volume.minValue, volume.maxValue);
            source.panStereo = Random.Range(pan.minValue, pan.maxValue);
            source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
            source.reverbZoneMix = Random.Range(reverbZoneMix.minValue, reverbZoneMix.maxValue);
        }

        // Used only to preview on the inspector
        public override void Preview(AudioSource source)
        {
            base.Preview(source);

            source.volume = Random.Range(volume.minValue, volume.maxValue);
            source.panStereo = Random.Range(pan.minValue, pan.maxValue);
            source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
            source.reverbZoneMix = Random.Range(reverbZoneMix.minValue, reverbZoneMix.maxValue);

            source.Play();
        }
        #endregion
    }
}
