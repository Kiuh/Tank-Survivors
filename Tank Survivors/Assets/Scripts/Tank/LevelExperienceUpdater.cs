using DataStructs;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.LevelExperienceUpdater")]
    public class LevelExperienceUpdater : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        private PlayerLevel playerLevel;
        private LevelProgression progression;

        private float minLevelValue;
        private float maxLevelValue;

        private void Awake()
        {
            playerLevel = tank.PlayerLevel;
            progression = tank.LevelProgression;

            Keyframe[] curveFrames = progression.CurveProgression.keys;
            if (curveFrames != null)
            {
                minLevelValue = curveFrames[0].time;
                maxLevelValue = curveFrames[^1].time;
            }

            playerLevel.MaxExperienceCount = GetLevelExperience(playerLevel.CurrentLevel);

            playerLevel.OnLevelUp += UpdateMaxExpirienceCount;
        }

        private void UpdateMaxExpirienceCount()
        {
            uint currentLevel = playerLevel.CurrentLevel;

            playerLevel.MaxExperienceCount = GetLevelExperience(currentLevel);
        }

        private uint GetLevelExperience(uint currentLevel)
        {
            uint levelExperience = progression.StartLevelExperience
                + (uint)progression.CurveProgression.Evaluate(
                    Mathf.Lerp(minLevelValue, maxLevelValue, (float)currentLevel / progression.LimitLevel));

            if (currentLevel > progression.LimitLevel)
            {
                levelExperience += (uint)((currentLevel - progression.LimitLevel) * progression.CurveLength);
            }

            return levelExperience;
        }
    }
}
