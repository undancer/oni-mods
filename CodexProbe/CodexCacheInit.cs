using Harmony;

namespace CodexProbe
{
    //    GeneratePageNotFound
    
    [HarmonyPatch(typeof(CodexCache), nameof(CodexCache.CodexCacheInit))]
    public static class CodexCacheInit
    {
        public static void Postfix()
        {
            Debug.Log("CodexCacheInit");
        }
    }
}