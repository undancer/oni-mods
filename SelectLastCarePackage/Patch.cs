using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace SelectLastCarePackage
{
    public static class ImmigrantScreenContext
    {
        public static CarePackageInfo LastSelectedCarePackageInfo { get; set; }
    }

    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    internal class ImmigrantScreenOnProceed
    {
        public static void Prefix(ImmigrantScreen __instance)
        {
            var selectedDeliverable = Traverse.Create(__instance).Field("selectedDeliverables")
                .GetValue<List<ITelepadDeliverable>>().First();
            if (!(selectedDeliverable is CarePackageInfo carePackageInfo)) return;
            ImmigrantScreenContext.LastSelectedCarePackageInfo = carePackageInfo;
        }
    }

    [HarmonyPatch(typeof(Immigration), "RandomCarePackage")]
    internal class ImmigrationRandomCarePackage
    {
        public static bool Prefix(Immigration __instance, ref CarePackageInfo __result)
        {
            var lastSelectedCarePackageInfo = ImmigrantScreenContext.LastSelectedCarePackageInfo;
            if (lastSelectedCarePackageInfo == null)
            {
                return true;
            }

            __result = lastSelectedCarePackageInfo;
            ImmigrantScreenContext.LastSelectedCarePackageInfo = null;
            return false;
        }
    }
}