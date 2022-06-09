// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// namespace Game.Editor
// {
//     [CustomEditor(typeof(ComponentReference))]
//     public class ComponentReferenceInspector : UnityEditor.Editor
//     {
//         private static string s_NameSpace = "Game.Hotfix";
//         private static string s_OutputDictionary = "Assets";
//
//         private string m_TypeName;
//         private ComponentReference m_Target;
//         private SerializedProperty m_ReferenceDatas;
//         private SerializedProperty m_Components;
//
//         private void OnEnable()
//         {
//             m_TypeName = target.name;
//             m_Target = (ComponentReference)target;
//             m_ReferenceDatas = serializedObject.FindProperty("m_ReferenceDatas");
//             m_Components = serializedObject.FindProperty("m_Components");
//         }
//
//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             
//             InputsGUI();
//             
//             serializedObject.ApplyModifiedProperties();
//         }
//         
//         private void ButtonsGUI()
//         {
//             EditorGUILayout.BeginHorizontal();
//
//             if (GUILayout.Button("排序"))
//             {
//                 Sort();
//             }
//
//             if (GUILayout.Button("全部删除"))
//             {
//                 RemoveAll();
//             }
//
//             if (GUILayout.Button("删除空引用"))
//             {
//                 RemoveNull();
//             }
//
//             if (GUILayout.Button("自动绑定组件"))
//             {
//                 AutoBindComponent();
//             }
//
//             if (GUILayout.Button("生成绑定代码"))
//             {
//                 GenAutoBindCode();
//             }
//
//             EditorGUILayout.EndHorizontal();
//         }
//
//         private void InputsGUI()
//         {
//             s_NameSpace = EditorGUILayout.TextField("NameSpace", s_NameSpace);
//             m_TypeName = EditorGUILayout.TextField("TypeName", m_TypeName);
//             EditorGUILayout.BeginHorizontal();
//             s_OutputDictionary = EditorGUILayout.TextField("OutputPath", s_OutputDictionary);
//             if (GUILayout.Button("Select"))
//             {
//                 string directory = EditorUtility.OpenFolderPanel("Select Output Dictionary", s_OutputDictionary, string.Empty);
//                 if (!string.IsNullOrEmpty(directory))
//                 {
//                     s_OutputDictionary = directory;
//                 }
//             }
//             EditorGUILayout.EndHorizontal();
//         }
//
//         private void ListGUI()
//         {
//             
//         }
//         
//         /// <summary>
//         /// 排序
//         /// </summary>
//         private void Sort()
//         {
//             List<ComponentReference.ReferenceData> referenceDatas = new List<ComponentReference.ReferenceData>();
//             foreach (ComponentReference.ReferenceData data in m_Target.m_ReferenceDatas)
//             {
//                 referenceDatas.Add(new ComponentReference.ReferenceData(data.m_Name, data.m_Component));
//             }
//             referenceDatas.Sort((x, y) =>
//             {
//                 return string.Compare(x.m_Name, y.m_Name, StringComparison.Ordinal);
//             });
//
//             m_Target.m_ReferenceDatas.Clear();
//             m_Target.m_ReferenceDatas = referenceDatas;
//
//             SyncBindComs();
//         }
//
//         private void SyncUpdateComponents()
//         {
//             
//         }
//     }
// }
