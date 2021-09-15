
namespace SFXTC
{
    // Default SFX player
    public class SFXPlayer : SFXPlayerBase
    {
        // Constructor
        public SFXPlayer(SFXManager audioData) : base(audioData) {}

        #region Functions
        public override void Play(string name)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX))
                SFX.source.Play();
        }
        public override void Play(SFXEvent SFXEvent)
        {
            if (SetSFX(SFXEvent))
                SFXEvent.source.Play();
        }

        public override void PlayDelayed(string name, float delay)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX))
                SFX.source.PlayDelayed(delay);
        }
        public override void PlayDelayed(SFXEvent SFXEvent, float delay)
        {
            if (SetSFX(SFXEvent))
                SFXEvent.source.PlayDelayed(delay);
        }

        public override void PlayScheduled(string name, double time)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX))
                SFX.source.PlayScheduled(time);
        }
        public override void PlayScheduled(SFXEvent SFXEvent, double time)
        {
            if (SetSFX(SFXEvent))
                SFXEvent.source.PlayScheduled(time);
        }

        public override void Stop(string name)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX, false))
                SFX.source.Stop();
        }
        public override void Stop(SFXEvent SFXEvent)
        {
            if (SetSFX(SFXEvent, false))
                SFXEvent.source.Stop();
        }

        public override void Pause(string name)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX, false))
                SFX.source.Pause();
        }
        public override void Pause(SFXEvent SFXEvent)
        {
            if (SetSFX(SFXEvent, false))
                SFXEvent.source.Pause();
        }

        public override void UnPause(string name)
        {
            SFXEvent SFX;

            if (SetSFX(name, out SFX, false))
                SFX.source.UnPause();
        }
        public override void UnPause(SFXEvent SFXEvent)
        {
            if (SetSFX(SFXEvent, false))
                SFXEvent.source.UnPause();
        }
        #endregion
    }
}
