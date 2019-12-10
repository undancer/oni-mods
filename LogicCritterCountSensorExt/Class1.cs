using System;
using Harmony;

namespace ClassLibrary4
{
    public static class LogicCritterCountSensorExt
    {
        public static void SetCurrentCount(this LogicCritterCountSensor instance, int count)
        {
            Traverse.Create(instance).Field("currentCount").SetValue(count);
        }

        public static int GetCurrentCount(this LogicCritterCountSensor instance)
        {
            return Traverse.Create(instance).Field("currentCount").GetValue<int>();
        }
    }

    public class LogicCritterCountSensorContext : KMonoBehaviour
    {
        public bool Initialed { get; set; }
        public bool CountCreatures { get; set; }
        public bool CountEggs { get; set; }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();

//            Debug.Log("初始化 ！！");

            if (!Initialed)
            {
                CountCreatures = true;
                CountEggs = true;
                Initialed = true;
            }
        }

        public int GetState()
        {
            var state = 0;
            if (CountCreatures)
            {
                state += 1;
            }

            if (CountEggs)
            {
                state += 2;
            }

            return state;
        }
    }

    [HarmonyPatch(typeof(LogicCritterCountSensor), "Sim200ms")]
    public class Class2
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

    [HarmonyPatch(typeof(LogicCritterCountSensor), "OnSpawn")]
    public class Class1
    {
        public static void Postfix(LogicCritterCountSensor __instance)
        {
            __instance.Subscribe((int) GameHashes.RefreshUserMenu,
                data =>
                {
                    Debug.Log(__instance);
                    Debug.Log(data);

                    var context = __instance.FindOrAddComponent<LogicCritterCountSensorContext>();
                    
                    var text = "";
                    switch (context.GetState())
                    {
                        case 1:
                            text = "统计：小动物";
                            break;
                        case 2:
                            text = "统计：蛋";
                            break;
                        case 3:
                            text = "统计：小动物和蛋";
                            break;
                        default:
                            text = "统计：错误";
                            break;
                    }

                    Game.Instance.userMenu.AddButton(__instance.gameObject,
                        new KIconButtonMenu.ButtonInfo("action_power", text, delegate
                        {
                            var state = context.GetState() - 1;
                            state = (state  + 1) % 3;
                            switch (state)
                            {
                                case 0:
                                    context.CountCreatures = true;
                                    context.CountEggs = false;
                                    break;
                                case 1:
                                    context.CountCreatures = false;
                                    context.CountEggs = true;
                                    break;
                                case 2:
                                    context.CountCreatures = true;
                                    context.CountEggs = true;
                                    break;
                                default:
                                    context.CountCreatures = true;
                                    context.CountEggs = true;
                                    break;
                            }
                        })
                    );
                }
            );
        }
    }
}