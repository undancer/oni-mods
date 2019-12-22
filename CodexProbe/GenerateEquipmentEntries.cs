using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateEquipmentEntries))]
    public static class GenerateEquipmentEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateEquipmentEntries");
        }
    }
}