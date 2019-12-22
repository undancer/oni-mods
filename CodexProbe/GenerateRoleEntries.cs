using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateRoleEntries))]
    public static class GenerateRoleEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateRoleEntries");
        }
    }
}