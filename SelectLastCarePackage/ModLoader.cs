namespace undancer.SelectLastCarePackage
{
    public static class ModLoader
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