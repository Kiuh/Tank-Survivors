using System.IO;
using System.IO.Compression;
using Configs;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace PlatformSpecific
{
    public class BuildCommands
    {
        [MenuItem("Build Tools/Build All OpenGL")]
        public static void BuildOpenGL()
        {
            BuildGameYGCommand();
            BuildGameItchCommand();
        }

        [MenuItem("Build Tools/Build for YandexGame")]
        public static void BuildGameYGCommand()
        {
            ClearEditorData();
            BuildGameYG(BuildSettings.Data.BuildFolderPath, BuildSettings.Data.BuildScenes);
        }

        [MenuItem("Build Tools/Build for Itch.io")]
        public static void BuildGameItchCommand()
        {
            ClearEditorData();
            BuildGameItch(BuildSettings.Data.BuildFolderPath, BuildSettings.Data.BuildScenes);
        }

        [MenuItem("Build Tools/Build for Google Play")]
        public static void BuildGameGooglePlayCommand()
        {
            ClearEditorData();
            BuildGameGooglePlay(BuildSettings.Data.BuildFolderPath, BuildSettings.Data.BuildScenes);
        }

        private static void ClearEditorData()
        {
            if (BuildSettings.Data.ClearEditorDataInBuild)
            {
                Levels.Data.ResetConfig();
            }
        }

        private static void BuildGameYG(string path, string[] scenes)
        {
            BuildSettings.Data.IsYandexGameBuild = true;
            PlayerSettings.WebGL.template = BuildSettings.Data.YandexGameWebGlTemplate;

            string folderPath = path + "/YandexGame";

            if (BuildSettings.Data.ClearBuildFolder)
            {
                Directory.Delete(folderPath, true);
                ClearYandexGameZips();
            }

            if (!Directory.Exists(folderPath))
            {
                _ = Directory.CreateDirectory(folderPath);
            }

            BuildPlayerOptions options =
                new()
                {
                    scenes = scenes,
                    options = BuildOptions.None,
                    locationPathName = folderPath,
                    target = BuildTarget.WebGL
                };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            Archiving(folderPath);
            LogReport(report, "YandexGame");
        }

        private static void BuildGameItch(string path, string[] scenes)
        {
            BuildSettings.Data.IsYandexGameBuild = false;
            PlayerSettings.WebGL.template = BuildSettings.Data.ItchWebGlTemplate;

            string folderPath = path + "/Itch";

            if (BuildSettings.Data.ClearBuildFolder)
            {
                Directory.Delete(folderPath, true);
                ClearItchZips();
            }

            if (!Directory.Exists(folderPath))
            {
                _ = Directory.CreateDirectory(folderPath);
            }

            BuildPlayerOptions options =
                new()
                {
                    scenes = scenes,
                    options = BuildOptions.None,
                    locationPathName = folderPath,
                    target = BuildTarget.WebGL
                };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            Archiving(folderPath);
            LogReport(report, "Itch");
        }

        private static void BuildGameGooglePlay(string path, string[] scenes)
        {
            BuildSettings.Data.IsYandexGameBuild = false;

            PlayerSettings.Android.keystorePass = "kxYVmgR0xUaN3xp1RJ92";
            PlayerSettings.Android.keyaliasPass = "kxYVmgR0xUaN3xp1RJ92";
            PlayerSettings.Android.bundleVersionCode += 1;

            string folderPath = path + "/GooglePlay";

            if (BuildSettings.Data.ClearBuildFolder)
            {
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }

            if (!Directory.Exists(folderPath))
            {
                _ = Directory.CreateDirectory(folderPath);
            }

            BuildPlayerOptions options =
                new()
                {
                    scenes = scenes,
                    options = BuildOptions.None,
                    locationPathName = folderPath + "/TankSurvivors.aab",
                    target = BuildTarget.Android
                };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            LogReport(report, "GooglePlay");
        }

        private static void LogReport(BuildReport report, string platform)
        {
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                UnityEngine.Debug.Log(
                    $"Build {platform} succeeded: {(double)(summary.totalSize / 1024f / 1024f):0.##} MB"
                );
                UnityEngine.Debug.Log(
                    $"Build {platform} Total time: {summary.totalTime.TotalMinutes} min {summary.totalTime.Seconds} sec"
                );
                UnityEngine.Debug.Log(
                    $"Build {platform} Errors:{summary.totalErrors} Warnings: {summary.totalWarnings}"
                );
            }

            if (summary.result == BuildResult.Failed)
            {
                UnityEngine.Debug.Log("Build failed");
            }
        }

        private static void Archiving(string pathToBuiltProject)
        {
            string number = "";

            if (!File.Exists(pathToBuiltProject + ".zip"))
            {
                Do();
            }
            else
            {
                for (int i = 1; i < 100; i++)
                {
                    if (!File.Exists(pathToBuiltProject + "_" + i + ".zip"))
                    {
                        number = "_" + i;
                        Do();
                        break;
                    }
                }
            }

            void Do()
            {
                ZipFile.CreateFromDirectory(
                    pathToBuiltProject,
                    pathToBuiltProject + number + ".zip"
                );
            }
        }

        public static void ClearItchZips()
        {
            foreach (string file in Directory.GetFiles(BuildSettings.Data.BuildFolderPath))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.StartsWith("Itch") && fileName.EndsWith(".zip"))
                {
                    File.Delete(file);
                }
            }
        }

        public static void ClearYandexGameZips()
        {
            foreach (string file in Directory.GetFiles(BuildSettings.Data.BuildFolderPath))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.StartsWith("YandexGame") && fileName.EndsWith(".zip"))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
