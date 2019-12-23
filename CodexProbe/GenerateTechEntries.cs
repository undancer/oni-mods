using Harmony;
using UnityEngine;

namespace CodexProbe
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateTechEntries))]
    public static class GenerateTechEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateTechEntries");
            foreach (var resource in Db.Get().Techs.resources)
            {
                //科技研究没有图标
            }

            GenerateSpaceEntries();
        }

        public static void GenerateSpaceEntries()
        {
            foreach (var resource in Db.Get().SpaceDestinationTypes.resources)
            {
                var name = resource.Id;
                var sprite = Assets.GetSprite(resource.spriteName);

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/SPACES", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white,
                });                
            }
        }
    }
}