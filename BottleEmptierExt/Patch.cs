using BottleEmptierExt;
using Harmony;
using TUNING;
using UnityEngine;

namespace BottleEmptierExt
{
    [HarmonyPatch(typeof(BottleEmptier), "OnCopySettings")]
    public static class BottleEmptier2OnCopySettings
    {
        public static bool Prefix(BottleEmptier __instance, object data)
        {
            if (!(__instance is BottleEmptier2 instance)) return true;
            var other = ((GameObject) data).GetComponent<BottleEmptier2>();
            instance.allowManualPumpingStationFetching = other.allowManualPumpingStationFetching;
            instance.UserMaxCapacity = other.UserMaxCapacity;
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
            var controller = __instance.GetComponent<KBatchedAnimController>();
            var tags = __instance.GetComponent<TreeFilterable>().GetTags();
            if (tags == null || tags.Length == 0)
            {
                controller.TintColour = __instance.master.noFilterTint;
            }
            else
            {
                controller.TintColour = __instance.master.filterTint;
                var forbiddenTags = !__instance.master.allowManualPumpingStationFetching
                    ? new[] {GameTags.LiquidSource}
                    : new Tag[0];

                var amount = ((BottleEmptier2) __instance.master).UserMaxCapacity / 1000f;

                var chore = new FetchChore(Db.Get().ChoreTypes.StorageFetch, __instance.GetComponent<Storage>(), amount,
                    __instance.GetComponent<TreeFilterable>().GetTags(), (Tag[]) null, forbiddenTags);
                Traverse.Create(__instance).Field("chore").SetValue(chore);
            }

            return false;
        }
    }
}