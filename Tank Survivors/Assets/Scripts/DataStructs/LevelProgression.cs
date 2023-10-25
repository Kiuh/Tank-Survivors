using UnityEngine;

namespace DataStructs
{
    [System.Serializable]
    public struct LevelProgression
    {
        [SerializeField]
        private uint startLevelExperience;
        public uint StartLevelExperience => startLevelExperience;

        [SerializeField]
        private float curveLength;
        public float CurveLength => curveLength;

        [SerializeField]
        private uint limitLevel;
        public uint LimitLevel => limitLevel;

        [SerializeField]
        private uint overlimitExperienceDelta;
        public uint OverlimitExperienceDelta => overlimitExperienceDelta;

        [SerializeField]
        private AnimationCurve curveProgression;
        public AnimationCurve CurveProgression => curveProgression;
    }
}