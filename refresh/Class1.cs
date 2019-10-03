using System;
using Harmony;

namespace refresh
{
 //   [HarmonyPatch(typeof(ImmigrantScreen),"OnRejectAll")]
    [HarmonyPatch(typeof(InterfaceTool),"ActivateTool")]
    public class Class1
    {

        public static void Prefix()
        {
            Debug.Log("SUCCESS !!");
            Console.WriteLine("SUCCESS !!");

        }
    }
}