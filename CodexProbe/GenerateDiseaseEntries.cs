using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateDiseaseEntries))]
    public static class GenerateDiseaseEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateDiseaseEntries");
            foreach (var resource in Db.Get().Diseases.resources)
            {
                if (resource.Disabled) continue;
                var name = resource.Id;
                var sprite = Assets.GetSprite( "overlay_disease");
                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/DISEASE", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white,
                });
            }
        }
    }
}