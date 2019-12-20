using System;
using System.Collections.Generic;
using System.IO;
using Harmony;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ClassLibrary10
{
    public static class C
    {
        public static Texture2D CropTexture(this Texture2D pSource, int left, int top, int width, int height)
        {
            if (left < 0)
            {
                width += left;
                left = 0;
            }

            if (top < 0)
            {
                height += top;
                top = 0;
            }

            if (left + width > pSource.width)
            {
                width = pSource.width - left;
            }

            if (top + height > pSource.height)
            {
                height = pSource.height - top;
            }

            if (width <= 0 || height <= 0)
            {
                return null;
            }

            var rt = RenderTexture.GetTemporary(
                pSource.width,
                pSource.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear
            );


            Graphics.Blit(pSource, rt);

            var previous = RenderTexture.active;

            RenderTexture.active = rt;

            var texture = new Texture2D(width, height);
            texture.ReadPixels(new Rect(left, top, width, height), 0, 0);
            texture.Apply();
            
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(rt);
            return texture;

            /*
             


            //*** Make New
            Texture2D oNewTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            Color[] aColor = null;
            Color[] aSourceColor = pSource.GetPixels(0);


            //*** Make destination array
            int xLength = width * height;
            aColor = new Color[xLength];

            Debug.Log("left : " + left);
            Debug.Log("top : " + top);
            Debug.Log("width : " + width);
            Debug.Log("height : " + height);
            Debug.Log("length : " + aSourceColor.Length);
            int i = 0;
            for (int y = 0; y < height; y++)
            {
                int sourceIndex = (y + top) * pSource.width + left;
                for (int x = 0; x < width; x++)
                {
                    aColor[i++] = aSourceColor[sourceIndex++];
                }
            }

            foreach (var color in aColor)
            {
                Debug.Log(color);
            }

            if (aColor != null)
            {
                //*** Set Pixels
                oNewTex.SetPixels(aColor);
                oNewTex.Apply();
            }

            //*** Return
            return oNewTex;
             */
        }

        public static Texture2D FillTexture(this Texture2D source, Color color)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            var newTex = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            for (var y = 0; y < source.height; y++)
            {
                for (var x = 0; x < source.width; x++)
                {
                    var pColor = source.GetPixel(x, y);
                    Color newColor;

                    Debug.Log("x: " + x + " y: " + y + " -> " + pColor);
                    if (Color.white.CompareRGB(pColor))
                    {
                        newColor = color;
                        newColor.a = pColor.a;
                    }
                    else
                    {
                        newColor = pColor;
                    }

                    newTex.SetPixel(x, y, newColor);
                }
            }

            newTex.Apply();
            return newTex;
        }

        public static Texture2D DeCompress(this Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;

            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);

            return readableText;
        }
    }

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