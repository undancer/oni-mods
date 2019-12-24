using System.IO;
using Harmony;

namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexCache),nameof(CodexCache.CodexCacheInit))]
    public class CodexCacheInitPrefixPatch
    {
        public static void Prefix()
        {
            Debug.Log("CodexCacheInit");

            Directory.Delete("/Users/undancer/oni", true);
        }
    }
}