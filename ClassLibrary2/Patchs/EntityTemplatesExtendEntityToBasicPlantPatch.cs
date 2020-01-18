using Harmony;
using UnityEngine;

namespace ClassLibrary2
{
    [HarmonyPatch(typeof(EntityTemplates), nameof(EntityTemplates.ExtendEntityToBasicPlant))]
    public static class EntityTemplatesExtendEntityToBasicPlantPatch
    {
        public static void Prefix(
            GameObject template,
            ref float max_age)
        {
            var name = template.PrefabID().Name;
            var times = Mod.Config.GetSettings(name);
            max_age *= times;// 自动收获的时间
        }
    }
}