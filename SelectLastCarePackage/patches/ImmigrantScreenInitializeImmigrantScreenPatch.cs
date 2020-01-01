using Harmony;
using undancer.Commons.Configuration;
using undancer.SelectLastCarePackage.config;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), nameof(ImmigrantScreen.InitializeImmigrantScreen))]
    public static class ImmigrantScreenInitializeImmigrantScreenPatch
    {
        public static void Postfix()
        {
            Configuration<Settings>.Instance.SkipFlag = false;
            Debug.Log("新的开始");
        }
    }
}