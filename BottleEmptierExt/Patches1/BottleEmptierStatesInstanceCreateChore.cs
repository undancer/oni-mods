using HarmonyLib;

namespace undancer.BottleEmptierExt
{
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