
namespace AudioTC
{
    // Null audio player to disable all audio without error
    public class NullAudioPlayer : IAudioPlayer
    {
        // Constructor
        public NullAudioPlayer(AudioData audioData) : base(audioData) { }

        #region Functions
        public override void Pause(string name) {}

        public override void Play(string name) {}

        public override void PlayDelayed(string name, float delay) {}

        public override void PlayScheduled(string name, double time) {}

        public override void Stop(string name) {}

        public override void UnPause(string name) {}
        #endregion
    }
}
