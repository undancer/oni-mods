using System.Collections.Generic;
using System.Reflection;
using Harmony;

namespace Nothing
{
    [HarmonyPatch(typeof(MainMenu), "OnPrefabInit")]
    public static class MainMenuOnPrefabInitPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            var method = AccessTools.Method(typeof(Hook), nameof(Hook.AlwaysTrue));
            foreach (var instruction in instructions)
            {
                if (instruction.ToString() == "call Boolean get_Initialized()") instruction.operand = method;
                yield return instruction;
            }
        }
    }
}