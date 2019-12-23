using System.Collections.Generic;
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
            
            Directory.Delete("/Users/undancer/oni",true);
        }
    }
}