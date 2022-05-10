using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace D_Utilities.Storages
{
#if UNITY_EDITOR
    [CustomEditor(typeof(StringWeightedStorage))]
    public class StringWeightedStorageEditor : StringStorageEditorBase
    {
        private SerializedProperty percentageList;
        private SerializedProperty elementList;

        private float[] percentageBuffer;

        private void OnEnable()
        {
            percentageList = serializedObject.FindProperty("percentages");
            elementList = serializedObject.FindProperty("elements");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawImportButton();

            percentageBuffer = percentageList.GetAsArray<float>();

            if (percentageBuffer.Sum() != 100f)
            {
                EditorGUILayout.HelpBox("Percentages do not cover all cases", MessageType.Warning);
            }
        }

        /*public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(elementList, GUILayout.Width(160f));
            EditorGUILayout.PropertyField(percentageList, GUILayout.Width(180f));
            EditorGUILayout.EndHorizontal();

            float percentageSum = 0f;

            for (int i = 0; i < percentageList.arraySize; i++)
            {
                percentageSum += percentageList.GetArrayElementAtIndex(i).floatValue;
            }

            if (percentageSum != 100f)
            {
                EditorGUILayout.HelpBox("Percentages do not cover all cases", MessageType.Warning);
            }

            DrawImportButton();
            serializedObject.Update();
        }*/
    }
#endif
}