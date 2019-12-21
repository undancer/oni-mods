using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Harmony;
using UnityEngine;

namespace ClassLibrary10
{
    public class Image
    {
        public Sprite sprite { get; set; }
        public Color color { get; set; }
        public string[] prefixes { get; set; }
    }

    [HarmonyPatch(typeof(CodexScreen), "OnActivate")]
    public class Class1
    {
        public static bool log = false;

        public static Dictionary<string, string[]> cache = new Dictionary<string, string[]>();

        public static List<Image> GetImage()
        {
            var list = new List<Image>();

            var entries = (Dictionary<string, CodexEntry>)
                AccessTools.Field(typeof(CodexCache), "entries").GetValue(null);

            var subEntries = (Dictionary<string, SubEntry>)
                AccessTools.Field(typeof(CodexCache), "subEntries").GetValue(null);

            var prefixes = new List<string>();

            foreach (var entry in entries.Select(keyValuePair => keyValuePair.Value))
            {
                list.AddRange(GetImage(entry, prefixes.ToArray()));
            }

            foreach (var entry in subEntries.Select(keyValuePair => keyValuePair.Value))
            {
                list.AddRange(GetImage(entry, prefixes.ToArray()));
            }

            return list;
        }

        public static List<Image> GetImage(CodexEntry entry, string[] prefixes = null)
        {
            if (log)
            {
                Debug.Log("GetImage CodexEntry");
                Debug.Log("entry id -> " + entry.id);
                Debug.Log("entry name -> " + entry.name);
                Debug.Log("entry tit -> " + entry.title);
                Debug.Log("entry sub -> " + entry.subtitle);
                Debug.Log("entry cat -> " + entry.category);
                Debug.Log("entry icon -> " + entry.iconPrefabID);
            }

            prefixes = prefixes?.Add(entry.category).Add(entry.id).ToArray();

            var list = new List<Image>();
//            entry.GetFirstWidget();

            foreach (var contentContainer in entry.contentContainers)
            {
                list.AddRange(GetImage(contentContainer, prefixes));
            }

            foreach (var subEntry in entry.subEntries)
            {
                list.AddRange(GetImage(subEntry, prefixes));
            }


            return list;
        }


        public static List<Image> GetImage(SubEntry entry, string[] prefixes = null)
        {
            if (log)
            {
                Debug.Log("GetImage SubEntry");
                Debug.Log("sub id -> " + entry.id);
                Debug.Log("sub name -> " + entry.name);
                Debug.Log("sub tit -> " + entry.title);
                Debug.Log("sub sub -> " + entry.subtitle);
                Debug.Log("sub lock -> " + entry.lockID);
            }

            prefixes = prefixes?.Add(entry.id).ToArray();

            var list = new List<Image>();

            foreach (var contentContainer in entry.contentContainers)
            {
                list.AddRange(GetImage(contentContainer, prefixes));
            }

            return list;
        }

        public static List<Image> GetImage(ContentContainer container, string[] prefixes = null)
        {
//            Debug.Log("GetImage ContentContainer");
            var id = container.lockID;
            var go = container.go;
            var content = container.content;

            if (prefixes != null)
            {
                var last = prefixes.Last();
                if (!cache.ContainsKey(last))
                {
                    cache.Add(last, prefixes);
                }

                cache.TryGetValue(last, out prefixes);
            }

            var list = new List<Image>();

            if (content != null)
            {
                list.AddRange(
                    content.OfType<CodexImage>()
                        .Select(image => new Image {sprite = image.sprite, color = image.color, prefixes = prefixes})
                );

                list.AddRange(
                    content.OfType<CodexLabelWithIcon>()
                        .Select(input =>
                        {
                            var text = Regex.Replace(input.label.text, "<.*?>", "");
                            return new Image
                            {
                                sprite = input.icon.sprite,
                                color = input.icon.color,
                                prefixes = prefixes.ToArray().Add(text).ToArray()
                            };
                        })
                );

                list.AddRange(
                    content.OfType<CodexIndentedLabelWithIcon>()
                        .Select(input =>
                        {
                            var text = Regex.Replace(input.label.text, "<.*?>", "");
                            return new Image
                            {
                                sprite = input.icon.sprite,
                                color = input.icon.color,
                                prefixes = prefixes.ToArray().Add(text).ToArray()
                            };
                        })
                );

                if (log)
                {
                    foreach (var widget in content)
                    {
                        switch (widget)
                        {
                            case CodexText text:
                                Debug.Log(">> " + text.text);
                                break;
                            case CodexLabelWithIcon text:
                                Debug.Log(">> " + text.label.text + "•");
                                break;
                            case CodexIndentedLabelWithIcon text:
                                Debug.Log(">> " + text.label.text + "•");
                                break;
                            default:
                                Debug.Log(widget.GetType());
                                break;
                        }
                    }
                }
            }

            return list;
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

        public static string GetFileName(string[] inputs, string prefix, string suffix)
        {
            while (true)
            {
                var path = Path.Combine(prefix, inputs.Join(null, "/") + suffix);
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

        public static void Postfix()
        {
            Debug.Log(" !! Postfix !! ");

            var list = GetImage();

            foreach (var image in list)
            {
                Debug.Log(image.prefixes.Join());
                Debug.Log(image.sprite);
                Debug.Log(image.color);
                Debug.Log("");

                SaveImage(image);
            }

            Debug.Log("文件生成完成 !!");
        }
    }
}