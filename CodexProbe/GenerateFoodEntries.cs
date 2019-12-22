using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    public static class GenerateFoodEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateFoodEntries");
            foreach (var info in TUNING.FOOD.FOOD_TYPES_LIST)
            {
                var name = info.Id;
                var sprite = Def.GetUISprite(info.ConsumableId).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ITEMS/FOOD", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white
                });
            }
        }
    }
}