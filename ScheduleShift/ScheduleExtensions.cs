using System.Collections.Generic;
using System.Linq;

namespace undancer.ScheduleShift
{
    public static class ScheduleExtensions
    {
        public static List<ScheduleGroup> GetGroups(this Schedule schedule)
        {
            return schedule.GetBlocks().Select(block => block.GetScheduleGroup()).ToList();
        }

        public static void LeftShift(this Schedule schedule)
        {
            var list = schedule.GetGroups();
            for (var index = 0; index < list.Count; index++)
                schedule.SetGroup(index, list[(list.Count + index + 1) % list.Count]);
        }

        public static void RightShift(this Schedule schedule)
        {
            var list = schedule.GetGroups();
            for (var index = 0; index < list.Count; index++)
                schedule.SetGroup(index, list[(list.Count + index - 1) % list.Count]);
        }
    }
}