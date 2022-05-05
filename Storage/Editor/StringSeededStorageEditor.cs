using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace D_Utilities.Storages
{
#if UNITY_EDITOR
    [CustomEditor(typeof(StringSeededStorage))]
    public class StringSeededStorageEditor : StringStorageEditorBase
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawImportButton();
            
        }
    }
#endif
}
