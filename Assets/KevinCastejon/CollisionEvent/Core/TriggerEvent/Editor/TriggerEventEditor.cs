using UnityEditor;
using UnityEngine;

namespace KevinCastejon.CollisionEvents
{
    [CustomEditor(typeof(TriggerEvent))]
    public class TriggerEventEditor : Editor
    {
        private SerializedProperty _onEnter;
        private SerializedProperty _onStay;
        private SerializedProperty _onExit;
        private SerializedProperty _colliders;

        private TriggerEvent _script;

        private bool _eventFolded = true;

        private void OnEnable()
        {
            _onEnter = serializedObject.FindProperty("_onEnter");
            _onStay = serializedObject.FindProperty("_onStay");
            _onExit = serializedObject.FindProperty("_onExit");
            _colliders = serializedObject.FindProperty("_colliders");

            _script = target as TriggerEvent;
        }

        private bool isOneColliderTrigger()
        {
            Collider[] ownColliders = _script.GetComponent<Rigidbody>() == null ? _script.GetComponents<Collider>() : _script.GetComponentsInChildren<Collider>();
            foreach (Collider col in ownColliders)
            {
                if (col.enabled && col.isTrigger)
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnInspectorGUI()
        {
            if (!isOneColliderTrigger())
            {
                EditorGUILayout.HelpBox("TriggerDetector needs at least one enabled collider with 'IsTrigger' checked", MessageType.Error);
                return;
            }
            serializedObject.Update();
            _eventFolded = EditorGUILayout.BeginFoldoutHeaderGroup(_eventFolded, "Trigger events");
            if (_eventFolded)
            {
                EditorGUILayout.PropertyField(_onEnter);
                EditorGUILayout.PropertyField(_onStay);
                EditorGUILayout.PropertyField(_onExit);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            if (_script.isActiveAndEnabled)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(_colliders, new GUIContent("Colliding objects", "List of currently colliding objects"));
                EditorGUI.EndDisabledGroup();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}