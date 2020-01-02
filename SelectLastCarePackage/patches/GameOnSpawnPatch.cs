using Harmony;
using undancer.Commons.Configuration;
using undancer.SelectLastCarePackage.config;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(Game), "OnSpawn")]
    public static class GameOnSpawnPatch
    {
        public static void Postfix()
        {
            Configuration<Settings>.Instance.CleanIndex();
        }
    }
}