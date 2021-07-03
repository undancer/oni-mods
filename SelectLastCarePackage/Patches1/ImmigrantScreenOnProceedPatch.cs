using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    public static class ImmigrantScreenOnProceedPatch //按下打印按钮
    {
        public static void Prefix(List<ITelepadDeliverable> ___selectedDeliverables)
        {
            var context = SaveGame.Instance.GetComponent<ImmigrantScreenContext>();
            var selectedDeliverable = ___selectedDeliverables.First();
            if (selectedDeliverable is CarePackageInfo selectedCarePackage)
                context.LastSelectedCarePackageInfo = selectedCarePackage;
            context.Skip = false;
        }
    }
}