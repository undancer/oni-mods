using System.Collections.Generic;
using ClassLibrary8.lang;
using Harmony;
using KMod;

namespace ClassLibrary8
{
    public class ModLoader
    {
        public static void OnLoad()
        {
            Debug.Log("OnLoad !!");
#if DEBUG
            ModUtil.RegisterForTranslation(typeof(Class1));
#else
            Localization.RegisterForTranslation(typeof(Class1));
#endif
        }
    }

    [HarmonyPatch(typeof(Localization), nameof(Localization.Initialize))]
    public class Class3
    {
        public static void Postfix()
        {
            Debug.Log(lang.Class1.TEST);
            Debug.Log(STRINGS.UI.ALERTS);
        }
    }

//    [HarmonyPatch(typeof(Strings),"Add")]
    public class Class2
    {
        public static void Postfix(IEnumerable<string> value)
        {
            Debug.Log("----------");
            foreach (var str in value)
            {
                Debug.Log("LANG -> " + str);
            }

            Debug.Log("----------");
        }
    }
}