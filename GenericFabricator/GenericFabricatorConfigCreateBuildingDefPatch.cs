using System.Collections.Generic;
using System.Linq;
using Harmony;
using UnityEngine;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(GenericFabricatorConfig), nameof(GenericFabricatorConfig.CreateBuildingDef))]
    public static class GenericFabricatorConfigCreateBuildingDefPatch
    {
        public static void Postfix(ref BuildingDef __result)
        {
            __result.Deprecated = false;
        }
    }
}