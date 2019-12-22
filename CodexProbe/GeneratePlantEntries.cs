using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GeneratePlantEntries))]
    public static class GeneratePlantEntries
    {
        public static void Postfix()
        {
            Debug.Log("GeneratePlantEntries");
            var cache = new List<string>();

            var prefabsWithComponent = Assets.GetPrefabsWithComponent<Harvestable>();
            prefabsWithComponent.AddRange(Assets.GetPrefabsWithComponent<WiltCondition>());

            foreach (var go in prefabsWithComponent)
            {
                var name = go.PrefabID().ToString();

                if (cache.Contains(name))
                {
                    continue;
                }

                cache.Add(name);

                var sprite = Def.GetUISprite(go).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/PLANTS", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white
                });
            }
        }
    }
}