using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace undancer.Commons.Configuration
{
    public class Configuration<T> where T : IConfiguration, new()
    {
        private static T _Instance;
        private static string _modConfigFolder;
        private static JsonSerializerSettings _settings;
        private static JsonSerializer _serializer;

        private static string FilePath { get; set; }

        public static T Instance
        {
            get
            {
                if (_Instance == null) LoadConfig();

                return _Instance;
            }
            set => _Instance = value;
        }

        public static void LoadConfig()
        {
            Debug.Log("LoadConfig");

            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Debug.Log(directoryName);

            if (directoryName == null)
                throw new DirectoryNotFoundException("Can't find root directory for config.json");
            var str = Path.Combine(new FileInfo(directoryName).Directory.Parent.FullName, "config");
            if (!Directory.Exists(str))
                Directory.CreateDirectory(str);


            Debug.Log(str);

            _modConfigFolder = Path.Combine(str, new FileInfo(directoryName).Name);
            if (!Directory.Exists(_modConfigFolder))
                Directory.CreateDirectory(_modConfigFolder);
            FilePath = Path.Combine(_modConfigFolder, "config.json");
            _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DefaultValueHandling = DefaultValueHandling.Populate,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            _serializer = JsonSerializer.CreateDefault(_settings);
            Instance = LoadConfig(FilePath);
        }

        protected static T LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log("Not found config.json (" + path + ") creating default");
                Instance = new T();
                Instance.InitializeDefaultValues();
                Save();
                return Instance;
            }

            T obj;
            using (var streamReader = new StreamReader(path))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    try
                    {
                        obj = _serializer.Deserialize<T>(jsonTextReader);
                        jsonTextReader.Close();
                    }
                    catch
                    {
                        obj = new T();
                        Debug.Log("Failed to load existing, created config file (" + path + ")");
                        Instance = obj;
                        Save();
                    }
                }

                streamReader.Close();
            }

            return Instance = obj;
        }

        public static void Save()
        {
            using (var streamWriter = new StreamWriter(FilePath))
            {
                var jsonTextWriter = new JsonTextWriter(streamWriter);
                _serializer.Serialize(jsonTextWriter, Instance);
                jsonTextWriter.Close();
            }
        }
    }
}