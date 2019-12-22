using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    public static class GenerateFoodEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateFoodEntries");
        }
    }
}