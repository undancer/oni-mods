using HarmonyLib;

namespace undancer.w2o.Patches
{
    [HarmonyPatch(typeof(PlantableCellQuery),"CheckValidPlotCell")]
    public class PlantableCellQueryCheckValidPlotCellPatch
    {
        public static bool Prefix(ref bool __result, ref PlantableSeed seed, ref int plant_cell)
        {
            if (Grid.IsValidCell(plant_cell))
            {
                var cell = seed.Direction != SingleEntityReceptacle.ReceptacleDirection.Bottom
                    ? Grid.CellBelow(plant_cell)
                    : Grid.CellAbove(plant_cell);
                __result = Grid.IsValidCell(cell) && seed.TestSuitableGround(plant_cell);
            }
            else
            {
                __result = false;
            }

            return false;
        }
    }
}