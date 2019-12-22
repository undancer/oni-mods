using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateGeyserEntries))]
    public static class GenerateGeyserEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateGeyserEntries");
        }
    }
}