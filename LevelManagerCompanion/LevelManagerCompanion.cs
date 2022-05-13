using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace D_Framework
{
    public class LevelManagerCompanion : MonoBehaviour
    {
        private const string CURRENT_LEVEL_ERROR_MESSAGE = "Incorrect Level Input";
        private const string SEQUENTIAL_LEVEL_MODE_MESSAGE = "Sequential Level Mode";

        [SerializeField] private LevelManager levelManager = null;
        [SerializeField] private EditorDatabase editorDatabase = null;

        private StringBuilder sb = new StringBuilder();

        public string GetLevelList()
        {
            sb.Clear();

            for (int i = 0; i < editorDatabase.levelDatas.Count; i++)
            {
                sb.Append($"{i} - {editorDatabase.levelDatas[i].name}");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string GetCurrentlySelectedLevel()
        {
            if (editorDatabase == null || levelManager == null ||
                levelManager.alwaysLoadLevelId < -1 || levelManager.alwaysLoadLevelId >= editorDatabase.levelDatas.Count)
                return CURRENT_LEVEL_ERROR_MESSAGE;

            if (levelManager.alwaysLoadLevelId == -1)
                return SEQUENTIAL_LEVEL_MODE_MESSAGE;

            GameObject currentLevel = editorDatabase.levelDatas[levelManager.alwaysLoadLevelId];

            if (currentLevel != null)
            {
                return $"Current level: {currentLevel.name}";
            }

            return CURRENT_LEVEL_ERROR_MESSAGE;
        }

        public void SetSequentialLevelMode()
        {
            levelManager.alwaysLoadLevelId = -1;
            MainData.ClearGamePP();
        }
    }
}