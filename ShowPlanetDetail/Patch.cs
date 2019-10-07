using System.Linq;
using Harmony;
using Klei.CustomSettings;
using STRINGS;
using UnityEngine;

namespace ShowPlanetDetail
{
    [HarmonyPatch(typeof(BuildWatermark), "OnSpawn")]
    public static class BuildWatermarkOnSpawn
    {
        private static string GetWaterMark()
        {
            var str = "LU-" + (!Application.isEditor ? KleiVersion.ChangeList.ToString() : "<EDITOR>");
            return string.Format(UI.DEVELOPMENTBUILDS.WATERMARK, str);
        }

        private static string GetPlanetName(string world, int seed)
        {
            var data = new ColonyDestinationAsteroidData(world, seed);
            var name = data.GetParamDescriptors().Select(descriptor => descriptor.text).First();
            return name;
        }

        private static string GetTraits(string world, int seed)
        {
            var data = new ColonyDestinationAsteroidData(world, seed);
            var traits = data.GetTraitDescriptors().Join(descriptor => descriptor.text, ", ");
            return traits;
        }

        public static void Postfix(BuildWatermark __instance)
        {
            if (SaveLoader.Instance == null) return;
            var world = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.World).id;
            var seed = SaveLoader.Instance.worldDetailSave.globalWorldSeed;
            var text = __instance.textDisplay;
            text.SetText(new[]
            {
                GetWaterMark(),
//                CustomGameSettings.Instance.GetSettingsCoordinate(),
                GetPlanetName(world, seed),
                GetTraits(world, seed)
            }.Join(null, "\n"));
        }
    }
}