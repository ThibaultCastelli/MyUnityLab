using UnityEngine;
using UnityEngine.Audio;

namespace MusicTC
{
    #region Enum
    public enum LayerType
    {
        Additive,
        Single
    }
    #endregion

    [CreateAssetMenu(fileName = "Default Music Event", menuName = "Audio/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        #region Variables
        [Header("COMPONENTS")]
        [Tooltip("A list of audio clips that represent different layers of a music.")]
        [SerializeField] AudioClip[] musicLayers;
        [Tooltip("Mixer's group that will be assign to each music layer.")]
        [SerializeField] AudioMixerGroup mixerGroup;
        [Space]

        [Header("MUSIC INFOS")]
        [Tooltip("The type of layer blend: \nAdditive : All the layer can be play at the same time.\nSingle : Only one layer can be play at the same time.")]
        [SerializeField] LayerType layerType = LayerType.Additive;
        [Tooltip("Select if the layers should automatically replay.")]
        [SerializeField] bool loop = true;
        [Tooltip("Select the default volume for each layer.\n0 = mute | 1 = full sound")]
        [SerializeField] [Range(0, 1)] float defaultVolume = 1;
        #endregion

        #region Properties
        public AudioClip[] MusicLayers => musicLayers;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public LayerType LayerType => layerType;
        public bool Loop => loop;
        public float DefaultVolume => defaultVolume;
        #endregion

        #region Functions
        public void Play(float fadeTime = 0)
        {
            MusicManager.Instance.Play(this, fadeTime);
        }
        #endregion
    }
}
