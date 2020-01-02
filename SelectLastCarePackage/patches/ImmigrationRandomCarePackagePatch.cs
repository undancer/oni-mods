using Harmony;
using undancer.Commons.Configuration;
using undancer.SelectLastCarePackage.config;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(Immigration), "RandomCarePackage")]
    public static class ImmigrationRandomCarePackagePatch
    {
        public static bool Prefix(ref CarePackageInfo __result)
        {
            var skip = Configuration<Settings>.Instance.SkipFlag;
            if (skip) return true;
            var lastSelectedCarePackageInfo = Configuration<Settings>.Instance.GetHistory();
            if (lastSelectedCarePackageInfo == null) return true;
            __result = lastSelectedCarePackageInfo;
            Configuration<Settings>.Instance.SkipFlag = true;
            return false;
        }
    }
}