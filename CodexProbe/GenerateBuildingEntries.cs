using System.Collections.Generic;
using System.Linq;
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
            var fabricatorCache = new List<string>();
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

                    var buildingGo = buildingDef.BuildingComplete;
                    // Fabricator

                    var fabricator = buildingGo.GetComponent<ComplexFabricator>();
                    if (fabricator != null)
                    {
                        foreach (var recipe in fabricator.GetRecipes())
                        {
                            Debug.Log("RECIPE : " + recipe.id);
                            
                            var elements = new TagSet();
                            //原料
                            foreach (var element in recipe.ingredients)
                            {
                                elements.Add(element.material);
                            }
                            //制品
                            foreach (var element in recipe.results)
                            {
                                elements.Add(element.material);
                            }
                            
                            foreach (var element in elements)
                            {
                                var elementGo = Assets.GetPrefab(element);
                                if (elementGo == null) continue;
                                var elementName = elementGo.PrefabID().ToString();
                                var elementTuple = Def.GetUISprite(element);
                                var elementSprite = elementTuple.first;
                                var elementColor = elementTuple.second;

                                ImageUtils.SaveImage(new Image
                                {
                                    prefixes = new[] {"ASSETS/EL", name.ToUpper(), elementName.ToUpper()},
                                    sprite = elementSprite,
                                    color = elementColor,
                                });
                            }
                        }
                    }


                    // Receptacle
                    var plot = buildingGo.GetComponent<SingleEntityReceptacle>();
                    if (plot == null) continue;
                    foreach (var depositObjectTag in plot.possibleDepositObjectTags)
                    {
                        var prefabsWithTag = Assets.GetPrefabsWithTag(depositObjectTag);
                        foreach (var receptacleGo in prefabsWithTag)
                        {
                            if (receptacleGo == null) continue;
                            var receptacleId = receptacleGo.PrefabID().ToString();
                            if (!receptacleId.StartsWith("artifact_")) continue;
                            var receptacleName = receptacleId;
                            var receptacleTuple = Def.GetUISprite(receptacleGo);
                            var receptacleSprite = receptacleTuple.first;
                            var receptacleColor = receptacleTuple.second;

                            ImageUtils.SaveImage(new Image
                            {
                                prefixes = new[] {"ASSETS/ARTIFACT", receptacleName.ToUpper()},
                                sprite = receptacleSprite,
                                color = receptacleColor,
                            });
                        }
                    }
                }
            }
        }
    }
}