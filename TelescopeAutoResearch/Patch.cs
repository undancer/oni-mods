using System.Collections.Generic;
using System.Linq;
using Database;
using Harmony;
using STRINGS;

namespace undancer.TelescopeAutoResearch
{
    public static class SpaceDestinationTypeExtends
    {
        public static string GetNotificationColor(this SpaceDestinationType type)
        {
            var mappings = new Dictionary<string, string>
            {
                {Db.Get().SpaceDestinationTypes.Wormhole.Id, "ED2DFE"},
                {Db.Get().SpaceDestinationTypes.RedDwarf.Id, "D32F2F"}
            };
            mappings.TryGetValue(type.Id, out var color);
            return color ?? "FFC107";
        }

        public static Notification GetNotification(this SpaceDestinationType type,
            NotificationType notificationType = NotificationType.Good,
            string group = "SDT")
        {
            var color = type.GetNotificationColor();
            var title = string.Format(UI.STARMAP.ANALYSIS_AMOUNT.text,
                $" <color=#{color}><b>{type.Name}</b></color>");
            var notification = new Notification(
                title,
                notificationType,
                group,
                (list, o) => type.description);
            return notification;
        }
    }

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
            var minDist = int.MaxValue;

            SpaceDestination next = null;

            foreach (var destination in instance.destinations)
            {
                var dist = destination.distance;
                if (instance.GetDestinationAnalysisState(destination) !=
                    SpacecraftManager.DestinationAnalysisState.Complete)
                    if (dist <= minDist)
                    {
                        if (completed || prevDist < 4)
                        {
                            if (dist >= prevDist || prevDist == wormhole.distance)
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

            if (next != null) destId = next.id;

            Debug.Log(type == 1 ? "广度优先" : "深度优先");
            Debug.Log("准备研究下一个星体:" + destId);
            instance.SetStarmapAnalysisDestinationID(destId);

            var notifier = instance.FindOrAddComponent<Notifier>();
            var destinationType = instance.GetDestination(previous).GetDestinationType();
            notifier.Add(destinationType.GetNotification(group: "SpaceHook"));
        }
    }

    [HarmonyPatch(typeof(SpacecraftManager), "EarnDestinationAnalysisPoints")]
    public static class SpacecraftManagerEarnDestinationAnalysisPointsPatches
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.MethodReplacer(
                AccessTools.Method(typeof(SpacecraftManager),
                    nameof(SpacecraftManager.SetStarmapAnalysisDestinationID)),
                AccessTools.Method(typeof(Hook),
                    nameof(Hook.GetUnAnalysisSpaceDestination))
            );
        }
    }
}