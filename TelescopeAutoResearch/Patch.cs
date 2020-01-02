using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Harmony;

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

        public static Notification GetNotification(this SpaceDestinationType type, string group = "SDT")
        {
            var color = type.GetNotificationColor();
            var title = string.Format(STRINGS.UI.STARMAP.ANALYSIS_AMOUNT.text,
                $" <color=#{color}><b>{type.Name}</b></color>");
            var notification = new Notification(
                title,
                NotificationType.Good,
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
            notifier.Add(destinationType.GetNotification("SpaceHook"));
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

//    [HarmonyPatch(typeof(SpacecraftManager), nameof(SpacecraftManager.SetStarmapAnalysisDestinationID))]
    public static class SpacecraftManagerSetStarmapAnalysisDestinationIDPathch
    {
        public static void Postfix(
            SpacecraftManager __instance,
            int id)
        {
            Debug.Log("SetStarmapAnalysisDestinationID");
            Debug.Log(id);
            var notifier = __instance.FindOrAddComponent<Notifier>();

//            Notifier notifier = new Notifier();

            /* foreach (var value in Enum.GetValues(typeof(NotificationType)).OfType<NotificationType>())
             {
                 var notification = new Notification($"HAHA {value.ToString()}", value,
                     new HashedString($"SP_Group_{value.ToString()}"));
                 notifier.Add(notification);
                 Debug.Log(value.ToString());
             }*/

            /*
            planet.SetLabel(
                
                !flag ? 
                    (string) STRINGS.UI.STARMAP.UNKNOWN_DESTINATION + "\n" +
                    
                    string.Format(STRINGS.UI.STARMAP.ANALYSIS_AMOUNT.text, (object) GameUtil.GetFormattedPercent((float) (100.0 * ((double) SpacecraftManager.instance.GetDestinationAnalysisScore(KVP.Key) / (double) TUNING.ROCKETRY.DESTINATION_ANALYSIS.COMPLETE)), GameUtil.TimeSlice.None)) 
                    
                    : 
                    
                    destinationType.Name + "\n<color=#979798> " + GameUtil.GetFormattedDistance((float) ((double) KVP.Key.OneBasedDistance * 10000.0 * 1000.0)) + "</color>"
                    );
                    */


            foreach (var color in new[]
            {
                /*    "ffffff",
                    "ff0000",
                    "00ff00",
                    "0000ff",
                    "4791db",
                    "1976d2",
                    "115293",
                    "e33371",
                    "dc004e",
                    "9a0036",
                    "e57373",
                    "f44336",
                    "d32f2f",
                */
                "ffd54f",
                "ffc107",
                "ffa000",
                /* "64b5f6",
                 "2196f3",
                 "1976d2",
                 "81c784",
                 "4caf50",
                 "388e3c",*/

                "93ff33", "56c1ff", "ed2dfe", "33ffe0", "ff5b5b", "f7ff1a", "61ff91", "ffffff"
            })
            {
                var text = string.Format(STRINGS.UI.STARMAP.ANALYSIS_AMOUNT.text,
                    " " + $"<color=#{color}><b>" +
                    STRINGS.UI.SPACEDESTINATIONS.WORMHOLE.NAME.text
                    + "</b></color>" + $" -> {color}"
                );
                var n = new Notification(text, NotificationType.Good, new HashedString($"GROUP_{color}"));
                notifier.Add(n);
            }
        }
    }
}