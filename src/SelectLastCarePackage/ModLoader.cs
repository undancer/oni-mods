using System.IO;
using System.Reflection;
using HarmonyLib;
using KMod;

namespace undancer.SelectLastCarePackage
{
    public class ModLoader : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
#if DEBUG
            ModUtil.RegisterForTranslation(typeof(Languages));
#else
            Localization.RegisterForTranslation(typeof(Languages));
#endif

            // RemoveConfig();
        }

        private static void RemoveConfig()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var str = Path.Combine(new FileInfo(directoryName).Directory.Parent.FullName, "config");
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }

            var _modConfigFolder = Path.Combine(str, new FileInfo(directoryName).Name);
            if (Directory.Exists(_modConfigFolder))
            {
                Directory.Delete(_modConfigFolder, true);
            }
        }
    }
}