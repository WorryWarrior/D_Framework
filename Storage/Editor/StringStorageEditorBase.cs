using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace D_Framework.Storages
{
#if UNITY_EDITOR
    public class StringStorageEditorBase : Editor
    {
        private const string EMPTY_PATH = "";
        private const string STORAGE_COLLECTION_NAME = "elements";
        private const string TXT_EXTENSION = ".txt";

        protected void DrawImportButton()
        {
            if (GUILayout.Button("Import File Contents"))
            {
                string path = EditorUtility.OpenFilePanel("", Application.dataPath, "");

                if (TryProcessFileAtPath(path, out List<string> fileContents))
                {
                    System.Type targetType = target.GetType().BaseType.BaseType; // Get to StorageBase type

                    FieldInfo elementsProperty = targetType.GetField(STORAGE_COLLECTION_NAME, BindingFlags.NonPublic | BindingFlags.Instance);
                    elementsProperty.SetValue(target, fileContents);
                    //AssetDatabase.SaveAssets();
                }
            }
        }

        private bool TryProcessFileAtPath(string path, out List<string> fileContents)
        {
            fileContents = new List<string>();

            if (path == EMPTY_PATH)
                return false;

            fileContents = GetParser(Path.GetExtension(path)).Parse(path);

            return true;
        }

        private IParser GetParser(string extension)
        {
            switch (extension)
            {
                case TXT_EXTENSION:
                    return new TxtParser();
                default:
                    Debug.LogError($"{extension} files are not supported");
                    return null;
            }
        }
    }
#endif
}