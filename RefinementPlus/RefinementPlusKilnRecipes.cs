// Decompiled with JetBrains decompiler
// Type: RefinementPlus.RefinementPlusKilnRecipes
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using Harmony;

namespace RefinementPlus
{
    [HarmonyPatch(typeof (KilnConfig), "ConfgiureRecipes")]
    public class RefinementPlusKilnRecipes
    {
        public static void Postfix()
        {
            Debug.Log("Refinement Plus - KilnConfig Postfix: ");
            var methods = new Methods();
            methods.burnCloth();
            methods.burnRot();
            methods.seedsToCoal();
        }
    }
}