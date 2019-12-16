using System.Collections.Generic;
using ClassLibrary8.lang;
using Harmony;

namespace ClassLibrary8
{
    public class ModLoader
    {
        public static void OnLoad()
        {
            Debug.Log("OnLoad !!");
            Localization.RegisterForTranslation(typeof(Class1));
            
            
        }
    }

    [HarmonyPatch(typeof(Strings),"Add")]
    public class Class2
    {
        public static void Postfix(IEnumerable<string> value)
        {
            Debug.Log("----------");
            foreach (var str in value)
            {
                Debug.Log("LANG -> " +  str);
            }
            Debug.Log("----------");
        }
    }
}