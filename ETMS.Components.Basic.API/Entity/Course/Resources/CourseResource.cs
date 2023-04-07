using System.Collections.Generic;

namespace ETMS.Components.Basic.API.Entity.Course.Resources
{
    public class CourseResource
    {
        public List<CourseResourceDetail> CourseResourceInfo
        {
            get;
            set;
        }
    }

    public class CourseResourceDetail
    {
        public string ResourceName
        {
            get;
            set;
        }

        public int ResourceNum
        {
            get;
            set;
        }

        public string FunctionUrl
        {
            get;
            set;
        }

        public int ResourceTotalNum
        {
            get;
            set;
        }
    }
}
