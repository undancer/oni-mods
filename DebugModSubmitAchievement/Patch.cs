using Harmony;

namespace DebugModSubmitAchievement
{
    [HarmonyPatch(typeof(ColonyAchievementTracker), "UnlockPlatformAchievement", typeof(string))]
    internal static class ColonyAchievementTrackerUnlockPlatformAchievement
    {
        public static bool Prefix(string achievement_id)
        {
            Debug.Log("检查成就" + achievement_id);

            var colonyAchievement = Db.Get().ColonyAchievements.Get(achievement_id);
            if (colonyAchievement == null || string.IsNullOrEmpty(colonyAchievement.steamAchievementId))
                return true;

            if (SteamAchievementService.Instance != null)
                SteamAchievementService.Instance.Unlock(colonyAchievement.steamAchievementId);
            else
                Debug.LogWarningFormat("Steam achievement [{0}] was achieved, but achievement service was null",
                    (object) colonyAchievement.steamAchievementId);

            return false;
        }
    }
}