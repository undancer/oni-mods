using System.Collections.Generic;
using KMod;

namespace SelectLastCarePackage
{
    public static class ModUtils
    {
        private static readonly List<Label> Mods = Global.Instance.modManager.mods.FindAll(mod => mod.enabled)
            .ConvertAll(mod => mod.label);


        public static bool HasMod(string id)
        {
            return Mods.Exists(label => id == label.id);
        }
    }
}