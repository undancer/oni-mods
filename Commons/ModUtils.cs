using System.Collections.Generic;
using KMod;

namespace undancer.Commons
{
    public static class ModUtils
    {
        private static readonly List<Label> Mods = Global.Instance.modManager.mods.FindAll(mod => mod.enabled)
            .ConvertAll(mod => mod.label);

        private const string RefreshModId = "1724518038";
        private const string NewRefreshModId = "2317581286";

        public static bool HasRefreshMod()
        {
            return HasMod(RefreshModId) || HasMod(NewRefreshModId);
        }


        public static bool HasMod(string id)
        {
            return Mods.Exists(label => id == label.id);
        }
    }
}