using Harmony;

namespace LogicCritterCountSensorExt
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
}