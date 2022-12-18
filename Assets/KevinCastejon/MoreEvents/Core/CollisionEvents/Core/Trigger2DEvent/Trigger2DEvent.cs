using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.CollisionEvents
{
    /// <summary>
    /// Bind triggers message methods to UnityEvents.
    /// </summary>
    public class Trigger2DEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider2D> _onEnter = new UnityEvent<Collider2D>();
        [SerializeField]
        private UnityEvent<Collider2D> _onStay = new UnityEvent<Collider2D>();
        [SerializeField]
        private UnityEvent<Collider2D> _onExit = new UnityEvent<Collider2D>();
        [SerializeField]
        private List<Collider2D> _colliders;
        [SerializeField]
        private bool _useTagFilter;
        [SerializeField]
        private string _tag;

        public int TriggeringCollider2DsCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider2D> OnEnter { get => _onEnter; }
        public UnityEvent<Collider2D> OnStay { get => _onStay; }
        public UnityEvent<Collider2D> OnExit { get => _onExit; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _colliders.Add(other);
            if (isActiveAndEnabled && (!_useTagFilter || other.CompareTag(_tag)))
            {
                _onEnter.Invoke(other);
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (isActiveAndEnabled && (!_useTagFilter || other.CompareTag(_tag)))
            {
                _onStay.Invoke(other);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
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
        public bool HasCollider(Collider2D target)
        {
            return _colliders.Contains(target);
        }
        public int IndexOfCollider(Collider2D collider)
        {
            return _colliders.IndexOf(collider);
        }
        public Collider2D GetCollider(int i)
        {
            return _colliders[i];
        }
    }
}
