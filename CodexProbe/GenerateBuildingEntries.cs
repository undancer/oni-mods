using System.Collections.Generic;
using Harmony;
using Newtonsoft.Json;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateBuildingEntries))]
    public static class GenerateBuildingEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateBuildingEntries");
            const string prefix = "BUILD_CATEGORY_";
            foreach (var planInfo in TUNING.BUILDINGS.PLANORDER)
            {
                var category = HashCache.Get().Get(planInfo.category);
                var linkId = CodexCache.FormatLinkID(prefix + category);
                var buildings = (List<string>) planInfo.data;
                foreach (var building in buildings)
                {
                    var buildingDef = Assets.GetBuildingDef(building);
//                    var json = JsonConvert.SerializeObject(buildingDef, Formatting.Indented);

                    Debug.Log("DEF " + buildingDef.PrefabID);
                    Debug.Log("DEF " + buildingDef.PrefabID.ToUpper());
                    Debug.Log("DEF " + buildingDef.name);
                    
                    buildingDef.SaveImage();
                }
            }
        }
    }
}