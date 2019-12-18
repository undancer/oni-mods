using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    public static class ImmigrantScreenOnProceedPatch
    {
        public static void Prefix(ImmigrantScreen __instance)
        {
            var selectedDeliverable = Traverse.Create(__instance).Field("selectedDeliverables")
                .GetValue<List<ITelepadDeliverable>>().First();
            CarePackageInfo selectedCarePackage = null;
            if (selectedDeliverable is CarePackageInfo carePackageInfo) selectedCarePackage = carePackageInfo;
            ImmigrantScreenContext.LastSelectedCarePackageInfo = selectedCarePackage;
        }
    }

}