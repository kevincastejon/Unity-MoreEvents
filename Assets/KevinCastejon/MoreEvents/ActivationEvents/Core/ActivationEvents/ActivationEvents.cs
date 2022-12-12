using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.ActivationEvents
{
    public class ActivationEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onActivated;
        [SerializeField] private UnityEvent _onDeactivated;
        [SerializeField] private UnityEvent<bool> _onChange;

        private void OnEnable()
        {
            _onActivated.Invoke();
            _onChange.Invoke(true);
        }
        private void OnDisable()
        {
            _onDeactivated.Invoke();
            _onChange.Invoke(false);
        }
    }
}
