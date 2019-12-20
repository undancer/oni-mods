using System;
using System.Collections.Generic;
using System.IO;
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

            foreach (var camera in Camera.allCameras)
            {
                Debug.Log(camera);
            }

            foreach (var element in ElementLoader.elements)
            {
                if (element.tag.Name != "LiquidOxygen")
                {
                    continue;
                }

                try
                {
                    var image = new CodexImage(32, 32, Def.GetUISprite(element));
                    var sprite = image.sprite;
                    var color = image.color;
                    Debug.Log(element.name);
                    Debug.Log(element.IsSolid);
                    Debug.Log(element.IsLiquid);
                    Debug.Log(element.IsGas);
                    Debug.Log(element.tag.Name);
                    Debug.Log(sprite.name);
                    Debug.Log(color);
                    Debug.Log(sprite.texture.format);

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

                    texture = texture.FillTexture(color);

                    var data = texture.EncodeToPNG();
                    var path = Path.Combine("/Users/undancer/oni", element.tag.Name + ".png");
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