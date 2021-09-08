using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverVector2 : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierVector2 notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<Vector2> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}
