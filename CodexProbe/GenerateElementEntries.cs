using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateElementEntries))]
    public static class GenerateElementEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateElementEntries");
        }
    }
}