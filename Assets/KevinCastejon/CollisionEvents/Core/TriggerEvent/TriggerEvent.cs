using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.CollisionEvents
{
    /// <summary>
    /// Bind triggers message methods to UnityEvents.
    /// </summary>
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider> _onEnter = new UnityEvent<Collider>();
        [SerializeField]
        private UnityEvent<Collider> _onStay = new UnityEvent<Collider>();
        [SerializeField]
        private UnityEvent<Collider> _onExit = new UnityEvent<Collider>();
        [SerializeField]
        private List<Collider> _colliders;

        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider> OnEnter { get => _onEnter; }
        public UnityEvent<Collider> OnStay { get => _onStay; }
        public UnityEvent<Collider> OnExit { get => _onExit; }
        private void OnEnable()
        {
            for (int i = 0; i < _colliders.Count; i++)
            {
                _onEnter.Invoke(_colliders[i]);
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < _colliders.Count; i++)
            {
                _onExit.Invoke(_colliders[i]);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);
            _onEnter.Invoke(other);
        }
        private void OnTriggerStay(Collider other)
        {
            _onStay.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!_colliders.Contains(other))
            {
                return;
            }

            _colliders.Remove(other);
            _onExit.Invoke(other);
        }
        public bool HasTriggeringCollider(Collider target)
        {
            return _colliders.Contains(target);
        }
        public int IndexOfTriggeringCollider(Collider collider)
        {
            return _colliders.IndexOf(collider);
        }
        public Collider GetTriggeringColliderAt(int i)
        {
            return _colliders[i];
        }
    }
}
