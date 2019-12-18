using Harmony;
using Klei.AI;
using TUNING;
using UnityEngine;

namespace undancer.RandomOverjoyed
{
    [HarmonyPatch(typeof(JoyBehaviourMonitor.Instance), nameof(JoyBehaviourMonitor.Instance.ShouldBeOverjoyed))]
    public static class JoyBehaviourMonitorInstancePatch
    {
        public static bool Prefix(
            AttributeInstance ___qolAttribute,
            AttributeInstance ___expectationAttribute,
            ref bool __result)
        {
            var qav = ___qolAttribute.GetTotalValue();
            var eav = ___expectationAttribute.GetTotalValue();

            var val = MathUtil.ReRange(
                qav - eav,
                TRAITS.JOY_REACTIONS.MIN_MORALE_EXCESS,
                TRAITS.JOY_REACTIONS.MAX_MORALE_EXCESS,
                TRAITS.JOY_REACTIONS.MIN_REACTION_CHANCE,
                TRAITS.JOY_REACTIONS.MAX_REACTION_CHANCE
            );
            var rdm = Random.Range(0.0f, 100.0f);
            Debug.Log("val: " + val + " random:" + rdm);
            if (val < 50) val = 50;

            __result = rdm <= val;
            return false;
        }
    }
}