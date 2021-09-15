
namespace SFXTC
{
    // Null SFX player to disable all audio without error
    public class NullSFXPlayer : SFXPlayerBase
    {
        // Constructor
        public NullSFXPlayer(SFXManager audioData) : base(audioData) { }

        #region Functions
        public override void Pause(string name) {}
        public override void Pause(SFXEvent SFX) {}

        public override void Play(string name) {}
        public override void Play(SFXEvent SFX) {}

        public override void PlayDelayed(string name, float delay) {}
        public override void PlayDelayed(SFXEvent SFX, float delay) {}

        public override void PlayScheduled(string name, double time) {}
        public override void PlayScheduled(SFXEvent SFX, double time) {}

        public override void Stop(string name) {}
        public override void Stop(SFXEvent SFX) {}

        public override void UnPause(string name) {}
        public override void UnPause(SFXEvent SFX) {}
        #endregion
    }
}
