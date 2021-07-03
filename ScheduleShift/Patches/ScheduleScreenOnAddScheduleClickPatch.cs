using System.Linq;
using HarmonyLib;

namespace undancer.ScheduleShift.Patches
{
    [HarmonyPatch(typeof(ScheduleScreen), "OnAddScheduleClick")]
    public static class ScheduleScreenOnAddScheduleClickPatch
    {
        public static bool Prefix()
        {
            var schedule = ScheduleManager.Instance.GetSchedules().LastOrDefault();
            ScheduleManager.Instance.AddSchedule(
                schedule != null ? schedule.GetGroups() : Db.Get().ScheduleGroups.allGroups);
            return false;
        }
    }
}