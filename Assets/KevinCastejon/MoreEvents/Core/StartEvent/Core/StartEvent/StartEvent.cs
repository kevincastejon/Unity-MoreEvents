using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.StartEvent
{
    public class StartEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onStarted;

        private void Start()
        {
            _onStarted.Invoke();
        }
    }
}
