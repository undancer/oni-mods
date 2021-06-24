using System.Collections.Generic;
using HarmonyLib;
using KMod;

namespace undancer.SelectLastCarePackage
{
    public class SignMod :UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            Debug.Log($"[SignMod] OnLoad {harmony}");
        }

        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
        {
            base.OnAllModsLoaded(harmony, mods);
            foreach (var mod1 in mods)
            {
            }
            Debug.Log($"[SignMod] OnAllModsLoaded {harmony} - {mods}");
        }
    }
}