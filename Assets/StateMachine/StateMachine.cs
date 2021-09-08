using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateTC
{
    public abstract class StateMachine : MonoBehaviour
    {
        // Keep track of current state
        public IState currentState { get; private set; }
        // Keep track of previous state
        public IState previousState { get; private set; }

        // Change to the desired state
        public void SetState(IState newState)
        {
            // Prevent from switching to a null state or to the same state
            if (newState == null || newState == currentState)
                return;

            if (currentState != null)
            {
                currentState.OnExitState();
                previousState = currentState;
            }

            currentState = newState;
            currentState.OnEnterState();
        }

        // Switch to the previous state
        public void ReverseState()
        {
            if (currentState == null || previousState == null)
                return;

            IState temp = currentState;
            currentState = previousState;
            previousState = temp;
        }
    }
}
