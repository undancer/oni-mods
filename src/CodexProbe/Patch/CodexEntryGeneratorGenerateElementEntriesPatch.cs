using System.Collections.Generic;
using System.Linq;
using CodexProbe.Jobs;
using HarmonyLib;
using UnityEngine;

namespace CodexProbe.Patch
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateElementEntries))]
    public static class CodexEntryGeneratorGenerateElementEntriesPatch
    {
        public static void Postfix()
        {
            Debug.Log("GenerateElementEntries");
            GenerateElementEntriesJob.GenerateElementEntries();
            GenerateIndustrialProductEntries();
        }

        public static void GenerateIndustrialProductEntries()
        {
            var list = new List<GameObject>();
            list.AddRange(Assets.GetPrefabsWithTag(GameTags.IndustrialIngredient));
            list.AddRange(Assets.GetPrefabsWithTag(GameTags.IndustrialProduct));
            list.AddRange(Assets.GetPrefabsWithTag(GameTags.Medicine));
            list.AddRange(Assets.GetPrefabsWithTag(GameTags.MedicalSupplies));
            list.Add(Assets.GetPrefab(MachinePartsConfig.TAG));
            list = list.Distinct().ToList();

            foreach (var go in list)
            {
                var name = go.PrefabID().ToString();
                var tuple = Def.GetUISprite(go);
                var sprite = tuple.first;
                var color = tuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ITEMS/INDUSTRIAL_PRODUCTS", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}