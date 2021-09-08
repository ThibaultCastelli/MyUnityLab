
namespace StateTC
{
    public interface IState
    {
        // Automatically get called in the state machine
        void OnEnterState();
        // Automatically get called in the state machine
        void OnExitState();
        // Simulate Update method
        void Tick();
        // Simulate FixedUpdate method
        void FixedTick();
    }
}
