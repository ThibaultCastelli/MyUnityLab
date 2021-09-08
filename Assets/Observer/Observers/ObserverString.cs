using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverString : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierString notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<string> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}
