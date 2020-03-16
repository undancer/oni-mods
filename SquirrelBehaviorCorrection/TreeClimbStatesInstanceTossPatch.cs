using Harmony;

namespace undancer.SquirrelBehaviorCorrection
{
    [HarmonyPatch(typeof(TreeClimbStates.Instance), nameof(TreeClimbStates.Instance.Toss))]
    public static class TreeClimbStatesInstanceTossPatch
    {
        public static bool Prefix(Pickupable pu)
        {
            return pu.KPrefabID.HasTag(TagManager.Create("Seed"));
        }
    }
}