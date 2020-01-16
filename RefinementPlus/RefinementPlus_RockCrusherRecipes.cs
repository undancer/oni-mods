// Decompiled with JetBrains decompiler
// Type: RefinementPlus.RefinementPlus_RockCrusherRecipes
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using Harmony;
using UnityEngine;

namespace RefinementPlus
{
  [HarmonyPatch(typeof (RockCrusherConfig), "ConfigureBuildingTemplate", new System.Type[] {typeof (GameObject), typeof (Tag)})]
  public class RefinementPlus_RockCrusherRecipes
  {
    private static bool Prefix(RockCrusherConfig __instance, ref GameObject go)
    {
      Debug.Log((object) "Refinement Plus - RockCrusherConfig Postfix: ");
      Prioritizable.AddRef(go);
      go.AddOrGet<DropAllWorkable>();
      go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
      ComplexFabricator fabricator = go.AddOrGet<ComplexFabricator>();
      fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
      fabricator.duplicantOperated = true;
      go.AddOrGet<FabricatorIngredientStatusManager>();
      go.AddOrGet<CopyBuildingSettings>();
      ComplexFabricatorWorkable fabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
      BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
      fabricatorWorkable.overrideAnims = new KAnimFile[1]
      {
        Assets.GetAnim((HashedString) "anim_interacts_rockrefinery_kanim")
      };
      fabricatorWorkable.workingPstComplete = (HashedString[]) (HashedString) "working_pst_complete";
      Methods methods = new Methods();
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
