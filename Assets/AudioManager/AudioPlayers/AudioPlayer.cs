
namespace AudioTC
{
    // Default audio player
    public class AudioPlayer : IAudioPlayer
    {
        // Constructor
        public AudioPlayer(AudioManager audioData) : base(audioData) {}

        #region Functions
        public override void Play(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.Play();
        }
        public override void Play(ClipsData clipData)
        {
            if (SetAudio(clipData))
                clipData.source.Play();
        }

        public override void PlayDelayed(string name, float delay)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.PlayDelayed(delay);
        }
        public override void PlayDelayed(ClipsData clipData, float delay)
        {
            if (SetAudio(clipData))
                clipData.source.PlayDelayed(delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.PlayScheduled(time);
        }
        public override void PlayScheduled(ClipsData clipData, double time)
        {
            if (SetAudio(clipData))
                clipData.source.PlayScheduled(time);
        }

        public override void Stop(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.Stop();
        }
        public override void Stop(ClipsData clipData)
        {
            if (SetAudio(clipData, false))
                clipData.source.Stop();
        }

        public override void Pause(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.Pause();
        }
        public override void Pause(ClipsData clipData)
        {
            if (SetAudio(clipData, false))
                clipData.source.Pause();
        }

        public override void UnPause(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.UnPause();
        }
        public override void UnPause(ClipsData clipData)
        {
            if (SetAudio(clipData, false))
                clipData.source.UnPause();
        }
        #endregion
    }
}
