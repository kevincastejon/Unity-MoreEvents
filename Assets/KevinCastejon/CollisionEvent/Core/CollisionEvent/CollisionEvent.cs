using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.CollisionEvents
{
    /// <summary>
    /// Bind triggers message methods to UnityEvents.
    /// </summary>
    public class CollisionEvent : MonoBehaviour
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
        private void OnCollisionEnter(Collision collision)
        {
            _colliders.Add(collision.collider);
            _onEnter.Invoke(collision.collider);
        }
        private void OnCollisionStay(Collision collision)
        {
            _onStay.Invoke(collision.collider);
        }
        private void OnCollisionExit(Collision collision)
        {
            if (!_colliders.Contains(collision.collider))
            {
                return;
            }

            _colliders.Remove(collision.collider);
            _onExit.Invoke(collision.collider);
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
