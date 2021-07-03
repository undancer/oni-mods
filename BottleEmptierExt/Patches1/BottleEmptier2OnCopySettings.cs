using HarmonyLib;
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
}