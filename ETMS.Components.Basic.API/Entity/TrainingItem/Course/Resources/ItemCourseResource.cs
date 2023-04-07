using System.Collections.Generic;

namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources
{
    public class ItemCourseResource
    {
        public List<ItemCourseResourceDetail> ItemCourseResourceInfo
        {
            get;
            set;
        }
    }

    public class ItemCourseResourceDetail
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

        public int ItemResourceNum
        {
            get;
            set;
        }

        public string FunctionUrl
        {
            get;
            set;
        }
    }





}
