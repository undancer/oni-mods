using Harmony;
using Klei.AI;
using TUNING;

namespace RandomOverjoyed
{
    [HarmonyPatch(typeof(JoyBehaviourMonitor.Instance), "ShouldBeOverjoyed")]
    public class JoyBehaviourMonitorInstancePatch
    {
        public static bool Prefix(JoyBehaviourMonitor.Instance __instance, ref bool __result)
        {
            var instance = Traverse.Create(__instance);
            var qav = instance.Field("qolAttribute").GetValue<AttributeInstance>().GetTotalValue();
            var eav = instance.Field("expectationAttribute").GetValue<AttributeInstance>().GetTotalValue();

            var val = MathUtil.ReRange(
                qav - eav,
                TRAITS.JOY_REACTIONS.MIN_MORALE_EXCESS,
                TRAITS.JOY_REACTIONS.MAX_MORALE_EXCESS,
                TRAITS.JOY_REACTIONS.MIN_REACTION_CHANCE,
                TRAITS.JOY_REACTIONS.MAX_REACTION_CHANCE
            );
            var rdm = UnityEngine.Random.Range(0.0f, 100.0f);
            Debug.Log("val: " + val + " random:" + rdm);
            if (val < 50)
            {
                val = 50;
            }

            __result = rdm <= val;
            return false;
        }
    }
}