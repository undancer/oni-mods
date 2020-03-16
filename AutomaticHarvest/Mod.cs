using undancer.Commons;

namespace undancer.AutomaticHarvest
{
    public static class Mod
    {
        public static Config.Config Config;

        public static void OnLoad()
        {
            Config = ConfigUtils.LoadConfig<Config.Config>();
        }
    }
}