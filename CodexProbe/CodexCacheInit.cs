using System;
using System.IO;
using Harmony;

namespace CodexProbe
{
    //    GeneratePageNotFound

    [HarmonyPatch(typeof(CodexCache), nameof(CodexCache.CodexCacheInit))]
    public static class CodexCacheInit
    {
        public static void Prefix()
        {
            Debug.Log("CodexCacheInit");

            Directory.Delete("/Users/undancer/oni", true);
        }

        public static void Postfix()
        {
            Debug.Log("CodexCacheInit");
            /*
             foreach (var prefabId in Assets.Prefabs)
            {
                var name = prefabId.PrefabID().ToString();
                Debug.Log("PID: " + name);
                var go = prefabId.PrefabID().Prefab();
                if (go == null) continue;
                try
                {
                    var tuple = Def.GetUISprite(go);
                    var sprite = tuple.first;
                    var color = tuple.second;

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/TAGS", name.ToUpper()},
                        sprite = sprite,
                        color = color,
                    });
                }
                catch (Exception e)
                {
                    Debug.Log(go);
                    Debug.LogError(e);
                }

                //CAREPACKAGE
            }
            */
        }
    }
}