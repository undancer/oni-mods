using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateBuildingEntries))]
    public static class GenerateBuildingEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateBuildingEntries");
        }
    }
}