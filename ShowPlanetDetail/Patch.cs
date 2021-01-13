using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using ProcGen;
using STRINGS;
using UnityEngine;

namespace undancer.ShowPlanetDetail
{
    public static class Hook
    {
        private static string GetPlanetName()
        {
            var worldName = "";
//            worldName = SaveLoader.Instance.GameInfo.worldID;
            worldName = SaveLoader.Instance.GameInfo.clusterId;
            var world = SettingsCache.worlds.GetWorldData(worldName);
            var name = Strings.Get(world.name);
            return string.Format(WORLDS.SURVIVAL_CHANCE.PLANETNAME, name);
        }

        private static string GetTraits()
        {
            if (SaveLoader.Instance.GameInfo.worldTraits != null)
                return SaveLoader.Instance.GameInfo.worldTraits.Select(worldTrait =>
                {
                    var cachedTrait = SettingsCache.GetCachedTrait(worldTrait, false);
                    return cachedTrait != null
                        ? string.Format("<color=#{1}>{0}</color>", Strings.Get(cachedTrait.name),
                            cachedTrait.colorHex)
                        : WORLD_TRAITS.MISSING_TRAIT.ToString();
                }).Join();

            return WORLD_TRAITS.NO_TRAITS.NAME;
        }

        public static void SetText(LocText text, string value)
        {
            if (SaveLoader.Instance == null)
            {
                text.SetText(value);
                return;
            }

            text.color = Color.white;
            text.SetText(new[]
            {
                value,
//                CustomGameSettings.Instance.GetSettingsCoordinate(),
                GetPlanetName(),
                GetTraits()
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
                if (instruction.ToString() == "callvirt Void SetText(System.String)") instruction.operand = method;

                yield return instruction;
            }

            /*
             return instructions.MethodReplacer(
                AccessTools.Method(typeof(LocText), nameof(LocText.SetText), new[] {typeof(string)}),
                AccessTools.Method(typeof(Hook), nameof(Hook.SetText))
            );
             */
        }
    }
}