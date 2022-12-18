using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace KevinCastejon.EditorToolbox
{
    public class CustomEditorWithListGenerator : Editor
    {
        [MenuItem("Assets/EditorTools/Generate Custom Editor With List", true)]
        private static bool GenerateCustomEditorValidation()
        {
            string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
            Type selectedType = AssetDatabase.GetMainAssetTypeAtPath(selectedPath);
            bool isScriptAsset = selectedType == typeof(MonoScript);
            return isScriptAsset;
        }

        [MenuItem("Assets/EditorTools/Generate Custom Editor With List")]
        private static void GenerateCustomEditor()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = path.Substring(6);
            path = path.Substring(0, path.LastIndexOf("/"));
            MonoScript script = (MonoScript)Selection.activeObject;
            Type scriptClass;
            SerializedObject so;
            GameObject go = null;
            bool isScriptable = false;
            if (script.GetClass().IsSubclassOf(typeof(ScriptableObject)))
            {
                scriptClass = script.GetClass();
                so = new SerializedObject(CreateInstance(scriptClass));
                isScriptable = true;
            }
            else if (script.GetClass().IsSubclassOf(typeof(MonoBehaviour)))
            {
                go = new GameObject("CustomEditorGenerator");
                scriptClass = script.GetClass();
                Component cpt = go.AddComponent(scriptClass);
                so = new SerializedObject(cpt);
            }
            else
            {
                Debug.LogError("CustomEditorGenerator only works with MonoBehaviour or ScriptableObject script assets");
                return;
            }

            List<string> propsNames = new List<string>();
            List<string> symbolicNames = new List<string>();
            List<bool> areLists = new List<bool>();
            SerializedProperty sp = so.GetIterator();
            if (sp.NextVisible(true))
            {
                do
                {
                    if (sp.name != "m_Script")
                    {
                        propsNames.Add(sp.name);
                        symbolicNames.Add(sp.displayName.Replace(" ", ""));
                        areLists.Add(sp.isArray);
                    }
                }
                while (sp.NextVisible(false));
            }
            if (!isScriptable)
            {
                DestroyImmediate(go);
            }
            if (!Directory.Exists(Application.dataPath + path + "/Editor"))
            {
                AssetDatabase.CreateFolder("Assets" + path, "Editor");
            }
            string copyPath = Application.dataPath + path + "/Editor/" + scriptClass.Name + "Editor.cs";
            if (!File.Exists(copyPath))
            { // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine($"using System.Collections;");
                    outfile.WriteLine($"using System.Collections.Generic;");
                    outfile.WriteLine($"using UnityEngine;");
                    outfile.WriteLine($"using UnityEditor;");
                    outfile.WriteLine($"using UnityEditorInternal;");
                    outfile.WriteLine($"");
                    outfile.WriteLine($"[CustomEditor(typeof({scriptClass.Name}))]");
                    outfile.WriteLine($"public class {scriptClass.Name}Editor : Editor");
                    outfile.WriteLine($"{{");
                    for (int i = 0; i < propsNames.Count; i++)
                    {
                        string propName = propsNames[i];
                        if (areLists[i])
                        {
                            outfile.WriteLine($"    private SerializedProperty {propName};");
                            outfile.WriteLine($"    private ReorderableList {propName}EditorList;");
                        }
                        else
                        {
                            outfile.WriteLine($"    private SerializedProperty {propName};");
                        }
                    }
                    outfile.WriteLine($"");
                    outfile.WriteLine($"    private {scriptClass.Name} _object;");
                    outfile.WriteLine($"");
                    outfile.WriteLine($"    private void OnEnable()");
                    outfile.WriteLine($"    {{");
                    for (int i = 0; i < propsNames.Count; i++)
                    {
                        string propName = propsNames[i];
                        string symbolName = symbolicNames[i];
                        if (areLists[i])
                        {
                            outfile.WriteLine($"        {propName} = serializedObject.FindProperty(\"{propName}\");");
                            outfile.WriteLine($"        {propName}EditorList = new ReorderableList(serializedObject, {propName}, true, true, true, true);");
                            outfile.WriteLine($"        {propName}EditorList.drawHeaderCallback = {symbolName}DrawHeaderCallback;");
                            outfile.WriteLine($"        {propName}EditorList.drawElementCallback = {symbolName}DrawElementCallback;");
                            outfile.WriteLine($"        {propName}EditorList.elementHeightCallback = {symbolName}ElementHeightCallback;");
                            outfile.WriteLine($"        //{propName}EditorList.onCanAddCallback = {symbolName}OnCanAddCallback;");
                            outfile.WriteLine($"        //{propName}EditorList.onAddCallback = {symbolName}OnAddCallback;");
                            outfile.WriteLine($"        //{propName}EditorList.onCanRemoveCallback = {symbolName}OnCanRemoveCallback;");
                            outfile.WriteLine($"        //{propName}EditorList.onRemoveCallback = {symbolName}OnRemoveCallback;");
                            outfile.WriteLine($"        //{propName}EditorList.drawFooterCallback = {symbolName}DrawFooterCallback;");
                        }
                        else
                        {
                            outfile.WriteLine($"        {propName} = serializedObject.FindProperty(\"{propName}\");");
                        }
                    }
                    outfile.WriteLine($"");
                    outfile.WriteLine($"        _object = ({scriptClass.Name})target;");
                    outfile.WriteLine($"    }}");
                    outfile.WriteLine($"");
                    outfile.WriteLine($"    public override void OnInspectorGUI()");
                    outfile.WriteLine($"    {{");
                    outfile.WriteLine($"        serializedObject.Update();");
                    outfile.WriteLine($"");
                    for (int i = 0; i < propsNames.Count; i++)
                    {
                        string propName = propsNames[i];
                        if (areLists[i])
                        {
                            outfile.WriteLine($"        {propName}EditorList.DoLayoutList();");
                        }
                        else
                        {
                            outfile.WriteLine($"        EditorGUILayout.PropertyField({propName});");
                        }
                    }
                    outfile.WriteLine($"");
                    outfile.WriteLine($"        serializedObject.ApplyModifiedProperties();");
                    outfile.WriteLine($"    }}");
                    for (int i = 0; i < propsNames.Count; i++)
                    {
                        if (areLists[i])
                        {
                            string propName = propsNames[i];
                            string symbolName = symbolicNames[i];
                            outfile.WriteLine($"    private void {symbolName}DrawHeaderCallback(Rect rect)");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"        EditorGUI.LabelField(rect, {propName}.displayName);");
                            outfile.WriteLine($"    }}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    private void {symbolName}DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"        EditorGUI.PropertyField(rect, {propName}.GetArrayElementAtIndex(index));");
                            outfile.WriteLine($"    }}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    private float {symbolName}ElementHeightCallback(int index)");
                            outfile.WriteLine($"    {{");
                            outfile.WriteLine($"        return EditorGUI.GetPropertyHeight({propName}.GetArrayElementAtIndex(index));");
                            outfile.WriteLine($"    }}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    //private bool {symbolName}OnCanAddCallback(ReorderableList list)");
                            outfile.WriteLine($"    //{{");
                            outfile.WriteLine($"    //    return true;");
                            outfile.WriteLine($"    //}}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    //private void {symbolName}OnAddCallback(ReorderableList list)");
                            outfile.WriteLine($"    //{{");
                            outfile.WriteLine($"    //");
                            outfile.WriteLine($"    //}}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    //private bool {symbolName}OnCanRemoveCallback(ReorderableList list)");
                            outfile.WriteLine($"    //{{");
                            outfile.WriteLine($"    //    return true;");
                            outfile.WriteLine($"    //}}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    //private void {symbolName}OnRemoveCallback(ReorderableList list)");
                            outfile.WriteLine($"    //{{");
                            outfile.WriteLine($"    //");
                            outfile.WriteLine($"    //}}");
                            outfile.WriteLine($"");
                            outfile.WriteLine($"    //private void {symbolName}DrawFooterCallback(Rect rect)");
                            outfile.WriteLine($"    //{{");
                            outfile.WriteLine($"    //");
                            outfile.WriteLine($"    //}}");
                        }
                    }
                    outfile.WriteLine($"}}");
                }
                AssetDatabase.Refresh();
                Debug.Log("Editor file created at " + copyPath);
            }
            else
            {
                Debug.LogError("The file " + copyPath + " already exists!");
            }
        }
    }
}