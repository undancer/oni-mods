using Harmony;

namespace GenericFabricator
{
    [HarmonyPatch(typeof(ComplexFabricator), "ValidateNextOrder")]
    public static class ComplexFabricatorValidateNextOrderPatch
    {
        public static bool Prefix(ComplexRecipe[] ___recipe_list, int ___nextOrderIdx)
        {
            return ___recipe_list.Length > ___nextOrderIdx;
        }
    }
}