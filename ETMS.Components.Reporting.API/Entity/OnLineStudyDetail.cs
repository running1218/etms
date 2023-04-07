using System.Collections.Generic;

namespace ETMS.Components.Reporting.API.Entity
{
    public class OnLineStudyDetail
    {
        public List<OnlineItemCourse> ItemCourse { get; set; }
        public List<OnlineItemCourseTest> ItemCourseTest { get; set; }
        public List<OnlineItemCourseWare> ItemCourseWare { get; set; }
    }
}
