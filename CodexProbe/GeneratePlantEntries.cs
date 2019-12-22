using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GeneratePlantEntries))]
    public static class GeneratePlantEntries
    {
        public static void Postfix()
        {
            Debug.Log("GeneratePlantEntries");
        }
    }
}