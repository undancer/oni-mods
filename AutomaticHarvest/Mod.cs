using HarmonyLib;
using KMod;
using undancer.AutomaticHarvest.Commons;

namespace undancer.AutomaticHarvest
{
    public class Mod:UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            Context.Config = ConfigUtils.LoadConfig<Config.Config>();
        }
    }
}