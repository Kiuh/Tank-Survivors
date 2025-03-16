using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Panels.Pause;
using Sirenix.OdinInspector;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(
        fileName = "StatsModuleSerializer",
        menuName = "ScriptableObjects/StatsModuleSerializer"
    )]
    internal class StatsModuleSerializer : ScriptableObject
    {
        [Serializable]
        private class ModuleView
        {
            public IEnumerable<string> AllWeaponsModules =>
                Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(type =>
                        typeof(IWeaponModule).IsAssignableFrom(type) && !type.IsInterface
                    )
                    .Select(x => x.FullName);

            [ValueDropdown("@AllWeaponsModules")]
            public string ModuleName;
            public string ModuleViewName;
            public string ModuleViewValueFormat;
        }

        private static StatsModuleSerializer CachedInstance { get; set; } = null;
        public static StatsModuleSerializer Instance
        {
            get
            {
                CachedInstance ??= Resources.Load<StatsModuleSerializer>("StatsModuleSerializer");
                return CachedInstance;
            }
        }

        [SerializeField]
        private List<ModuleView> moduleViews = new();

        public StatData GetStatData(IWeaponModule weaponModule)
        {
            StatData data = new() { Name = "Unknown name", Value = "No Value" };
            ModuleView moduleView = moduleViews.FirstOrDefault(x =>
                x.ModuleName == weaponModule.GetType().FullName
            );
            if (moduleView != null)
            {
                data.Name = moduleView.ModuleViewName;
                data.Value = string.Format(moduleView.ModuleViewValueFormat, weaponModule.Values);
            }
            return data;
        }
    }
}
