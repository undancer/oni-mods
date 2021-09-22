using HarmonyLib;

namespace undancer.w2o.Patches
{
    [HarmonyPatch(typeof(PlantableSeed), nameof(PlantableSeed.TestSuitableGround))]
    public class PlantableSeedTestSuitableGroundPatch
    {
        public static bool Prefix(ref bool __result, ref int cell, ref PlantableSeed __instance)
        {

            var flag = false;
            if (Grid.IsValidCell(cell))
            {
                var index = __instance.Direction != SingleEntityReceptacle.ReceptacleDirection.Bottom
                    ? Grid.CellBelow(cell)
                    : Grid.CellAbove(cell);
                if (Grid.IsValidCell(index))
                {
                    var prefab = Assets.GetPrefab(__instance.PlantID);
                    var component = prefab.GetComponent<EntombVulnerable>();
                    if (component == null || component.IsCellSafe(cell))
                    {
                        var component2 = prefab.GetComponent<DrowningMonitor>();
                        if (component2 == null || component2.IsCellSafe(cell))
                        {
                            var component3 = prefab.GetComponent<TemperatureVulnerable>();
                            if (component3 == null || component3.IsCellSafe(cell))
                            {
                                var component4 = prefab.GetComponent<UprootedMonitor>();
                                if (component4 == null || component4.IsSuitableFoundation(cell))
                                {
                                    var component5 = prefab.GetComponent<OccupyArea>();
                                    if (component5 == null || component5.CanOccupyArea(cell, ObjectLayer.Building))
                                    {
                                        flag = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            __result = flag;

            return false;
        }
    }
}