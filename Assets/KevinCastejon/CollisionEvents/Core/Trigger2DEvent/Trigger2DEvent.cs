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

        public int TriggeringCollider2DsCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider2D> OnEnter { get => _onEnter; }
        public UnityEvent<Collider2D> OnStay { get => _onStay; }
        public UnityEvent<Collider2D> OnExit { get => _onExit; }
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
        private void OnTriggerEnter2D(Collider2D other)
        {
            _colliders.Add(other);
            _onEnter.Invoke(other);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            _onStay.Invoke(other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_colliders.Contains(other))
            {
                return;
            }

            _colliders.Remove(other);
            _onExit.Invoke(other);
        }
        public bool HasTriggeringCollider(Collider2D target)
        {
            return _colliders.Contains(target);
        }
        public int IndexOfTriggeringCollider(Collider2D collider)
        {
            return _colliders.IndexOf(collider);
        }
        public Collider2D GetTriggeringColliderAt(int i)
        {
            return _colliders[i];
        }
    }
}
