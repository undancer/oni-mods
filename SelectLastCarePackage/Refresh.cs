using System.Collections.Generic;
using System.Linq;
using Harmony;
using UnityEngine;

namespace SelectLastCarePackage
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnRejectAll")]
    internal static class Refresh
    {
        private const string RefreshModId = "1724518038";

        private static float _lastTime = 0.0f;


        private static bool HasRefreshMod()
        {
            return ModUtils.HasMod(RefreshModId);
        }

        private static bool Prefix(ImmigrantScreen __instance)
        {
            if (HasRefreshMod())
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
            {
                characterContainer.SetReshufflingState(false);
            }

            _lastTime = Time.realtimeSinceStartup;
            return false;
        }
    }
}