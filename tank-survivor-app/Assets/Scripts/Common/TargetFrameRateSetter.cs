using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.TargetFrameRateSetter")]
    internal class TargetFrameRateSetter : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
