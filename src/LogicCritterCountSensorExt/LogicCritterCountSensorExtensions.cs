using HarmonyLib;

namespace undancer.LogicCritterCountSensorExt
{
    public static class LogicCritterCountSensorExtensions
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
}