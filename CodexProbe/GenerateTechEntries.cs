using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateTechEntries))]
    public static class GenerateTechEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateTechEntries");
        }
    }
}