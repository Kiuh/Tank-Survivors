using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Panels.Pause
{
    [Serializable]
    public struct StatData
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public struct StatBlockData
    {
        public string StatName;
        public List<StatData> StatsData;
    }

    [AddComponentMenu("Scripts/Panels/Pause/Panels.Pause.StatGrid")]
    internal class StatGrid : MonoBehaviour
    {
        [Required]
        [AssetSelector]
        [SerializeField]
        private StatBlock statBlockPrefab;

        [Required]
        [SerializeField]
        private Transform gridRoot;

        [Required]
        [SerializeField]
        private TMP_Text titleLabel;

        public void FillWithData(StatBlockData data)
        {
            ClearGrid();
            titleLabel.text = data.StatName;
            foreach (StatData stat in data.StatsData)
            {
                CreateBlock(stat.Name, stat.Value);
            }
        }

        private void CreateBlock(string name, string value)
        {
            StatBlock statBlockInstance = Instantiate(statBlockPrefab, gridRoot);
            statBlockInstance.SetContent(name, value);
        }

        private void ClearGrid()
        {
            for (int i = gridRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(gridRoot.GetChild(i).gameObject);
            }
        }
    }
}
