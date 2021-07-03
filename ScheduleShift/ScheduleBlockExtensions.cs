namespace undancer.ScheduleShift
{
    public static class ScheduleBlockExtensions
    {
        public static ScheduleGroup GetScheduleGroup(this ScheduleBlock block)
        {
            var scheduleGroups = Db.Get().ScheduleGroups;
            var group = scheduleGroups.Get(block.GroupId);
            return new ScheduleGroup(group.Id, scheduleGroups, 1, group.Name, group.description,
                group.notificationTooltip, group.allowedTypes, group.alarm);
        }
    }
}