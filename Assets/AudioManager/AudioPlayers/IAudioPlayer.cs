using UnityEngine;

namespace AudioTC
{
    // Base class for all audio players
    public abstract class IAudioPlayer
    {
        // Used to get access to the dictionary and list of ClipsData
        AudioData audioData;

        // Constructor
        public IAudioPlayer(AudioData audioData)
        {
            this.audioData = audioData;
        }

        #region Abstract Methods
        public abstract void Play(string name);
        public abstract void PlayDelayed(string name, float delay);
        public abstract void PlayScheduled(string name, double time);
        public abstract void Stop(string name);
        public abstract void Pause(string name);
        public abstract void UnPause(string name);
        #endregion

        #region Functions
        // Return a ClipsData
        public ClipsData GetClipsData(string name)
        {
            ClipsData audio;

            if (!audioData.audios.TryGetValue(name, out audio))
            {
                Debug.LogError($"ERROR : '{name}' does not exist.");
                return null;
            }

            return audio;
        }

        // Stop all audio source
        public void StopAllAudio()
        {
            foreach (ClipsData audio in audioData.audioDatas)
                audio.source.Stop();
        }

        // Check if the ClipsData exist and set the audio source if needed
        protected bool SetAudio(string name, out ClipsData audio, bool setAudioSource = true)
        {
            if (!audioData.audios.TryGetValue(name, out audio))
            {
                Debug.LogError($"ERROR : '{name}' does not exist.");
                return false;
            }

            if (setAudioSource)
                audio.SetAudioSource();
            return true;
        }
        #endregion
    }
}
