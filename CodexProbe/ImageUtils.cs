using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Harmony;
using UnityEngine;

namespace CodexProbe
{
    public class ImageUtils
    {
        public static string GetPath(string[] inputs)
        {
            var mapping = new Dictionary<string, string>
            {
                {"Root", ""},
                {"ROOT", ""},
                {"Root/BUILDINGS", "BUILDINGS"},
                {"Root/CREATURES", "CREATURES"},
                {"Root/DISEASE", "DISEASE"},
                {"Root/ELEMENTS", "ELEMENTS"},
                {"Root/EQUIPMENT", "EQUIPMENT"},
                {"Root/FOOD", "FOOD"},
                {"Root/GEYSERS", "GEYSERS"},
                {"Root/HOME", "HOME"},
                {"Root/LESSONS", "LESSONS"},
                {"Root/PLANTS", "PLANTS"},
                {"Root/ROLES", "ROLES"},
                {"Root/TECH", "TECH"},

                {"BUILDCATEGORYAUTOMATION", "BUILDINGS/BUILDCATEGORYAUTOMATION"},
                {"BUILDCATEGORYBASE", "BUILDINGS/BUILDCATEGORYBASE"},
                {"BUILDCATEGORYCONVEYANCE", "BUILDINGS/BUILDCATEGORYCONVEYANCE"},
                {"BUILDCATEGORYEQUIPMENT", "BUILDINGS/BUILDCATEGORYEQUIPMENT"},
                {"BUILDCATEGORYFOOD", "BUILDINGS/BUILDCATEGORYFOOD"},
                {"BUILDCATEGORYFURNITURE", "BUILDINGS/BUILDCATEGORYFURNITURE"},
                {"BUILDCATEGORYHVAC", "BUILDINGS/BUILDCATEGORYHVAC"},
                {"BUILDCATEGORYMEDICAL", "BUILDINGS/BUILDCATEGORYMEDICAL"},
                {"BUILDCATEGORYOXYGEN", "BUILDINGS/BUILDCATEGORYOXYGEN"},
                {"BUILDCATEGORYPLUMBING", "BUILDINGS/BUILDCATEGORYPLUMBING"},
                {"BUILDCATEGORYPOWER", "BUILDINGS/BUILDCATEGORYPOWER"},
                {"BUILDCATEGORYREFINING", "BUILDINGS/BUILDCATEGORYREFINING"},
                {"BUILDCATEGORYROCKETRY", "BUILDINGS/BUILDCATEGORYROCKETRY"},
                {"BUILDCATEGORYUTILITIES", "BUILDINGS/BUILDCATEGORYUTILITIES"},

                {"ELEMENTSGAS", "ELEMENTS/ELEMENTSGAS"},
                {"ELEMENTSLIQUID", "ELEMENTS/ELEMENTSLIQUID"},
                {"ELEMENTSOTHER", "ELEMENTS/ELEMENTSOTHER"},
                {"ELEMENTSSOLID", "ELEMENTS/ELEMENTSSOLID"},
            };

            var prefixes = new string[inputs.Length - 1];
            var last = inputs.Last();
            Array.ConstrainedCopy(inputs, 0, prefixes, 0, inputs.Length - 1);
            var key = prefixes.Join(null, "/");
            mapping.TryGetValue(key, out var value);

            if (value == null)
            {
                value = key;
                Debug.Log("key -> " + key);
            }

            return new[] {value, last}.Join(null, "/");
        }

        public static string GetFileName(string[] inputs, string prefix, string suffix)
        {
            while (true)
            {
                var path = Path.Combine(prefix, GetPath(inputs) + suffix);
                var dirs = Path.GetDirectoryName(path);
                if (!Directory.Exists(dirs))
                {
                    Directory.CreateDirectory(dirs);
                }

                if (!File.Exists(path)) return path;
                var match = Regex.Match(suffix, "_(\\d+)\\.");
                var index = match.Success ? int.Parse(match.Groups["1"].Value) : 0;
                suffix = "_" + (index + 1) + "." + suffix.Split('.').Last();
            }
        }

        public static void SaveImage(Image image)
        {
            var prefixes = image.prefixes;
            var sprite = image.sprite;
            var color = image.color;

            var r = sprite.textureRect;
            var texture = sprite.texture;
            var temp = texture.DeCompress();
            try
            {
                temp = temp.CropTexture((int) r.xMin, (int) r.yMin, (int) r.width, (int) r.height);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log(" 导出失败");
                Debug.Log(r);
            }
            catch (NullReferenceException e)
            {
                Debug.Log(" 导出失败");
                Debug.Log(r);
            }

            if (!color.Equals(Color.white))
            {
                temp = temp.FillTexture(color);
            }

            var data = temp.EncodeToPNG();

            var path = GetFileName(prefixes, "/Users/undancer/oni", ".png");

            File.WriteAllBytes(path, data);
        }
    }
}