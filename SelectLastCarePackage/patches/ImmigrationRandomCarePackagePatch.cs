using Harmony;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(Immigration), "RandomCarePackage")]
    public static class ImmigrationRandomCarePackagePatch
    {
        public static bool Prefix(Immigration __instance, ref CarePackageInfo __result)
        {
            var lastSelectedCarePackageInfo = ImmigrantScreenContext.LastSelectedCarePackageInfo;
            if (lastSelectedCarePackageInfo == null) return true;
            __result = lastSelectedCarePackageInfo;
            ImmigrantScreenContext.LastSelectedCarePackageInfo = null;
            return false;
        }
    }
}