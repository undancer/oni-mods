using System.IO;
using System.Reflection;
using Klei;

namespace undancer.Commons
{
    public static class ConfigUtils
    {
        public static T LoadConfig<T>(string file = "config.yaml") where T : new()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (directoryName != null)
            {
                var directoryInfo = new DirectoryInfo(directoryName);
                var config = Path.Combine(directoryName, file);
                Debug.Log(config);

                var fileInfo = new FileInfo(config);

                if (!fileInfo.Exists)
                {
                    var cfg = new T();
                    YamlIO.Save(cfg, config);
                    return cfg;
                }

                return YamlIO.LoadFile<T>(config,
                    delegate(YamlIO.Error error, bool warning) { Debug.Log($"ERROR - {error}"); });
            }

            return new T();
        }
    }
}