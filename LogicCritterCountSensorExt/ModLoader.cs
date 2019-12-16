using System.Collections.Generic;
using System.Reflection;
using Harmony;

namespace LogicCritterCountSensorExt
{
    public class ModLoader
    {
        public static void OnLoad()
        {
#if DEBUG
            ModUtil.RegisterForTranslation(typeof(Languages));
#else
            Localization.RegisterForTranslation(typeof(Languages));
#endif
        }
    }
}