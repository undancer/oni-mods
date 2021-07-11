using HarmonyLib;
using STRINGS;
using undancer.Commons;

namespace undancer.SelectLastCarePackage.Patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnSpawn")]
    public static class ImmigrantScreenOnSpawnPatch // 显示选人界面
    {
        public static void Postfix(KButton ___rejectButton)
        {
            if (ModUtils.HasRefreshMod()) return;
            ___rejectButton
                .SetText(
                    Localization.GetLocale() != null &&
                    Localization.GetLocale().Lang == Localization.Language.Chinese
                        ? Languages.REROLL
                        : UI.IMMIGRANTSCREEN.SHUFFLE
                );
        }
    }
}