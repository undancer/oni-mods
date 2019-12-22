using Harmony;
using TUNING;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    public static class GenerateFoodEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateFoodEntries");
            foreach (var info in FOOD.FOOD_TYPES_LIST)
            {
                var name = info.Id;
                var sprite = Def.GetUISprite(info.ConsumableId).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/FOOD", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white
                });
            }
        }
    }
}