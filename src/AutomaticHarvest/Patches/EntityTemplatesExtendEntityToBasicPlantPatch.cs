using HarmonyLib;
using UnityEngine;

namespace undancer.AutomaticHarvest.Patches
{
    [HarmonyPatch(typeof(EntityTemplates), nameof(EntityTemplates.ExtendEntityToBasicPlant))]
    public static class EntityTemplatesExtendEntityToBasicPlantPatch
    {
        public static void Prefix(
            GameObject template,
            ref float max_age)
        {
            var name = template.PrefabID().Name;
            var times = Context.Config.GetSettings(name);
            max_age *= times; // 自动收获的时间
            while (max_age <= 0) // 确保成熟期大于0，否则会不产生作物
                max_age += 0.5f;
        }
    }
}