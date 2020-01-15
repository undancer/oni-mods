using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(SaveGame), "OnPrefabInit")]
    public static class SaveGameOnSpawnPatch
    {
        public static void Postfix()
        {
            var recipeIdList = new List<string>();
            foreach (var recipe in ComplexRecipeManager.Get().recipes)
            {
                if (!recipe.fabricators.First().Equals(new Tag(GenericFabricatorConfig.ID)))
                {
                    {
                        recipeIdList.Add(recipe.id);
                    }
                }
            }

            foreach (var recipeId in recipeIdList)
            {
                var recipe = ComplexRecipeManager.Get().GetRecipe(recipeId);
                
                var recipeObj = new ComplexRecipe(
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
            }
        }
    }
}