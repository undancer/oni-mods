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

            GenerateRecipeEntries();
//            GenerateArtifactEntries();
        }

        public static void GenerateRecipeEntries()
        {
            var recipes = ComplexRecipeManager.Get().recipes;
            var elementSet = new TagSet();
            foreach (var recipe in recipes)
            {
                // 配方
                Debug.Log("RECIPE : " + recipe.id);
                // 原料
                foreach (var recipeElement in recipe.ingredients)
                {
                    elementSet.Add(recipeElement.material);
                }
                // 制品
                foreach (var recipeElement in recipe.results)
                {
                    elementSet.Add(recipeElement.material);
                }
            }

            foreach (var element in elementSet)
            {
                var elementGo = Assets.GetPrefab(element);
                if (elementGo == null) continue;
                var elementName = elementGo.PrefabID().ToString();
                var elementTuple = Def.GetUISprite(element);
                var elementSprite = elementTuple.first;
                var elementColor = elementTuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/RECIPES", elementName.ToUpper()},
                    sprite = elementSprite,
                    color = elementColor,
                });
            }
        }

        public static void GenerateArtifactEntries()
        {
            var config = new ArtifactConfig();
            foreach (var go in config.CreatePrefabs())
            {
                var name = go.PrefabID().ToString();
                var tuple = Def.GetUISprite(go);
                var sprite = tuple.first;
                var color = tuple.second;
                
                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ARTIFACTS", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}