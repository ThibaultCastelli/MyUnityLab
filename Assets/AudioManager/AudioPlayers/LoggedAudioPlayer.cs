using UnityEngine;

namespace AudioThibaultCastelli
{
    // Audio player used to log each changement of state of a ClipsData
    // (can be used with a default AudioPlayer or with a NullAudioPlayer)
    public class LoggedAudioPlayer : IAudioPlayer
    {
        // AudioPlayer used to play, stop or pause ClipsData
        IAudioPlayer wrappedAudioPlayer;

        // Constructor
        public LoggedAudioPlayer(AudioData audioData, IAudioPlayer wrappedAudioPlayer) : base(audioData) 
        {
            this.wrappedAudioPlayer = wrappedAudioPlayer;
        }

        #region Functions
        public override void Play(string name)
        {
            Debug.Log($"Play '{name}'.");
            wrappedAudioPlayer.Play(name);
        }

        public override void PlayDelayed(string name, float delay)
        {
            Debug.Log($"Play Delayed '{name}' with {delay}s of delay.");
            wrappedAudioPlayer.PlayDelayed(name, delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            Debug.Log($"Play Scheduled '{name}' with {time}s of delay.");
            wrappedAudioPlayer.PlayScheduled(name, time);
        }

        public override void Stop(string name)
        {
            Debug.Log($"Stop '{name}'.");
            wrappedAudioPlayer.Stop(name);
        }

        public override void Pause(string name)
        {
            Debug.Log($"Pause '{name}'.");
            wrappedAudioPlayer.Pause(name);
        }

        public override void UnPause(string name)
        {
            Debug.Log($"UnPause '{name}'.");
            wrappedAudioPlayer.UnPause(name);
        }
        #endregion
    }
}
