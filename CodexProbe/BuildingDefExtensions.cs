using UnityEngine;

namespace CodexProbe
{
    public static class BuildingDefExtensions
    {
        public static void SaveImage(this BuildingDef def)
        {
            ImageUtils.SaveImage(new Image
            {
                sprite = def.GetUISprite(),
                color = Color.white,
                prefixes = new[] {"ASSETS/BUILDINGS", def.PrefabID.ToUpper()}
            });
        }
        
        
    }
}