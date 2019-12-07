using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;

namespace TelescopeAutoResearch
{
    public static class Hook
    {
        public static void GetUnAnalysisSpaceDestination(SpacecraftManager instance, int destId)
        {
            var previous = instance.GetStarmapAnalysisDestinationID();
            var prevDist = instance.GetDestination(previous).distance;
            var wormhole = instance.destinations.Last();
            var completed = instance.GetDestinationAnalysisState(wormhole) ==
                            SpacecraftManager.DestinationAnalysisState.Complete;

            var type = 1;
            var minDist = Int32.MaxValue;

            SpaceDestination next = null;

            foreach (var destination in instance.destinations)
            {
                var dist = destination.distance;
                if (instance.GetDestinationAnalysisState(destination) !=
                    SpacecraftManager.DestinationAnalysisState.Complete)
                {
                    if (dist <= minDist)
                    {
                        if (completed || prevDist <= 4)
                        {
                            if (dist >= prevDist)
                            {
                                //广度优先
                                type = 1;
                                next = destination;
                                minDist = dist;
                            }
                        }
                        else
                        {
                            if (dist > prevDist)
                            {
                                //深度优先
                                type = 2;
                                next = destination;
                                minDist = dist;
                            }
                        }
                    }
                }
            }

            if (next != null)
            {
                destId = next.id;
            }

            Debug.Log(type == 1 ? "广度优先" : "深度优先");
            Debug.Log("准备研究下一个星体:" + destId);
            instance.SetStarmapAnalysisDestinationID(destId);
        }
    }

    [HarmonyPatch(typeof(SpacecraftManager), "EarnDestinationAnalysisPoints")]
    public static class SpacecraftManagerEarnDestinationAnalysisPointsPatches
    {
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            var oldMethod = AccessTools.Method(typeof(SpacecraftManager),
                nameof(SpacecraftManager.SetStarmapAnalysisDestinationID));
            var newMethod = AccessTools.Method(typeof(Hook), nameof(Hook.GetUnAnalysisSpaceDestination));

            foreach (var instruction in instructions)
            {
                if (instruction.operand == oldMethod)
                {
                    instruction.operand = newMethod;
                }

                yield return instruction;
            }
        }
    }
}