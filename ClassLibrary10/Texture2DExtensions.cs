using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace ClassLibrary10
{

    public static class StringExt
    {
        public static string ToUnderscore(this string str)
        {
            var output = Regex.Replace(str, "([A-Z])", "_$1").ToLower();
            if (output.StartsWith("_"))
            {
                output = output.Substring(1);
            }
            return output;
        }
    }
    public static class Texture2DExtensions
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

//                    Debug.Log("x: " + x + " y: " + y + " -> " + pColor.ToHexString() + " | " + pColor.gamma.ToHexString() + " | " + pColor.linear.ToHexString());
//                    if (Color.white.CompareRGB(pColor))
                    if (pColor.Compare(Color.white,0.25f))
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

        public static bool Compare(this Color _this, Color _that,float approach = 0.25f)
        {
            var _this_ = _this.ToVector3().sqrMagnitude;
            var _that_ = _that.ToVector3().sqrMagnitude;
            return (_this_ / _that_) > approach;
        }

        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }
    }
}