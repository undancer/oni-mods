using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateGeyserEntries))]
    public static class GenerateGeyserEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateGeyserEntries");
            const string prefix = "GeyserGeneric_";
            var prefabsWithComponent = Assets.GetPrefabsWithComponent<Geyser>();
            if (prefabsWithComponent != null)
            {
                foreach (var go in prefabsWithComponent)
                {
                    var name = go.PrefabID().ToString();
                    if (name.StartsWith(prefix))
                    {
                        name = name.Remove(0, prefix.Length);
                    }

                    var sprite = Def.GetUISprite(go).first;

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/CREATURES/SPECIES/GEYSERS", name.ToUpper()},
                        sprite = sprite,
                        color = Color.white
                    });
                }
            }
        }
    }
}