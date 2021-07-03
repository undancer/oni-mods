using System.Collections.Generic;
using HarmonyLib;
using undancer.Commons;
using UnityEngine;

namespace undancer.SelectLastCarePackage.patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnRejectAll")]
    public static class ImmigrantScreenOnRejectAllPatch //按下拒绝全部按钮
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
            deliverableContainerList = __instance.GetField<List<ITelepadDeliverableContainer>>("containers");
            deliverableContainerList.ForEach(c => Object.Destroy(c.GetGameObject()));
            deliverableContainerList.Clear();
            instance.Method("InitializeContainers").GetValue();
            deliverableContainerList = __instance.GetField<List<ITelepadDeliverableContainer>>("containers");
            deliverableContainerList.ForEach(c =>
                {
                    if (c is CharacterContainer characterContainer) characterContainer.SetReshufflingState(false);
                }
            );
            _lastTime = Time.realtimeSinceStartup;
            return false;
        }
    }
}