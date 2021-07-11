using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace CodexProbe.Jobs
{
    public class BaseJob
    {
        public static void SaveImage(Image image)
        {
            ImageUtils.SaveImage(image);
        }

        public static void SaveImage(string[] prefixes, Sprite sprite, Color color)
        {
            SaveImage(new Image
            {
                prefixes = prefixes,
                sprite = sprite,
                color = color,
            });
        }

        public static void WriteJson(string[] prefixes, object obj)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
            };

            var json = JsonConvert.SerializeObject(obj, settings);

            WriteJson(prefixes, json);
        }

        public static void WriteJson(string[] prefixes, string json)
        {
            Debug.Log("json");
            Debug.Log(json);
            File.WriteAllText(ImageUtils.GetFileName(prefixes, "/Users/undancer/oni", ".json"), json, Encoding.UTF8);
        }
    }
}