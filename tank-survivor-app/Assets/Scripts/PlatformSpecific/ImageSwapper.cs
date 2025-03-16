using UnityEngine;
using UnityEngine.UI;

namespace PlatformSpecific
{
    [AddComponentMenu("Scripts/PlatformSpecific/PlatformSpecific.ImageSwapper")]
    internal class ImageSwapper : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private Sprite newSprite;

        private void Awake()
        {
            if (BuildSettings.Data.IsYandexGameBuild)
            {
                image.sprite = newSprite;
            }
        }
    }
}
