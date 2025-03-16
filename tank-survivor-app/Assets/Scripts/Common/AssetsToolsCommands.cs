#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
#endif

namespace Common
{
    internal static class AssetsToolsCommands
    {
#if UNITY_EDITOR
        [MenuItem("Asset Tools/Force Reserialize ALL Assets")]
        public static void ForceReserializeAssets()
        {
            string[] guids = AssetDatabase.FindAssets("", null);
            Debug.Log("Start Reserialize " + guids.Length + " assets.");

            AssetDatabase.ForceReserializeAssets(
                guids.Select(x => AssetDatabase.GUIDToAssetPath(x)),
                ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata
            );
        }
#endif
    }
}
