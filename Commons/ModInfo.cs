using undancer.Commons.Configuration;

namespace undancer.Commons
{
    public class ModInfo
    {
        public static void Initialize<T>(Action configButtons) where T : IConfiguration, new()
        {
            //   ModInfo.Name = ((AssemblyTitleAttribute) Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof (AssemblyTitleAttribute), false)).Title;
            //   ModInfo.Id = ((AssemblyConfigurationAttribute) Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof (AssemblyConfigurationAttribute), false)).Configuration;
            //   ModInfo.ModDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\", "/");
            //   ModInfo.Settings = (IConfiguration) Configuration<T>.Instance;
            //   ModInfo.ScreenManager = (IModScreenManager) new ModScreenManager<T>(configButtons);
            //   Logger.Start("Version: " + ModInfo.Version + " initialized");
        }
    }
}