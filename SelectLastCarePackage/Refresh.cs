using System.Collections.Generic;
using System.Linq;
using Harmony;
using undancer.Commons;
using UnityEngine;

namespace undancer.SelectLastCarePackage
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnSpawn")]
    internal static class RefreshOnOnSpawn
    {
        private static void Postfix(ImmigrantScreen __instance)
        {
            if (ModUtils.HasRefreshMod()) return;
            __instance.GetField<KButton>("rejectButton").SetText(Languages.SHUFFLE);
        }
    }

    [HarmonyPatch(typeof(ImmigrantScreen), "OnRejectAll")]
    internal static class Refresh
    {
        private static float _lastTime;

        public static bool Prefix(ImmigrantScreen __instance)
        {
            if (ModUtils.HasRefreshMod())
            {
                Debug.Log("启用了刷新选人Mod，跳过");
                return true;
            }

            Debug.Log("没有启用刷新选人Mod，刷新");

            if (Time.realtimeSinceStartup - _lastTime < 0.5)
            {
                Debug.Log("-------------" + Time.realtimeSinceStartup + "-----------lastTime:" + _lastTime);
                return false;
            }

            _lastTime = Time.realtimeSinceStartup;
            var instance = Traverse.Create(__instance);
            List<ITelepadDeliverableContainer> deliverableContainerList = null;
            deliverableContainerList = instance.Field("containers").GetValue<List<ITelepadDeliverableContainer>>();
            deliverableContainerList.ForEach(c => Object.Destroy(c.GetGameObject()));
            deliverableContainerList.Clear();
            instance.Method("InitializeContainers").GetValue();
            deliverableContainerList = instance.Field("containers").GetValue<List<ITelepadDeliverableContainer>>();
            foreach (var characterContainer in deliverableContainerList.OfType<CharacterContainer>())
                characterContainer.SetReshufflingState(false);
            _lastTime = Time.realtimeSinceStartup;
            return false;
        }
    }
}