using UnityEngine;
using UnityEngine.Events;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent]
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private bool invokeOnAwake = false;
        [SerializeField] private UnityEvent onTriggerEnter = new UnityEvent();

        private void Start()
        {
            if (invokeOnAwake) onTriggerEnter.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !invokeOnAwake) onTriggerEnter.Invoke();
        }
    }
}