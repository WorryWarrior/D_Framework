using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace D_Utilities
{
#if UNITY_EDITOR

    [CustomEditor(typeof(LevelManagerCompanion))]
    public class LevelManagerCompanionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelManagerCompanion lmc = (LevelManagerCompanion)target;

            GUILayout.Space(20f);
            GUILayout.Label(lmc.GetLevelList());

            GUILayout.Space(20f);
            GUILayout.Label(lmc.GetCurrentlySelectedLevel());

            if (GUILayout.Button("Set Sequential Level Mode"))
            {
                lmc.SetSequentialLevelMode();
            }
        }

    }

#endif
}