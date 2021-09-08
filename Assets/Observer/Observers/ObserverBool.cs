using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverBool : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierBool notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<bool> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}