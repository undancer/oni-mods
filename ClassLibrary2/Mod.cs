using undancer.Commons;

namespace ClassLibrary2
{
    public static class Mod
    {
        public static Config Config;

        public static void OnLoad()
        {
            Config = ConfigUtils.LoadConfig<Config>();
        }
    }
}