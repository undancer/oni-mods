using System.Collections.Generic;
using KMod;

namespace SelectLastCarePackage
{
    public static class ModUtils
    {
        private static readonly List<Label> Mods = Global.Instance.modManager.mods.FindAll(mod => mod.enabled)
            .ConvertAll(mod => mod.label);

        private const string RefreshModId = "1724518038";

        public static bool HasRefreshMod()
        {
            return HasMod(RefreshModId);
        }


        public static bool HasMod(string id)
        {
            return Mods.Exists(label => id == label.id);
        }
    }
}