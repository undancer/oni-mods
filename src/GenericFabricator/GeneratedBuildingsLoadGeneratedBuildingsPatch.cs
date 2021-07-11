using HarmonyLib;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.LoadGeneratedBuildings))]
    public static class GeneratedBuildingsLoadGeneratedBuildingsPatch
    {
        public static void Prefix()
        {
            ModUtil.AddBuildingToPlanScreen(new HashedString("Refining"), GenericFabricatorConfig.ID);
        }
    }
}