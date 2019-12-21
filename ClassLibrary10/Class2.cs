using UnityEngine;

namespace ClassLibrary10
{
    public class Class2
    {
        public void foo()
        {
            foreach (var camera in Camera.allCameras)
            {
                RenderTexture rt = RenderTexture.GetTemporary(100, 100);
                camera.targetTexture = rt;
                camera.Render();

                GameObject o = new GameObject();
                var image = o.FindOrAddUnityComponent<UnityEngine.UI.Image>();
                image.fillCenter = true;

//                Graphics.DrawTexture();

                RenderTexture p = RenderTexture.active;
                RenderTexture.active = rt;

                Texture2D ss = new Texture2D(100, 100);
            }
        }
    }
}