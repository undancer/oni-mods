using HarmonyLib;
using KMod;

namespace undancer.LogicCritterCountSensorExt
{
    public class ModLoader:UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
#if DEBUG
            ModUtil.RegisterForTranslation(typeof(Languages));
#else
            Localization.RegisterForTranslation(typeof(Languages));
#endif
        }
    }
}