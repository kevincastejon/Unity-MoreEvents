using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KevinCastejon.CollisionEvents
{
    /// <summary>
    /// Bind triggers message methods to UnityEvents.
    /// </summary>
    public class Collision2DEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Collider2D> _onEnter = new UnityEvent<Collider2D>();
        [SerializeField]
        private UnityEvent<Collider2D> _onStay = new UnityEvent<Collider2D>();
        [SerializeField]
        private UnityEvent<Collider2D> _onExit = new UnityEvent<Collider2D>();
        [SerializeField]
        private List<Collider2D> _colliders;

        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
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
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _colliders.Add(collision.collider);
            _onEnter.Invoke(collision.collider);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            _onStay.Invoke(collision.collider);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!_colliders.Contains(collision.collider))
            {
                return;
            }

            _colliders.Remove(collision.collider);
            _onExit.Invoke(collision.collider);
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
