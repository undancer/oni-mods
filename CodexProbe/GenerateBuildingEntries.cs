using System.Collections.Generic;
using Harmony;
using TUNING;
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
            foreach (var info in BUILDINGS.PLANORDER)
            {
                var category = HashCache.Get().Get(info.category);
                var linkId = CodexCache.FormatLinkID(prefix + category);
                var buildings = (List<string>) info.data;
                foreach (var building in buildings)
                {
                    var buildingDef = Assets.GetBuildingDef(building);
                    var name = buildingDef.PrefabID;
//                    var json = JsonConvert.SerializeObject(buildingDef, Formatting.Indented);

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/BUILDINGS", name.ToUpper()},
                        sprite = buildingDef.GetUISprite(),
                        color = Color.white,
                    });
                }
            }
        }
    }
}