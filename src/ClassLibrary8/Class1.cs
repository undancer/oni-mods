using System.Collections.Generic;
using ClassLibrary8.lang;
using HarmonyLib;
using STRINGS;

namespace ClassLibrary8
{
    public static class ModLoader
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
    public static class Class3
    {
        public static void Postfix()
        {
            Debug.Log(Class1.TEST);
            Debug.Log(UI.ALERTS);
        }
    }

//    [HarmonyPatch(typeof(Strings),"Add")]
    public static class Class2
    {
        public static void Postfix(IEnumerable<string> value)
        {
            Debug.Log("----------");
            foreach (var str in value) Debug.Log("LANG -> " + str);

            Debug.Log("----------");
        }
    }
}