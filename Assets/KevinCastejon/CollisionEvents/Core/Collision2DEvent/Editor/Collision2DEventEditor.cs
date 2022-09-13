using UnityEditor;
using UnityEngine;

namespace KevinCastejon.CollisionEvents
{
    [CustomEditor(typeof(Collision2DEvent))]
    public class Collision2DEventEditor : Editor
    {
        private SerializedProperty _onEnter;
        private SerializedProperty _onStay;
        private SerializedProperty _onExit;
        private SerializedProperty _colliders;
        private SerializedProperty _useTagFilter;
        private SerializedProperty _tag;

        private Collision2DEvent _script;

        private void OnEnable()
        {
            _onEnter = serializedObject.FindProperty("_onEnter");
            _onStay = serializedObject.FindProperty("_onStay");
            _onExit = serializedObject.FindProperty("_onExit");
            _colliders = serializedObject.FindProperty("_colliders");
            _useTagFilter = serializedObject.FindProperty("_useTagFilter");
            _tag = serializedObject.FindProperty("_tag");

            _script = target as Collision2DEvent;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (_tag.stringValue == "")
            {
                _tag.stringValue = UnityEditorInternal.InternalEditorUtility.tags[0];
            }
            _useTagFilter.boolValue = EditorGUILayout.Toggle(new GUIContent("Use tag filter"), _useTagFilter.boolValue);
            EditorGUI.BeginDisabledGroup(!_useTagFilter.boolValue);
            EditorGUI.indentLevel++;
            _tag.stringValue = EditorGUILayout.TagField(new GUIContent("Tag filter"), _tag.stringValue);
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(_onEnter);
            EditorGUILayout.PropertyField(_onStay);
            EditorGUILayout.PropertyField(_onExit);

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