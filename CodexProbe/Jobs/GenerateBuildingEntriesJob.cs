using System.Collections.Generic;
using System.Linq;
using CodexProbe.Entity;
using CodexProbe.Patch;
using TUNING;
using UnityEngine;

namespace CodexProbe.Jobs
{
    public class GenerateBuildingEntriesJob : BaseJob
    {
        public static List<Tuple<string, string>> GetBuildings()
        {
            var buildingsMap = new Dictionary<string, string>();

            foreach (var info in BUILDINGS.PLANORDER)
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
            var buildings = new List<BuildingEntity>();

            foreach (var tuple in GetBuildings())
            {
                var building = tuple.first;
                var category = tuple.second;
                var def = Assets.GetBuildingDef(building);
                var name = def.PrefabID;
                var sprite = def.GetUISprite();
                if (sprite == null) continue;
                buildings.Add(new BuildingEntity(def, category));
                SaveImage(
                    new[] {"ASSETS/BUILDINGS", category.ToUpper(), name.ToUpper()},
                    sprite,
                    Color.white
                );
            }

            WriteJson(new[] {"json", "buildings"}, buildings);
        }
    }
}