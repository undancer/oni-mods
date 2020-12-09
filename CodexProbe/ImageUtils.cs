using System;
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
            var prefixes = new string[inputs.Length - 1];
            var last = inputs.Last();
            Array.ConstrainedCopy(inputs, 0, prefixes, 0, inputs.Length - 1);
            var value = prefixes.Join(null, "/");
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
            catch (IndexOutOfRangeException)
            {
                Debug.Log(" 导出失败");
                Debug.Log(r);
            }
            catch (NullReferenceException)
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