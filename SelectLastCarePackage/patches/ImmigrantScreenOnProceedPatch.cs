using System.Collections.Generic;
using System.Linq;
using Harmony;
using undancer.Commons.Configuration;
using undancer.SelectLastCarePackage.config;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    public static class ImmigrantScreenOnProceedPatch // 按下了按钮
    {
        public static void Prefix(List<ITelepadDeliverable> ___selectedDeliverables)
        {
            var selectedDeliverable = ___selectedDeliverables.First();
            CarePackageInfo selectedCarePackage = null;
            if (selectedDeliverable is CarePackageInfo carePackageInfo) selectedCarePackage = carePackageInfo;
            Configuration<Settings>.Instance.AddHistory(selectedCarePackage);
            Configuration<Settings>.Instance.Clean();
            Configuration<Settings>.Save();
        }
    }
}