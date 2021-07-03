// Decompiled with JetBrains decompiler
// Type: RefinemenPlus.TemplateMod_Game_OnPrefabInit
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using HarmonyLib;

namespace RefinemenPlus
{
    [HarmonyPatch(typeof(Game), "OnPrefabInit")]
    internal class TemplateModGameOnPrefabInit
    {
        private static void Postfix(Game __instance)
        {
            Debug.Log(" === TemplateMod_Game_OnPrefabInit Postfix === ");
        }
    }
}