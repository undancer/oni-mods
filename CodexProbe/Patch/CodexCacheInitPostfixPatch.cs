using UnityEngine;

namespace CodexProbe.Patch
{
    //    GeneratePageNotFound

//    [HarmonyPatch(typeof(CodexCache), nameof(CodexCache.CodexCacheInit))]
    public static class CodexCacheInitPostfixPatch
    {
        public static void Postfix()
        {
            Debug.Log("CodexCacheInit");

            foreach (var keyValuePair in Assets.Sprites)
            {
                var sprite = keyValuePair.Value;
                var name = sprite.name;

                if (name.StartsWith("action_")
                    || name.StartsWith("arrow_")
                    || name.StartsWith("asteroid_")
                    || name.StartsWith("Asteroid_")
                    || name.StartsWith("attribute_")
                    || name.StartsWith("codex")
                    || name.StartsWith("crew_state_")
                    || name.StartsWith("digIcon")
                    || name.StartsWith("electrical_")
                    || name.StartsWith("icon_")
                    || name.StartsWith("klei_logo")
                    || name.StartsWith("ONI_logo")
                    || name.StartsWith("legend_")
                    || name.StartsWith("mode_")
                    || name.StartsWith("overlay_")
                    || name.StartsWith("plant_status_")
                    || name.StartsWith("priority_")
                    || name.StartsWith("research_type_")
                    || name.StartsWith("skillbadge_role_")
                    || name.StartsWith("status_item_")
                    || name.StartsWith("tier_")
                    || name.StartsWith("ui_elements-")
                    || name.StartsWith("ui_duplicant_portrait_placeholder")
                    || name.StartsWith("ui_icon_")
                    || name.EndsWith("Icon")
                    || name.EndsWith("logo")
                )
                {
                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/ICONS", name},
                        sprite = sprite,
                        color = Color.white,
                    });
                }
                else
                {
                    /*
                     ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/AS", name},
                        sprite = sprite,
                        color = Color.white,
                    });    
                    */
                }
            }

            /*
             foreach (var prefabId in Assets.Prefabs)
            {
                var name = prefabId.PrefabID().ToString();
                Debug.Log("PID: " + name);
                var go = prefabId.PrefabID().Prefab();
                if (go == null) continue;
                try
                {
                    var tuple = Def.GetUISprite(go);
                    var sprite = tuple.first;
                    var color = tuple.second;

                    ImageUtils.SaveImage(new Image
                    {
                        prefixes = new[] {"ASSETS/TAGS", name.ToUpper()},
                        sprite = sprite,
                        color = color,
                    });
                }
                catch (Exception e)
                {
                    Debug.Log(go);
                    Debug.LogError(e);
                }

                //CAREPACKAGE
            }
            */
        }
    }
}