using UnityEngine;
using UnityEngine.Events;

namespace ObserverTC
{
    public class ObserverVector3 : MonoBehaviour
    {
        [Tooltip("The Notifier to subscribe.")]
        [SerializeField] NotifierVector3 notifier;

        [Tooltip("The reponse (function) to do when notify.")]
        public UnityEvent<Vector3> response;

        // Add to the notifier's list of observers
        private void OnEnable() => notifier.Subscribe(this);
        // remove from the notifier's list of observers
        private void OnDisable() => notifier.Unsubscribe(this);
    }
}
