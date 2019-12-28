using System.Collections.Generic;
using System.Linq;
using CodexProbe.Patch;
using UnityEngine;

namespace CodexProbe.Jobs
{
    public class GenerateBuildingEntriesJob : BaseJob
    {
        public static List<Tuple<string, string>> GetBuildings()
        {
            var buildingsMap = new Dictionary<string, string>();

            foreach (var info in TUNING.BUILDINGS.PLANORDER)
            {
                var category = HashCache.Get().Get(info.category);
                var buildings = (List<string>) info.data;
                foreach (var building in buildings)
                {
                    buildingsMap.Add(building, category);
                }
            }

            foreach (var building in BuildingContext.buildings.Where(building => !buildingsMap.ContainsKey(building)))
            {
                buildingsMap.Add(building, "Others");
            }

            return buildingsMap
                .Select(keyValuePair => 
                    new Tuple<string, string>(keyValuePair.Key, keyValuePair.Value)
                )
                .ToList();
        }

        public static void GenerateBuildingEntries()
        {
            foreach (var tuple in GetBuildings())
            {
                var building = tuple.first;
                var category = tuple.second;
                var def = Assets.GetBuildingDef(building);
                var name = def.PrefabID;
                var sprite = def.GetUISprite();
                if (sprite == null) continue;
                SaveImage(
                    new[] {"ASSETS/BUILDINGS", category.ToUpper(), name.ToUpper()},
                    sprite,
                    Color.white
                );
            }
        }
    }
}