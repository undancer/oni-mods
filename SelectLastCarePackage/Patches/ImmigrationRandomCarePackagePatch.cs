using HarmonyLib;

namespace undancer.SelectLastCarePackage.Patches
{
    [HarmonyPatch(typeof(Immigration), "RandomCarePackage")]
    public static class ImmigrationRandomCarePackagePatch // 随机补给包
    {
        public static bool Prefix(ref CarePackageInfo __result)
        {
            var context = SaveGame.Instance.GetComponent<ImmigrantScreenContext>();
            if (context.Skip) return true;
            var lastSelectedCarePackageInfo = context.LastSelectedCarePackageInfo;
            if (lastSelectedCarePackageInfo == null) return true;
            __result = lastSelectedCarePackageInfo;
            context.Skip = true;
            return false;
        }
    }
}