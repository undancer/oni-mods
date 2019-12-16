using System;
using Harmony;

namespace undancer.LogicCritterCountSensorExt
{
    [HarmonyPatch(typeof(LogicCritterCountSensor), "Sim200ms")]
    public class LogicCritterCountSensorSim200MsPatch
    {
        public static bool Prefix(
            LogicCritterCountSensor __instance,
            bool ___activateOnGreaterThan,
            int ___countThreshold,
            KSelectable ___selectable,
            float dt)
        {
            var roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(__instance.gameObject);
            if (roomOfGameObject != null)
            {
//                Debug.Log("房间系统" + roomOfGameObject);
                var currentCount = 0;

                var context = __instance.FindOrAddComponent<LogicCritterCountSensorContext>();

                if (context.CountCreatures)
                {
                    var count = roomOfGameObject.cavity.creatures.Count;
//                    Debug.Log("小动物：" + count);
                    currentCount += count;
                }

                if (context.CountEggs)
                {
                    var count = roomOfGameObject.cavity.eggs.Count;
//                    Debug.Log("蛋：" + count);
                    currentCount += count;
                }

                __instance.SetCurrentCount(currentCount);

                Traverse.Create(__instance).Method("SetState", new[] {typeof(bool)}, new object[]
                    {
                        !___activateOnGreaterThan
                            ? currentCount < ___countThreshold
                            : currentCount > ___countThreshold
                    })
                    .GetValue();
                if (!___selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
                    return false;
                ___selectable.RemoveStatusItem(Traverse.Create(__instance).Field("roomStatusGUID").GetValue<Guid>());
            }
            else
            {
//                Debug.Log("没有房间");

                if (!___selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
                    Traverse.Create(__instance).Field("roomStatusGUID")
                        .SetValue(___selectable.AddStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom));
                Traverse.Create(__instance).Method("SetState", new[] {typeof(bool)}, new object[] {false})
                    .GetValue();
            }

            return false;
        }
    }
}