using Harmony;

namespace LogicCritterCountSensorExt
{
   [HarmonyPatch(typeof(LogicCritterCountSensor), "OnSpawn")]
    public class LogicCritterCountSensorOnSpawnPatch
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