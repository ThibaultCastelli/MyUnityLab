using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverInt : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierInt notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<int> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}
