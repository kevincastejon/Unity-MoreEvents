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
        [SerializeField]
        private bool _useTagFilter;
        [SerializeField]
        private string _tag;

        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider> OnEnter { get => _onEnter; }
        public UnityEvent<Collider> OnStay { get => _onStay; }
        public UnityEvent<Collider> OnExit { get => _onExit; }
        public bool UseTagFilter { get => _useTagFilter; set => _useTagFilter = value; }

        private void OnCollisionEnter(Collision collision)
        {
            _colliders.Add(collision.collider);
            if (isActiveAndEnabled && (!_useTagFilter || collision.collider.CompareTag(_tag)))
            {
                _onEnter.Invoke(collision.collider);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (isActiveAndEnabled && (!_useTagFilter || collision.collider.CompareTag(_tag)))
            {
                _onStay.Invoke(collision.collider);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (!_colliders.Contains(collision.collider))
            {
                return;
            }
            _colliders.Remove(collision.collider);
            if (isActiveAndEnabled && (!_useTagFilter || collision.collider.CompareTag(_tag)))
            {
                _onExit.Invoke(collision.collider);
            }
        }
        public bool HasCollider(Collider target)
        {
            return _colliders.Contains(target);
        }
        public int IndexOfCollider(Collider collider)
        {
            return _colliders.IndexOf(collider);
        }
        public Collider GetCollider(int index)
        {
            return _colliders[index];
        }
    }
}
