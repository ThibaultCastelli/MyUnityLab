using System.Collections.Generic;
using UnityEngine;

namespace SFXTC
{
    // Component used to give all the SFXEvents that can be played
    // Also used to set the SFX player given by the SFXLocator
    public class SFXManager : MonoBehaviour
    {
        #region Variables
        [Header("COMPONENTS")]
        public List<SFXEvent> SFXEvents = new List<SFXEvent>();
        [Space]

        [Header("BEHAVIOUR")]
        [Tooltip("Check if you want to disable all audios")]
        [SerializeField] bool useNullAudioPlayer;
        [Tooltip("Check if you want to log any change of state of audios")]
        [SerializeField] bool useLoggedAudioPlayer;

        // Use a Dictionary to acess SFXEvents at constant time
        public Dictionary<string, SFXEvent> SFXs = new Dictionary<string, SFXEvent>();

        // Used to keep only one instance of the class between scene
        // (prevent sounds to be cut during transitions between scenes)
        static SFXManager instance;

        // Used to keep track of the SFX player wanted by the user so it can be changed during runtime
        bool _previousNull;
        bool _previousLog;
        #endregion

        #region Starts & Updates
        private void Awake()
        {
            // Prevent from having more than one SFXManager in the scene
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
            foreach (SFXEvent SFX in SFXEvents)
            {
                // Prevents from having the same SFXEvents twice
                if (SFXs.ContainsKey(SFX.name))
                    continue;

                // Create an audio source for each SFXEvent
                SFX.source = gameObject.AddComponent<AudioSource>();
                SFXs.Add(SFX.name, SFX);

                // Play the audio if it needs to be played on awake
                if (SFX.playOnAwake)
                    SFXLocator.GetSFXPlayer().Play(SFX.name);
            }

            // Initialize flags
            _previousNull = useNullAudioPlayer;
            _previousLog = useLoggedAudioPlayer;
        }

        private void Update()
        {
            // Change the SFX player at runtime
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
                    SFXLocator.SetSFXPlayer(new LoggedSFXPlayer(this, new NullSFXPlayer(this)));
                else
                    SFXLocator.SetSFXPlayer(new LoggedSFXPlayer(this, new SFXPlayer(this)));
            }
            else if (useNullAudioPlayer)
            {
                SFXLocator.SetSFXPlayer(new NullSFXPlayer(this));
            }
            else
                SFXLocator.SetSFXPlayer(new SFXPlayer(this));
        }
        #endregion
    }
}
