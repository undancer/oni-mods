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

                    LocString text = "";
                    switch (context.GetState())
                    {
                        case 1:
                            text = Languages.COUNT_CREATURES;
                            break;
                        case 2:
                            text = Languages.COUNT_EGG;
                            break;
                        case 3:
                            text = Languages.COUNT_CREATURES_AND_EGG;
                            break;
                        default:
                            text = Languages.COUNT_ERRORS;
                            break;
                    }

                    Game.Instance.userMenu.AddButton(__instance.gameObject,
                        new KIconButtonMenu.ButtonInfo("action_power", text, delegate
                        {
                            var state = context.GetState() - 1;
                            state = (state + 1) % 3;
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