using HarmonyLib;
using TUNING;
using UnityEngine;

namespace undancer.BottleEmptierExt
{
    [HarmonyPatch(typeof(BottleEmptierConfig), "ConfigureBuildingTemplate")]
    public static class BottleEmptierConfigConfigureBuildingTemplate
    {
        public static bool Prefix(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            var storage = go.AddOrGet<Storage>();
            storage.storageFilters = STORAGEFILTERS.LIQUIDS;
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.capacityKg = 200f;
            go.AddOrGet<TreeFilterable>();
            go.AddOrGet<BottleEmptier2>();

            return false;
        }
    }
}