using System.Collections.Generic;
using CodexProbe.Jobs;
using Harmony;
using UnityEngine;

namespace CodexProbe.Patch
{
    public static class BuildingContext
    {
        public static List<string> buildings = new List<string>();
    }

    [HarmonyPatch(typeof(BuildingTemplates), nameof(BuildingTemplates.CreateBuildingDef))]
    public static class BuildingTemplatesCreateBuildingDefPatch
    {
        public static void Postfix(
            string id,
            BuildingDef __result
        )
        {
            BuildingContext.buildings.Add(id);
            return;
            if (__result == null)
            {
                Debug.LogError(id);
                return;
            }

            BuildingDef def = __result;
//            string id = __result.PrefabID;
            int width = __result.WidthInCells;
            int height = __result.HeightInCells;
//            string anim = __result.ani
            int hitpoints = __result.HitPoints;
            float construction_time = __result.ConstructionTime;
            float[] construction_mass = __result.Mass;
            string[] construction_materials = __result.MaterialCategory;
            float melting_point = __result.BaseMeltingPoint;
            BuildLocationRule build_location_rule = __result.BuildLocationRule;
            EffectorValues decor = new EffectorValues((int) __result.BaseDecor, (int) __result.BaseDecorRadius);
            EffectorValues noise = new EffectorValues(__result.BaseNoisePollution, __result.BaseNoisePollutionRadius);
            //   float temperature_modification_mass_scale =
            Debug.Log(__result.MassForTemperatureModification);


//            var def = Assets.GetBuildingDef(id);
            var name = def.PrefabID;
            var sprite = def.GetUISprite();
            if (sprite != null)
            {
                BaseJob.SaveImage(
                    new[] {"ASSETS/BUILDINGS_", "ALL", name.ToUpper()},
                    sprite,
                    Color.white
                );
            }
            else
            {
                Debug.Log("MISS: " + name.ToUpper());
            }
        }
    }
}