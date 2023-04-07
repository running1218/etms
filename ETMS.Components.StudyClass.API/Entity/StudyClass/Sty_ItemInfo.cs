using System;
using System.Collections.Generic;

namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    public partial class Sty_ItemInfo
    {
        public Guid TrainingItemID
        {
            get;
            set;
        }
        public string ItemCode
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public DateTime ItemBeginTime
        {
            get;
            set;
        }
        public DateTime ItemEndTime
        {
            get;
            set;
        }
        public int SingnNum
        {
            get;
            set;
        }
        public int ClassNum
        {
            get;
            set;
        }
        public List<Sty_Class> StyClass
        {
            get;
            set;
        }
        public Guid ClassID { get; set; }
        public string ClassName { get; set; }
    }
}
