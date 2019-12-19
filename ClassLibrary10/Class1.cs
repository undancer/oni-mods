using System.Collections.Generic;
using System.IO;
using Harmony;
using STRINGS;
using UnityEngine;

namespace ClassLibrary10
{
    public static class C
    {
        
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
    
    [HarmonyPatch(typeof(CodexScreen),"OnActivate")]
    public class Class1
    {
        

        public static void Postfix()
        {
            Debug.Log(" !! Postfix !! ");
            
            foreach (var element in ElementLoader.elements)
            {
                Tuple<Sprite, Color> sprite =                 Def.GetUISprite(element);
                var image =                 new CodexImage(32,32,sprite);
                var path = Path.Combine("/Users/undancer/oni",                 element.tag.Name + ".png");
                Debug.Log(path);
                Debug.Log(image.sprite);
                Debug.Log(image.sprite.texture);
                var data = image.sprite.texture.DeCompress().EncodeToJPG();
                File.WriteAllBytes(path,data);
            }

        }
        
    }
}