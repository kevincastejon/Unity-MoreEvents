using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.UpdateEvents
{
    public class UpdateEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _onUpdate;
        [SerializeField] private UnityEvent<float> _onFixedUpdate;
        [SerializeField] private UnityEvent<float> _onLateUpdate;

        private void Update()
        {
            _onUpdate.Invoke(Time.deltaTime);
        }
        private void FixedUpdate()
        {
            _onFixedUpdate.Invoke(Time.deltaTime);            
        }
        private void LateUpdate()
        {
            _onLateUpdate.Invoke(Time.deltaTime);            
        }
    }
}
