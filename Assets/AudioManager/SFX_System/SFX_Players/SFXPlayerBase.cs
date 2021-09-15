using UnityEngine;

namespace SFXTC
{
    // Base class for all SFX players
    public abstract class SFXPlayerBase
    {
        // Used to get access to the dictionary and list of SFXEvent
        SFXManager _SFXManager;

        // Constructor
        public SFXPlayerBase(SFXManager SFXManager)
        {
            _SFXManager = SFXManager;
        }

        #region Abstract Methods
        public abstract void Play(string name);
        public abstract void Play(SFXEvent SFXEvent);
        public abstract void PlayDelayed(string name, float delay);
        public abstract void PlayDelayed(SFXEvent SFXEvent, float delay);
        public abstract void PlayScheduled(string name, double time);
        public abstract void PlayScheduled(SFXEvent SFXEvent, double time);
        public abstract void Stop(string name);
        public abstract void Stop(SFXEvent SFXEvent);
        public abstract void Pause(string name);
        public abstract void Pause(SFXEvent SFXEvent);
        public abstract void UnPause(string name);
        public abstract void UnPause(SFXEvent SFXEvent);
        #endregion

        #region Functions
        // Return a SFXEvent
        public SFXEvent GetSFXEvent(string name)
        {
            SFXEvent audio;

            if (!_SFXManager.SFXs.TryGetValue(name, out audio))
            {
                Debug.LogError($"ERROR : '{name}' does not exist.");
                return null;
            }

            return audio;
        }

        // Stop all audio source
        public void StopAllSFX()
        {
            foreach (SFXEvent audio in _SFXManager.SFXEvents)
                audio.source.Stop();
        }

        // Check if the SFXEvent exist and set the audio source if needed
        protected bool SetSFX(string name, out SFXEvent SFX, bool setAudioSource = true)
        {
            if (!_SFXManager.SFXs.TryGetValue(name, out SFX))
            {
                Debug.LogError($"ERROR : '{name}' does not exist.");
                return false;
            }

            if (setAudioSource)
                SFX.SetAudioSource();
            return true;
        }
        protected bool SetSFX(SFXEvent SFXEvent, bool setAudioSource = true)
        {
            if (!_SFXManager.SFXs.ContainsKey(SFXEvent.name))
            {
                Debug.LogError($"ERROR : '{SFXEvent.name}' does not exist.");
                return false;
            }

            if (setAudioSource)
                SFXEvent.SetAudioSource();
            return true;
        }
        #endregion
    }
}
