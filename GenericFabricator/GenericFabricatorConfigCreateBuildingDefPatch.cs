using System.Collections.Generic;
using System.Linq;
using Harmony;
using UnityEngine;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(GenericFabricatorConfig), nameof(GenericFabricatorConfig.CreateBuildingDef))]
    public static class GenericFabricatorConfigCreateBuildingDefPatch
    {
        public static void Postfix(ref BuildingDef __result)
        {
            __result.Deprecated = false;
        }
    }

    [HarmonyPatch(typeof(GenericFabricatorConfig), nameof(GenericFabricatorConfig.DoPostConfigureComplete))]
    public static class GenericFabricatorConfigDoPostConfigureCompletePatch
    {
        public static void Postfix()
        {
            var list = ComplexRecipeManager.Get().recipes.Select(recipe => recipe.id).ToList();
            foreach (var id in list)
            {
                var recipe = ComplexRecipeManager.Get().GetRecipe(id);
                var _recipe = new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(GenericFabricatorConfig.ID,
                        recipe.ingredients,
                        recipe.results)
                    , recipe.ingredients,
                    recipe.results)
                {
                    time = recipe.time,
                    description = recipe.description,
                    nameDisplay = recipe.nameDisplay,
                    fabricators = new List<Tag>
                    {
                        new Tag(GenericFabricatorConfig.ID)
                    },
                    sortOrder = recipe.sortOrder,
                };

                Debug.Log(_recipe);
            }

            var table = Traverse.Create(ComplexRecipeManager.Get()).Field("obsoleteIDMapping")
                .GetValue<Dictionary<string, string>>();
            foreach (var pair in table)
            {
                Debug.Log($"{pair.Key} => {pair.Value}");
            }
            
            foreach (var recipe in RecipeManager.Get().recipes)
            {
                Debug.Log($"recipe -> {recipe.Name}");
            }
        }
    }
}