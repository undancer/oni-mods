using Harmony;
using TUNING;
using UnityEngine;

namespace undancer.BottleEmptierExt
{
    [HarmonyPatch(typeof(BottleEmptier), "OnCopySettings")]
    public static class BottleEmptier2OnCopySettings
    {
        public static bool Prefix(BottleEmptier __instance, object data)
        {
            if (!(__instance is BottleEmptier2 instance)) return true;
            var other = ((GameObject) data).GetComponent<BottleEmptier2>();
            instance.UserMaxCapacity = other.UserMaxCapacity;
            instance.allowManualPumpingStationFetching = other.allowManualPumpingStationFetching;
            return false;
        }
    }

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

    [HarmonyPatch(typeof(BottleEmptier.StatesInstance), "CreateChore")]
    public static class BottleEmptierStatesInstanceCreateChore
    {
        public static bool Prefix(BottleEmptier.StatesInstance __instance)
        {
            if (!(__instance.master is IUserControlledCapacity)) return true;
            __instance.GetComponent<KBatchedAnimController>();
            var tags = __instance.GetComponent<TreeFilterable>().GetTags();
            var forbidden_tags = !__instance.master.allowManualPumpingStationFetching
                ? new Tag[1] {GameTags.LiquidSource}
                : new Tag[0];
            var amount = ((BottleEmptier2) __instance.master).UserMaxCapacity / 1000f;
            var component = __instance.GetComponent<Storage>();
            var chore = new FetchChore(Db.Get().ChoreTypes.StorageFetch, component, amount, tags,
                forbidden_tags: forbidden_tags);
            Traverse.Create(__instance).Field("chore").SetValue(chore);
            return false;
        }
    }
}