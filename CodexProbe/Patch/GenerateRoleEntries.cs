using Harmony;
using UnityEngine;

namespace CodexProbe.Patch
{
    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateRoleEntries))]
    public static class GenerateRoleEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateRoleEntries");
            foreach (var resource in Db.Get().Skills.resources)
            {
                var name = resource.Id;
                var sprite = Assets.GetSprite(resource.hat);

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/ROLES", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white,
                });
            }
        }
    }
}