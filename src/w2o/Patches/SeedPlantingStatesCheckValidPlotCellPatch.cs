using HarmonyLib;

namespace undancer.w2o.Patches
{
    [HarmonyPatch(typeof(SeedPlantingStates),"CheckValidPlotCell")]
    public class SeedPlantingStatesCheckValidPlotCellPatch
    {
        public static bool Prefix(ref bool __result, ref PlantableSeed seed, ref int cell)
        {
            __result = seed.TestSuitableGround(cell);
            return false;
        }
    }
}