using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverFloat : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierFloat notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<float> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}

