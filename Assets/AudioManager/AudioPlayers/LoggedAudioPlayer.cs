using UnityEngine;

namespace AudioTC
{
    // Audio player used to log each changement of state of a ClipsData
    // (can be used with a default AudioPlayer or with a NullAudioPlayer)
    public class LoggedAudioPlayer : IAudioPlayer
    {
        // AudioPlayer used to play, stop or pause ClipsData
        IAudioPlayer wrappedAudioPlayer;

        // Constructor
        public LoggedAudioPlayer(AudioManager audioData, IAudioPlayer wrappedAudioPlayer) : base(audioData) 
        {
            this.wrappedAudioPlayer = wrappedAudioPlayer;
        }

        #region Functions
        public override void Play(string name)
        {
            Debug.Log($"Play '{name}'.");
            wrappedAudioPlayer.Play(name);
        }
        public override void Play(ClipsData clipData)
        {
            Debug.Log($"Play '{clipData.name}'.");
            wrappedAudioPlayer.Play(clipData);
        }

        public override void PlayDelayed(string name, float delay)
        {
            Debug.Log($"Play Delayed '{name}' with {delay}s of delay.");
            wrappedAudioPlayer.PlayDelayed(name, delay);
        }
        public override void PlayDelayed(ClipsData clipData, float delay)
        {
            Debug.Log($"Play Delayed '{clipData.name}' with {delay}s of delay.");
            wrappedAudioPlayer.PlayDelayed(clipData, delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            Debug.Log($"Play Scheduled '{name}' with {time}s of delay.");
            wrappedAudioPlayer.PlayScheduled(name, time);
        }
        public override void PlayScheduled(ClipsData clipData, double time)
        {
            Debug.Log($"Play Scheduled '{clipData.name}' with {time}s of delay.");
            wrappedAudioPlayer.PlayScheduled(clipData, time);
        }

        public override void Stop(string name)
        {
            Debug.Log($"Stop '{name}'.");
            wrappedAudioPlayer.Stop(name);
        }
        public override void Stop(ClipsData clipData)
        {
            Debug.Log($"Stop '{clipData.name}'.");
            wrappedAudioPlayer.Stop(clipData);
        }

        public override void Pause(string name)
        {
            Debug.Log($"Pause '{name}'.");
            wrappedAudioPlayer.Pause(name);
        }
        public override void Pause(ClipsData clipData)
        {
            Debug.Log($"Pause '{clipData.name}'.");
            wrappedAudioPlayer.Pause(clipData);
        }

        public override void UnPause(string name)
        {
            Debug.Log($"UnPause '{name}'.");
            wrappedAudioPlayer.UnPause(name);
        }
        public override void UnPause(ClipsData clipData)
        {
            Debug.Log($"UnPause '{clipData.name}'.");
            wrappedAudioPlayer.UnPause(clipData);
        }
        #endregion
    }
}
