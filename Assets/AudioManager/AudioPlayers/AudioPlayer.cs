
namespace AudioTC
{
    // Default audio player
    public class AudioPlayer : IAudioPlayer
    {
        // Constructor
        public AudioPlayer(AudioData audioData) : base(audioData) {}

        #region Functions
        public override void Play(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.Play();
        }

        public override void PlayDelayed(string name, float delay)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.PlayDelayed(delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            ClipsData audio;

            if (SetAudio(name, out audio))
                audio.source.PlayScheduled(time);
        }

        public override void Stop(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.Stop();
        }

        public override void Pause(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.Pause();
        }

        public override void UnPause(string name)
        {
            ClipsData audio;

            if (SetAudio(name, out audio, false))
                audio.source.UnPause();
        }
        #endregion
    }
}
