using System.Collections.Generic;
using UnityEngine;

namespace AudioTC
{
    // Component used to give all the ClipsData that can be played
    // Also used to set the audio player given by the AudioLocator
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [Header("COMPONENTS")]
        public List<ClipsData> audioDatas = new List<ClipsData>();
        [Space]

        [Header("BEHAVIOUR")]
        [Tooltip("Check if you want to disable all audios")]
        [SerializeField] bool useNullAudioPlayer;
        [Tooltip("Check if you want to log any change of state of audios")]
        [SerializeField] bool useLoggedAudioPlayer;

        // Use a Dictionary to acess ClipsData at constant time
        public Dictionary<string, ClipsData> audios = new Dictionary<string, ClipsData>();

        // Used to keep only one instance of the class between scene
        // (prevent sounds to be cut during transitions between scenes)
        static AudioManager instance;

        // Used to keep track of the audio player wanted by the user so it can be changed during runtime
        bool _previousNull;
        bool _previousLog;
        #endregion

        #region Starts & Updates
        private void Awake()
        {
            // Prevent from having more than one AudioPlayer in the scene
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Provide the service locator
            SetProvider();

            // Initialize the Dictionary
            foreach (ClipsData audio in audioDatas)
            {
                // Prevents from having the same ClipsData twice
                if (audios.ContainsKey(audio.name))
                    continue;

                // Create an audio source for each audio clip
                audio.source = gameObject.AddComponent<AudioSource>();
                audios.Add(audio.name, audio);

                // Play the audio if it needs to be played on awake
                if (audio.playOnAwake)
                    AudioLocator.GetAudioPlayer().Play(audio.name);
            }

            // Initialize flags
            _previousNull = useNullAudioPlayer;
            _previousLog = useLoggedAudioPlayer;
        }

        private void Update()
        {
            // Change the audio player at runtime
            if (_previousNull != useNullAudioPlayer)
                SetProvider();
            if (_previousLog != useLoggedAudioPlayer)
                SetProvider();

            // Update flags
            _previousNull = useNullAudioPlayer;
            _previousLog = useLoggedAudioPlayer;
        }
        #endregion

        #region Functions
        // Set the service provider for the audio locator
        void SetProvider()
        {
            if (useLoggedAudioPlayer)
            {
                if (useNullAudioPlayer)
                    AudioLocator.SetAudioPlayer(new LoggedAudioPlayer(this, new NullAudioPlayer(this)));
                else
                    AudioLocator.SetAudioPlayer(new LoggedAudioPlayer(this, new AudioPlayer(this)));
            }
            else if (useNullAudioPlayer)
            {
                AudioLocator.SetAudioPlayer(new NullAudioPlayer(this));
            }
            else
                AudioLocator.SetAudioPlayer(new AudioPlayer(this));
        }
        #endregion
    }
}
