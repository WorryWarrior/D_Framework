using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace D_Utilities.Storages
{
#if UNITY_EDITOR
    public class StringStorageEditorBase : Editor
    {
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

            if (path == "")
                return false;

            string fileExtension = Path.GetExtension(path);
            switch (fileExtension)
            {
                case TXT_EXTENSION:
                    fileContents = ParseTxt(path);
                    break;
                default:
                    Debug.LogError($"{fileExtension} files are not supported");
                    break;
            }

            return true;
        }

        private List<string> ParseTxt(string txtPath)
        {
            string line;
            List<string> lines = new List<string>();

            using (StreamReader fileSR = new StreamReader(txtPath))
            {
                while ((line = fileSR.ReadLine()) != null)
                {
                    lines.Add(line);
                }

            }

            return lines;
        }
    }
#endif
}