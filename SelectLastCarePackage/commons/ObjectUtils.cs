using HarmonyLib;

namespace undancer.Commons
{
    public static class ObjectUtils
    {
        public static void SetField(this object obj, string name, object value)
        {
            AccessTools.Field(obj.GetType(), name).SetValue(obj, value);
        }

        public static T GetField<T>(this object obj, string name)
        {
            return (T) AccessTools.Field(obj.GetType(), name).GetValue(obj);
        }
    }
}