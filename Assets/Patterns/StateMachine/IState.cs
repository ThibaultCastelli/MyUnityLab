
namespace StateTC
{
    /// <summary>
    /// Interface of a state.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Get called when entering this state.
        /// </summary>
        void OnEnterState();

        /// <summary>
        /// Get called when exiting this state.
        /// </summary>
        void OnExitState();

        /// <summary>
        /// Simulate the Update method.
        /// </summary>
        void Tick();

        /// <summary>
        /// Simulate the FixedUpdate method.
        /// </summary>
        void FixedTick();
    }
}
