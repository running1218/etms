using System.Collections.Generic;

namespace ETMS.Components.Reporting.API.Entity
{
    public class OnlineStudyStatistics
    {
        public List<OnlineItemCourseStatistics> ItemCourseStatistics { get; set; }
        public List<OnlineItemCourseTest> ItemCourseTest { get; set; }
    }
}
