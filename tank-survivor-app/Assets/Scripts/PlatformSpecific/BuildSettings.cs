using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlatformSpecific
{
    [Serializable]
    public struct SceneReference
    {
        [Scene]
        public string SceneName;
    }

    [CreateAssetMenu(fileName = "BuildSettings", menuName = "Configs/BuildSettings")]
    public class BuildSettings : ScriptableObject
    {
        private static BuildSettings cashedInstance = null;
        public static BuildSettings Data
        {
            get
            {
                if (cashedInstance == null)
                {
                    cashedInstance = Resources.Load<BuildSettings>("BuildSettings");
                }
                return cashedInstance;
            }
        }

        public bool IsYandexGameBuild;
        public bool ClearEditorDataInBuild;
        public bool ClearBuildFolder;

        [SerializeField]
        private List<SceneReference> scenesToBuild;
        public string[] BuildScenes => scenesToBuild.Select(x => x.SceneName).ToArray();

        [SerializeField]
        [FolderPath(AbsolutePath = false, ParentFolder = "Assets")]
        private string buildFolderPath;
        public string BuildFolderPath
        {
            get
            {
                string absolutePath = Path.GetFullPath(
                    Application.dataPath + "/" + buildFolderPath
                );
                if (!Directory.Exists(absolutePath))
                {
                    Debug.LogError("Build Folder does not exist - (" + absolutePath + ")");
                    throw new DirectoryNotFoundException();
                }
                return absolutePath;
            }
        }

        private static string template_prefix = "PROJECT:";

        [SerializeField]
        [FolderPath(AbsolutePath = false)]
        private string yandexGameWebGlTemplateFolder;
        public string YandexGameWebGlTemplate =>
            template_prefix + Path.GetFileName(yandexGameWebGlTemplateFolder);

        [SerializeField]
        [FolderPath(AbsolutePath = false)]
        private string itchWebGlTemplateFolder;
        public string ItchWebGlTemplate =>
            template_prefix + Path.GetFileName(itchWebGlTemplateFolder);
    }
}
