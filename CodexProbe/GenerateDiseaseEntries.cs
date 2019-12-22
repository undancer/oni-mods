using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateDiseaseEntries))]
    public static class GenerateDiseaseEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateDiseaseEntries");
        }
    }
}