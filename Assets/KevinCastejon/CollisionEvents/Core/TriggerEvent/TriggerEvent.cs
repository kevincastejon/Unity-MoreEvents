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
        [SerializeField]
        private bool _useTagFilter;
        [SerializeField]
        private string _tag;

        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider> OnEnter { get => _onEnter; }
        public UnityEvent<Collider> OnStay { get => _onStay; }
        public UnityEvent<Collider> OnExit { get => _onExit; }
        public bool UseTagFilter { get => _useTagFilter; set => _useTagFilter = value; }
        
        private void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);
            if (isActiveAndEnabled && (!_useTagFilter || other.CompareTag(_tag)))
            {
                _onEnter.Invoke(other);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (isActiveAndEnabled && (!_useTagFilter || other.CompareTag(_tag)))
            {
                _onStay.Invoke(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (!_colliders.Contains(other))
            {
                return;
            }
            _colliders.Remove(other);
            if (isActiveAndEnabled && (!_useTagFilter || other.CompareTag(_tag)))
            {
                _onExit.Invoke(other);
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
        public Collider GetCollider(int i)
        {
            return _colliders[i];
        }
    }
}
