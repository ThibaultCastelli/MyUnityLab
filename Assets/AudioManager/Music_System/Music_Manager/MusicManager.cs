using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicTC
{
    public class MusicManager : MonoBehaviour
    {

        // Get active PlayerMusic
        MusicPlayer _musicPlayerA;
        MusicPlayer _musicPlayerB;

        bool _isPlayingA = true;

        public MusicPlayer ActivePlayer => _isPlayingA ? _musicPlayerA : _musicPlayerB;
        public MusicPlayer InactivePlayer => _isPlayingA ? _musicPlayerB : _musicPlayerA;

        // Get currentLayer
        int _currentLayer;

        // Get maxLayer
        [SerializeField] [Range(1, 10)] int maxLayerCount = 3;
        public int MaxLayerCount => maxLayerCount;

        static MusicManager _instance;
        public static MusicManager Instance
        {
            get
            {
                // Lazy Instantiation
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

        // Dont destroy on load
        // Singleton
        // Create 2 Music Player
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            GameObject musicPlayerAGO = new GameObject("Music_Player_A");
            musicPlayerAGO.transform.parent = this.transform;
            _musicPlayerA = musicPlayerAGO.AddComponent<MusicPlayer>();

            GameObject musicPlayerBGO = new GameObject("Music_Player_B");
            musicPlayerBGO.transform.parent = this.transform;
            _musicPlayerB = musicPlayerBGO.AddComponent<MusicPlayer>();
        }


        // EVERY METHOD CAN BE CALL WITH MUSICEVENT REF OR WITH STRING NAME
        // PlayImmediate method (playImediate on active music player)
        public void PlayImmediately(MusicEvent musicEvent)
        {
            if (musicEvent == null || musicEvent == ActivePlayer.musicEvent)
                return;

            // StopImmediate if active player is playing
            // Set layerCOunt
        }

        // PlayFade method (PlayFade on active music player)
        public void PlayFade(MusicEvent musicEvent)
        {
            if (musicEvent == null || musicEvent == ActivePlayer.musicEvent)
                return;

            // StopFade if active player is playing
            //Set layerCount
        }

        // StopImmediate method (StopImediate on active music player, invert music players and PlayImmediate on new active music player)
        public void StopImmediately()
        {
            if (ActivePlayer.musicEvent == null)
                return;
        }

        // StopFade method (same as before but with fade time)
        public void StopFade()
        {
            if (ActivePlayer.musicEvent == null)
                return;
        }

        // IncreaseLayer (increase the current layer from one and ChangeLayer on active music player)
        public void IncreaseLayer()
        {
            _currentLayer = Mathf.Clamp(++_currentLayer, 0, maxLayerCount - 1);
        }

        // DecreaseLayer (decrease the current layer from one and ChangeLayer on active music player)
        public void DecreaseLayer()
        {
            _currentLayer = Mathf.Clamp(--_currentLayer, 0, maxLayerCount - 1);
        }
    }
}
