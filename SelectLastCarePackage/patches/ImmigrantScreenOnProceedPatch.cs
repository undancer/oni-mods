using System.Collections.Generic;
using System.Linq;
using Harmony;
using undancer.Commons;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    public static class ImmigrantScreenOnProceedPatch
    {
        public static void Prefix(ImmigrantScreen __instance)
        {
            var selectedDeliverable = __instance.GetField<List<ITelepadDeliverable>>("selectedDeliverables").First();
            CarePackageInfo selectedCarePackage = null;
            if (selectedDeliverable is CarePackageInfo carePackageInfo) selectedCarePackage = carePackageInfo;
            ImmigrantScreenContext.LastSelectedCarePackageInfo = selectedCarePackage;
        }
    }
}