using UnityEngine;

namespace PlatformSpecific
{
    [AddComponentMenu("Scripts/PlatformSpecific/PlatformSpecific.YGInstaller")]
    internal class YGInstaller : MonoBehaviour
    {
        [SerializeField]
        private GameObject yandexGamePrefab;

        private void Awake()
        {
            if (BuildSettings.Data.IsYandexGameBuild)
            {
#if YG_PLUGIN_YANDEX_GAME
                _ = Instantiate(yandexGamePrefab);
#endif
            }
        }
    }
}
