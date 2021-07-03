using UnityEngine;

namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateCreatureEntries))]
    public static class GenerateCreatureEntries
    {
        public static void Postfix()
        {
            const string suffix = "Species";

            Debug.Log("GenerateCreatureEntries");
            var brains = Assets.GetPrefabsWithComponent<CreatureBrain>();
            if (brains == null) return;
            foreach (var go in brains)
            {
                if (go.GetDef<BabyMonitor.Def>() != null) continue;
                var name = go.PrefabID().ToString();
                var brain = go.GetComponent<CreatureBrain>();
                var species = brain.species.ToString();
                if (species.EndsWith(suffix))
                {
                    species = species.Remove(species.Length - suffix.Length);
                }

                var sprite = Def.GetUISprite(go).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/CREATURES", species.ToUpper(), name.ToUpper()},
                    sprite = sprite,
                    color = Color.white,
                });

                var babyGo = Assets.TryGetPrefab((go.PrefabID() + "Baby").ToTag());
                if (babyGo == null) continue;
                var babyName = babyGo.PrefabID().ToString();
                var babySprite = Def.GetUISprite(babyGo).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/CREATURES", species.ToUpper(), babyName.ToUpper()},
                    sprite = babySprite,
                    color = Color.white,
                });

                var eggGos = Assets.GetPrefabsWithTag((go.PrefabID() + "Egg").ToTag());
                if (eggGos == null || eggGos.Count <= 0) continue;
                foreach (var eggGo in eggGos)
                {
                    if (eggGo == null) continue;
                    var eggName = eggGo.PrefabID().ToString();
                    var eggSprite = Def.GetUISprite(eggGo).first;

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/CREATURES", species.ToUpper(), eggName.ToUpper()},
                        sprite = eggSprite,
                        color = Color.white,
                    });
                }
            }
        }
    }
}