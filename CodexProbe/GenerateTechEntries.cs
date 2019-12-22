using Harmony;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateTechEntries))]
    public static class GenerateTechEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateTechEntries");
            foreach (var resource in Db.Get().Techs.resources)
            {
                //科技研究没有图标
            }
        }
    }
}