using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Harmony;
using UnityEngine;

namespace CodexProbe
{
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

                ImageUtils.SaveImage(image);
            }

            Debug.Log("文件生成完成 !!");
        }
    }
}