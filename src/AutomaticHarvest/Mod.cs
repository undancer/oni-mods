using HarmonyLib;
using KMod;
using undancer.Commons;

namespace undancer.AutomaticHarvest
{
    public class Mod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            Context.Config = ConfigUtils.LoadConfig<Config.Config>();
        }
    }
}