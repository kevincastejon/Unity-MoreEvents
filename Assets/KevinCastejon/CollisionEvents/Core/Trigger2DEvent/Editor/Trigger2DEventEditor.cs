using UnityEditor;
using UnityEngine;

namespace KevinCastejon.CollisionEvents
{
    [CustomEditor(typeof(Trigger2DEvent))]
    public class Trigger2DEventEditor : Editor
    {
        private SerializedProperty _onEnter;
        private SerializedProperty _onStay;
        private SerializedProperty _onExit;
        private SerializedProperty _colliders;

        private Trigger2DEvent _script;

        private bool _eventFolded = true;

        private void OnEnable()
        {
            _onEnter = serializedObject.FindProperty("_onEnter");
            _onStay = serializedObject.FindProperty("_onStay");
            _onExit = serializedObject.FindProperty("_onExit");
            _colliders = serializedObject.FindProperty("_colliders");

            _script = target as Trigger2DEvent;
        }

        public override void OnInspectorGUI()
        {
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