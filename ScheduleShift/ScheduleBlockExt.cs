namespace undancer.ScheduleShift
{
    public static class ScheduleBlockExt
    {
        public static ScheduleGroup GetScheduleGroup(this ScheduleBlock block)
        {
            ScheduleGroup group;
            var scheduleGroups = Db.Get().ScheduleGroups;

            switch (block.GroupId)
            {
                case nameof(scheduleGroups.Hygene):
                    @group = scheduleGroups.Hygene;
                    break;
                case nameof(scheduleGroups.Worktime):
                    @group = scheduleGroups.Worktime;
                    break;
                case nameof(scheduleGroups.Recreation):
                    @group = scheduleGroups.Recreation;
                    break;
                case nameof(scheduleGroups.Sleep):
                    @group = scheduleGroups.Sleep;
                    break;
                default:
                    @group = scheduleGroups.Worktime;
                    break;
            }

            return new ScheduleGroup(group.Id, scheduleGroups, 1, group.Name, group.description,
                group.notificationTooltip, group.allowedTypes, group.alarm);
        }
    }
}