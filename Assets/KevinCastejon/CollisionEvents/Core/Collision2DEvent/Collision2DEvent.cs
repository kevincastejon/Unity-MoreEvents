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
        [SerializeField]
        private bool _useTagFilter;
        [SerializeField]
        private string _tag;

        public int TriggeringCollidersCount { get => isActiveAndEnabled ? _colliders.Count : 0; }
        public UnityEvent<Collider2D> OnEnter { get => _onEnter; }
        public UnityEvent<Collider2D> OnStay { get => _onStay; }
        public UnityEvent<Collider2D> OnExit { get => _onExit; }
        public bool UseTagFilter { get => _useTagFilter; set => _useTagFilter = value; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _colliders.Add(collision.collider);
            if (isActiveAndEnabled && (!_useTagFilter || collision.collider.CompareTag(_tag)))
            {
                _onEnter.Invoke(collision.collider);
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (isActiveAndEnabled && (!_useTagFilter || collision.collider.CompareTag(_tag)))
            {
                _onStay.Invoke(collision.collider);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
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
