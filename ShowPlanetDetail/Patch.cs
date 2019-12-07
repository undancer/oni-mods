using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using Klei.CustomSettings;
using UnityEngine;

namespace ShowPlanetDetail
{
    public static class Hook
    {
        private static string GetPlanetName(string world, int seed)
        {
            var data = new ColonyDestinationAsteroidData(world, seed);
            var name = data.GetParamDescriptors().Select(descriptor => descriptor.text).First();
            return name;
        }

        private static string GetTraits(string world, int seed)
        {
            var data = new ColonyDestinationAsteroidData(world, seed);
            var traits = data.GetTraitDescriptors().Join(descriptor => descriptor.text);
            return traits;
        }

        public static void SetText(LocText text, string value)
        {
            if (SaveLoader.Instance == null)
            {
                text.SetText(value);
                return;
            }

            var world = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.World).id;
            var seed = SaveLoader.Instance.worldDetailSave.globalWorldSeed;
            text.color = Color.white;
            text.SetText(new[]
            {
                value,
//                CustomGameSettings.Instance.GetSettingsCoordinate(),
                GetPlanetName(world, seed),
                GetTraits(world, seed)
            }.Join(null, "\n"));
        }
    }

    [HarmonyPatch(typeof(BuildWatermark), "OnSpawn")]
    public static class BuildWatermarkOnSpawnPatches
    {
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            var method = AccessTools.Method(typeof(Hook), nameof(Hook.SetText));

            foreach (var instruction in instructions)
            {
                if (instruction.ToString() == "callvirt Void SetText(System.String)")
                {
                    instruction.operand = method;
                }

                yield return instruction;
            }
        }
    }
}