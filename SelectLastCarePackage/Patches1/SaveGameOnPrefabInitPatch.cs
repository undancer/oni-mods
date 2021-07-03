using HarmonyLib;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(SaveGame), "OnPrefabInit")]
    public static class SaveGameOnPrefabInitPatch //初始化
    {
        internal static void Postfix(SaveGame __instance)
        {
            var gameObject = __instance.gameObject;
            if (gameObject == null) return;
            gameObject.FindOrAddComponent<ImmigrantScreenContext>();
        }
    }
}