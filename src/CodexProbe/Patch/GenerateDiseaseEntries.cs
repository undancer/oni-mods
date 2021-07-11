using UnityEngine;

namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateDiseaseEntries))]
    public static class GenerateDiseaseEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateDiseaseEntries");
            foreach (var resource in Db.Get().Diseases.resources)
            {
                if (resource.Disabled) continue;
                var name = resource.Id;
                var sprite = Assets.GetSprite("overlay_disease");
                var color = Color.clear;
//                 color = resource.overlayColour;
                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/DISEASE", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}