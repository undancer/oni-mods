// Decompiled with JetBrains decompiler
// Type: RefinementPlus.RefinementPlus_RockCrusherRecipes
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using Harmony;
using UnityEngine;

namespace RefinementPlus
{
    [HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate", typeof(GameObject), typeof(Tag))]
    public class RefinementPlusRockCrusherRecipes
    {
        private static bool Prefix(RockCrusherConfig __instance, ref GameObject go)
        {
            Debug.Log("Refinement Plus - RockCrusherConfig Postfix: ");
            Prioritizable.AddRef(go);
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            var fabricator = go.AddOrGet<ComplexFabricator>();
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            fabricator.duplicantOperated = true;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            var fabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricatorWorkable.overrideAnims = new[]
            {
                Assets.GetAnim((HashedString) "anim_interacts_rockrefinery_kanim")
            };
            fabricatorWorkable.workingPstComplete = new[]
            {
                (HashedString) "working_pst_complete"
            };
            var methods = new Methods();
            methods.clayToSlime();
            methods.limeCrushing();
            methods.fossilCrushing();
            methods.maficToRegolith();
            methods.mineralsCrushing();
            methods.oresToRefined();
            methods.organicToFert();
            methods.refinedToOres();
            methods.saltCrushing();
            methods.snowCone();
            return false;
        }
    }
}