using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MusicTC
{
    /// <summary>
    /// Class to manage all the MusicEvents and the MusicPlayers.
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        #region Variables
        [Header("INFOS")]
        [Tooltip("Select how many layers the MusicPlayer can create.\nIt can't be changed at runtime.\n A high number of layer have an impact on performance.")]
        [SerializeField] [Range(1, 10)] int maxLayerCount = 3;
        [Space]

        [Header("BEHAVIOUR")]
        [Tooltip("Check if you want to disable all Musics")]
        [SerializeField] bool useNullMusicPlayer;
        [Tooltip("Check if you want to log any change of state of Musics")]
        [SerializeField] bool useLoggedMusicPlayer;

        MusicEvent _currentMusicEvent;

        MusicPlayer _musicPlayerA;
        MusicPlayer _musicPlayerB;

        bool _isPlayingA = true;    // Used to check wich MusicPlayer is currently active.

        int _currentLayer;          // Used to track which layer is active on the MusicEvent currently playing.
        #endregion

        #region Properties
        /// <summary>
        /// Represents the MusicPlayer currently active.
        /// </summary>
        public MusicPlayer ActivePlayer => _isPlayingA ? _musicPlayerA : _musicPlayerB;

        /// <summary>
        /// Represents the MusicPlayer currently inactive.
        /// </summary>
        public MusicPlayer InactivePlayer => _isPlayingA ? _musicPlayerB : _musicPlayerA;

        /// <summary>
        /// Represents which layer is active on the MusicEvent currently playing.
        /// </summary>
        public int CurrentLayer => _currentLayer;

        /// <summary>
        /// Represents the maximum layers that MusicEvent can holds.
        /// </summary>
        public int MaxLayerCount => maxLayerCount;
        #endregion

        #region Initialization
        // Singleton Lazy Instantiation
        static MusicManager _instance;
        public static MusicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Search if the GameObject already exist
                    _instance = FindObjectOfType<MusicManager>();

                    if (_instance == null)
                    {
                        // Create the GameObject with a MusicManager component
                        GameObject musicManager = new GameObject("Music_Manager");
                        _instance = musicManager.AddComponent<MusicManager>();

                        DontDestroyOnLoad(musicManager);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // Prevent from having 2 MusicManager at the same time
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            // Prevent the MusicManager to be destroy when changing scene
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Automatically create two child object with MusicPlayer component
            GameObject musicPlayerAGO = new GameObject("Music_Player_A");
            musicPlayerAGO.transform.parent = this.transform;
            _musicPlayerA = musicPlayerAGO.AddComponent<MusicPlayer>();

            GameObject musicPlayerBGO = new GameObject("Music_Player_B");
            musicPlayerBGO.transform.parent = this.transform;
            _musicPlayerB = musicPlayerBGO.AddComponent<MusicPlayer>();
        }

        private void OnEnable()
        {
            // When changing scene, check if the current MusicEvent should stop or not.
            SceneManager.sceneLoaded += StopOnSceneLoad;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= StopOnSceneLoad;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Play a MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to play.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void Play(MusicEvent musicEvent, float fadeTime = -1)
        {
            // Prevent errors
            if (musicEvent == null)
            {
                Debug.LogError("ERROR : The MusicEvent you try to play is null.");
                return;
            }
            else if (musicEvent == ActivePlayer.musicEvent)
            {
                Debug.LogError("ERROR : This MusicEvent is already playing.");
                return;
            }

            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            // Use different behaviour based on the type of player selected
            if (useLoggedMusicPlayer)
                Debug.Log($"Play {musicEvent.name} with a fade time of {fadeTime}s.");
            if (useNullMusicPlayer)
                return;

            // Stop the current MusicPlayer if it is playing
            if (ActivePlayer.musicEvent != null)
                Stop(ActivePlayer.musicEvent, fadeTime);

            // Reset the layer
            _currentLayer = 0;
            
            // Keep track of the new MusicEvent and play it
            _currentMusicEvent = musicEvent;
            ActivePlayer.Play(musicEvent, fadeTime);
        }

        /// <summary>
        /// Replay a MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to replay.</param>
        /// <param name="fadeTime">How much time the fade in/out will take (in seconds).</param>
        public void Replay(MusicEvent musicEvent, float fadeTime = -1)
        {
            // Prevent errors
            if (musicEvent == null)
            {
                Debug.LogError("ERROR : The MusicEvent you try to replay is null.");
                return;
            }
            else if (musicEvent != ActivePlayer.musicEvent)
            {
                Debug.LogError("ERROR : The music event you try to replay is not the same that is currently playing.");
                return;
            }

            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            Replay(fadeTime);
        }

        /// <summary>
        /// Replay a MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="fadeTime">How much time the fade in/out will take (in seconds).</param>
        public void Replay(float fadeTime = -1)
        {
            if (fadeTime < 0)
            {
                fadeTime = _currentMusicEvent.DefaultFadeTime;
            }

            // Use different behaviour based on the type of player selected
            if (useLoggedMusicPlayer)
                Debug.Log($"Replay {musicEvent.name} with a fade time of {fadeTime}s.");
            if (useNullMusicPlayer)
                return;

            // Stop the music event and replay it from the begining while keeping the same current layer
            Stop(ActivePlayer.musicEvent, fadeTime);
            ActivePlayer.Play(musicEvent, fadeTime);
        }

        /// <summary>
        /// Stop a MusicEvent with the given fade out time.
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to stop.</param>
        /// <param name="fadeTime">How much time the fade out will take (in seconds).</param>
        public void Stop(MusicEvent musicEvent, float fadeTime = 0)
        {
            // Prevent errors
            if (musicEvent != ActivePlayer.musicEvent)
            {
                Debug.LogError("ERROR : This music event is not playing.");
                return;
            }

            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            Stop(fadeTime);
        }

        /// <summary>
        /// Stop a MusicEvent with the given fade out time.
        /// </summary>
        /// <param name="fadeTime">How much time the fade out will take (in seconds).</param>
        public void Stop(float fadeTime = -1)
        {
            if (ActivePlayer.musicEvent == null)
            {
                Debug.LogError("ERROR : There is no MusicEvent currently playing.");
                return;
            }

            if (fadeTime < 0)
            {
                fadeTime = _currentMusicEvent.DefaultFadeTime;
            }

            // Use different behaviour based on the type of player selected
            if (useLoggedMusicPlayer)
                Debug.Log($"Stop {_currentMusicEvent.name} with a fade time of {fadeTime}s.");
            if (useNullMusicPlayer)
                return;

            // Stop the active MusicPlayer and make it inactive. The previous inactive MusicPlayer active.
            ActivePlayer.Stop(fadeTime);
            _isPlayingA = !_isPlayingA;
        }

        /// <summary>
        /// Set the layer to play with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to set.</param>
        /// <param name="newLayer">Which layer to play.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void SetLayer(MusicEvent musicEvent, int newLayer, float fadeTime = -1)
        {
            // Prevent from changing the layer of the wrong music event
            if (musicEvent != _currentMusicEvent)
            {
                Debug.Log("ERROR : This music event needs to be playing in order to change its layer.");
                return;
            }

            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            SetLayer(newLayer, fadeTime);
        }

        /// <summary>
        /// Set the layer to play with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="newLayer">Which layer to play.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void SetLayer(int newLayer, float fadeTime = -1)
        {
            if (fadeTime < 0)
            {
                fadeTime = _currentMusicEvent.DefaultFadeTime;
            }

            // Use different behaviour based on the type of player selected
            if (useLoggedMusicPlayer)
                Debug.Log($"Set the layer of {_currentMusicEvent.name} at {newLayer}, with a fade time of {fadeTime}s.");
            if (useNullMusicPlayer)
                return;

            // Increase the layer and apply it to the active MusicPlayer (result depends on the LayerType of the current MusicEvent)
            _currentLayer = Mathf.Clamp(newLayer, 0, maxLayerCount - 1);
            ActivePlayer.Play(_currentMusicEvent, fadeTime);
        }

        /// <summary>
        /// Go to the next layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to set.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void IncreaseLayer(MusicEvent musicEvent, float fadeTime = -1)
        {
            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            SetLayer(musicEvent, _currentLayer + 1, fadeTime);
        }

        /// <summary>
        /// Go to the next layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void IncreaseLayer(float fadeTime = -1)
        {
            if (fadeTime < 0)
            {
                fadeTime = _currentMusicEvent.DefaultFadeTime;
            }

            SetLayer(_currentLayer + 1, fadeTime);
        }

        /// <summary>
        /// Go to the previous layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to set.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void DecreaseLayer(MusicEvent musicEvent, float fadeTime = 0)
        {
            if (fadeTime < 0)
            {
                fadeTime = musicEvent.DefaultFadeTime;
            }

            SetLayer(musicEvent, _currentLayer - 1, fadeTime);
        }

        /// <summary>
        /// Go to the previous layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void DecreaseLayer(float fadeTime = -1)
        {
            if (fadeTime < 0)
            {
                fadeTime = _currentMusicEvent.DefaultFadeTime;
            }

            SetLayer(_currentLayer - 1, fadeTime);
        }

        // Stop the active MusicEvent when changing scene if needed.
        void StopOnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (_currentMusicEvent != null && _currentMusicEvent.StopOnSceneChange)
                Stop(_currentMusicEvent);
        }
        #endregion
    }
}
