using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Harmony;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ClassLibrary10
{
    [HarmonyPatch(typeof(CodexScreen), "OnActivate")]
    public class Class1
    {
        public static void Postfix()
        {
            Debug.Log(" !! Postfix !! ");

            foreach (var element in ElementLoader.elements.Where(element => !element.IsVacuum))
            {
                try
                {
                    var image = new CodexImage(32, 32, Def.GetUISprite(element));
                    var sprite = image.sprite;
                    var color = image.color;

                    var r = sprite.textureRect;
                    var texture = sprite.texture.DeCompress();
                    Texture2D texture2D = null;
                    try
                    {
                        texture2D = texture.CropTexture((int) r.xMin, (int) r.yMin, (int) r.width, (int) r.height);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Debug.Log(element.tag.Name + " 导出失败");
                        Debug.Log(r);
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.Log(element.tag.Name + " 导出失败");
                        Debug.Log(r);
                    }

                    if (texture2D != null)
                    {
                        texture = texture2D;
                    }

                    if (!color.Equals(Color.white))
                    {
                        texture = texture.FillTexture(color);
                    }


                    var data = texture.EncodeToPNG();
                    var path = Path.Combine("/Users/undancer/oni", element.tag.Name.ToUnderscore() + ".png");
                    File.WriteAllBytes(path, data);

                    //    var path = Path.Combine("/Users/undancer/oni",                 texture.name + ".png");
                    //    var data = texture.DeCompress().EncodeToPNG();
                    //    File.WriteAllBytes(path,data);
                }
                catch (Exception e)
                {
                    Debug.Log(element.tag.Name);
                    Debug.LogException(e);
                    Debug.Log("");
                }
            }
        }
    }
}