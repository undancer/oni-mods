using Harmony;

namespace ClassLibrary2
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