using UnityEngine;

namespace SFXTC
{
    // SFX player used to log each changement of state of a SFXEvent
    // (can be used with a default SFXPlayer or with a NullSFXPlayer)
    public class LoggedSFXPlayer : SFXPlayerBase
    {
        // AudioPlayer used to play, stop or pause ClipsData
        SFXPlayerBase wrappedSFXPlayer;

        // Constructor
        public LoggedSFXPlayer(SFXManager SFXManager, SFXPlayerBase wrappedSFXPlayer) : base(SFXManager) 
        {
            this.wrappedSFXPlayer = wrappedSFXPlayer;
        }

        #region Functions
        public override void Play(string name)
        {
            Debug.Log($"Play '{name}'.");
            wrappedSFXPlayer.Play(name);
        }
        public override void Play(SFXEvent SFX)
        {
            Debug.Log($"Play '{SFX.name}'.");
            wrappedSFXPlayer.Play(SFX);
        }

        public override void PlayDelayed(string name, float delay)
        {
            Debug.Log($"Play Delayed '{name}' with {delay}s of delay.");
            wrappedSFXPlayer.PlayDelayed(name, delay);
        }
        public override void PlayDelayed(SFXEvent SFX, float delay)
        {
            Debug.Log($"Play Delayed '{SFX.name}' with {delay}s of delay.");
            wrappedSFXPlayer.PlayDelayed(SFX, delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            Debug.Log($"Play Scheduled '{name}' with {time}s of delay.");
            wrappedSFXPlayer.PlayScheduled(name, time);
        }
        public override void PlayScheduled(SFXEvent SFX, double time)
        {
            Debug.Log($"Play Scheduled '{SFX.name}' with {time}s of delay.");
            wrappedSFXPlayer.PlayScheduled(SFX, time);
        }

        public override void Stop(string name)
        {
            Debug.Log($"Stop '{name}'.");
            wrappedSFXPlayer.Stop(name);
        }
        public override void Stop(SFXEvent SFX)
        {
            Debug.Log($"Stop '{SFX.name}'.");
            wrappedSFXPlayer.Stop(SFX);
        }

        public override void Pause(string name)
        {
            Debug.Log($"Pause '{name}'.");
            wrappedSFXPlayer.Pause(name);
        }
        public override void Pause(SFXEvent SFX)
        {
            Debug.Log($"Pause '{SFX.name}'.");
            wrappedSFXPlayer.Pause(SFX);
        }

        public override void UnPause(string name)
        {
            Debug.Log($"UnPause '{name}'.");
            wrappedSFXPlayer.UnPause(name);
        }
        public override void UnPause(SFXEvent SFX)
        {
            Debug.Log($"UnPause '{SFX.name}'.");
            wrappedSFXPlayer.UnPause(SFX);
        }
        #endregion
    }
}
