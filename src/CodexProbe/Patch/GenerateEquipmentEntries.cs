using System.Linq;
using UnityEngine;

namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateEquipmentEntries))]
    public static class GenerateEquipmentEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateEquipmentEntries");
            var prefabsWithComponent = Assets.GetPrefabsWithComponent<Equippable>();
            if (prefabsWithComponent != null)
            {
                foreach (var go in prefabsWithComponent)
                {
                    var flag = false;
                    var component = go.GetComponent<Equippable>();
                    if (component.def.AdditionalTags != null)
                    {
                        if (component.def.AdditionalTags.Any(additionalTag =>
                            additionalTag == GameTags.DeprecatedContent))
                        {
                            flag = true;
                        }
                    }

                    if (flag || component.hideInCodex) continue;

                    var name = go.PrefabID().ToString();

                    var sprite = Def.GetUISprite(go).first;

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/EQUIPMENTS", name.ToUpper()},
                        sprite = sprite,
                        color = Color.white
                    });
                }
            }
        }
    }
}