using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;

namespace TelescopeAutoResearch
{
    [HarmonyPatch(typeof(SpacecraftManager), "SetStarmapAnalysisDestinationID")]
    public static class SpacecraftManagerSetStarmapAnalysisDestinationID
    {
        private static SpaceDestination GetLastUnAnalysisSpaceDestination()
        {
            var list = SpacecraftManager.instance.destinations;

            return list.First(destination => SpacecraftManager.instance.GetDestinationAnalysisState(destination) !=
                                             SpacecraftManager.DestinationAnalysisState.Complete);
        }


        public static void Postfix(SpacecraftManager __instance, int id)
        {
            if (id == -1)
            {
                var destination = GetLastUnAnalysisSpaceDestination();
                Debug.Log("准备研究下一个星体:" + destination.id);
                if (StarmapScreen.Instance != null)
                {
                    if (StarmapScreen.Instance.analyzeButton.CurrentState == 0)
                    {
                        Debug.Log("手工暂停，不干了。");
                        return;
                    }
                    else if (StarmapScreen.Instance.analyzeButton.CurrentState == 1)
                    {
                        
                        Debug.Log("开始研究");

                        var method = StarmapScreen.Instance.GetType().GetMethod("SelectDestination");
                        method?.Invoke(StarmapScreen.Instance, new object[] {destination});
                    }
                    
                }
                else
                {
                    Debug.Log("初次进入游戏且研究完成？");
                }
                
                
                Traverse.Create(__instance).Field("analyzeDestinationID").SetValue(destination.id);
                __instance.Trigger(532901469, destination.id);
                
            }
            
            
            
        }
    }
}