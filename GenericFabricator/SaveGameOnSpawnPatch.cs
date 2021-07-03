using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(SaveGame), "OnPrefabInit")]
    public static class SaveGameOnSpawnPatch
    {
        private static bool _inited;

        private static List<string> fabricatorList = new List<string>
        {
            MicrobeMusherConfig.ID, //食物压制器
            CookingStationConfig.ID, //电动烤炉
            GourmetCookingStationConfig.ID, //燃气灶
            KilnConfig.ID, //窑炉
            RockCrusherConfig.ID, //碎石机
            MetalRefineryConfig.ID, //金属精炼器
            GlassForgeConfig.ID, //玻璃熔炉
            SupermaterialRefineryConfig.ID, //分子熔炉
            ClothingFabricatorConfig.ID, //纺织机
            SuitFabricatorConfig.ID, //太空服锻造
            EggCrackerConfig.ID, //打蛋器
            ApothecaryConfig.ID, //制药桌
        };

        public static void Postfix()
        {
            if (_inited) return;
            var recipeIdList = new List<string>();
            foreach (var recipe in ComplexRecipeManager.Get().recipes)
                if (!recipe.fabricators.First().Equals(new Tag(GenericFabricatorConfig.ID)))
                    recipeIdList.Add(recipe.id);

            foreach (var recipeId in recipeIdList)
            {
                var recipe = ComplexRecipeManager.Get().GetRecipe(recipeId);
                var fabricator = recipe.id.Substring(0, recipe.id.IndexOf('_'));
                var index = fabricatorList.IndexOf(fabricator);

                if (index >= 0)
                {
                    var recipeObj = new ComplexRecipe(
                        ComplexRecipeManager.MakeRecipeID(GenericFabricatorConfig.ID,
                            recipe.ingredients,
                            recipe.results)
                        , recipe.ingredients,
                        recipe.results)
                    {
                        time = recipe.time,
                        FabricationVisualizer = recipe.FabricationVisualizer,
                        nameDisplay = recipe.nameDisplay,
                        description = recipe.description,
                        fabricators = new List<Tag>
                        {
                            new Tag(GenericFabricatorConfig.ID)
                        },
                        sortOrder = (index + 1) * 1000 + recipe.sortOrder,
                    };
                }
            }

            _inited = true;
        }
    }
}