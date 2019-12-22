using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateCreatureEntries))]
    public static class GenerateCreatureEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateCreatureEntries");
            var brains = Assets.GetPrefabsWithComponent<CreatureBrain>();
            if (brains != null)
            {
                foreach (var go in brains)
                {
                    if (go.GetDef<BabyMonitor.Def>() == null)
                    {
                        var name = go.PrefabID().ToString();
                        var prefab = Assets.TryGetPrefab((Tag) (go.PrefabID() + "Baby"));
                        var brain = go.GetComponent<CreatureBrain>();
                        var species = brain.species.ToString();
                        if (prefab != null)
                        {
                            var prefabName = prefab.PrefabID().ToString();
                            var prefabSprite = Def.GetUISprite(prefab).first;

                            ImageUtils.SaveImage(new Image
                            {
                                prefixes = new[] {"ASSETS/CREATURES", species.ToUpper(), prefabName.ToUpper()},
                                sprite = prefabSprite,
                                color = Color.white,
                            });
                        }

                        var sprite = Def.GetUISprite(go).first;

                        ImageUtils.SaveImage(new Image
                        {
                            prefixes = new[] {"ASSETS/CREATURES", species.ToUpper(), name.ToUpper()},
                            sprite = sprite,
                            color = Color.white,
                        });
                    }
                }
            }
        }
    }
}