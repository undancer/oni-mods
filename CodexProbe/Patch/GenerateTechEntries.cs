using Harmony;
using UnityEngine;

namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateTechEntries))]
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
            GenerateCometEntries();
            GenerateMiscPickupableEntries();
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

        public static void GenerateCometEntries()
        {
            var cometList = new[]
            {
                DustCometConfig.ID,
                RockCometConfig.ID,
                CopperCometConfig.ID,
                GoldCometConfig.ID,
                IronCometConfig.ID,
            };

            foreach (var comet in cometList)
            {
                var name = comet;
                var go = ((Tag) comet).Prefab();

                var tuple = Def.GetUISprite(go);
                var sprite = tuple.first;
                var color = tuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/COMETS", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }

        public static void GenerateMiscPickupableEntries()
        {
            foreach (var go in Assets.GetPrefabsWithTag(GameTags.MiscPickupable))
            {
                var name = go.PrefabID().ToString();
                var tuple = Def.GetUISprite(go);
                var sprite = tuple.first;
                var color = tuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ITEMS/OTHERS", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}