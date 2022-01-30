using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateTC
{
    /// <summary>
    /// Base class to create a State Machine pattern.
    /// </summary>
    public abstract class StateMachine : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Represent the current state of this machine.
        /// </summary>
        public IState currentState { get; private set; }

        /// <summary>
        /// Represent the previous state.
        /// </summary>
        public IState previousState { get; private set; }
        #endregion

        #region Functions
        /// <summary>
        /// Change current state to a new one.
        /// </summary>
        /// <param name="newState">The new state to set.</param>
        public void SetState(IState newState)
        {
            // Prevent from switching to a null state or to the same state
            if (newState == null || newState == currentState)
                return;

            // Exit and save the previous state
            if (currentState != null)
            {
                currentState.OnExitState();
                previousState = currentState;
            }

            // Set and enter the new state
            currentState = newState;
            currentState.OnEnterState();
        }

        /// <summary>
        /// Switch to the previous state.
        /// </summary>
        public void ReverseState()
        {
            if (currentState == null || previousState == null)
                return;

            IState temp = currentState;
            currentState = previousState;
            previousState = temp;
        }
        #endregion
    }
}
