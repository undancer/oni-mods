using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateCreatureEntries))]
    public static class GenerateCreatureEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateCreatureEntries");
        }
    }
}