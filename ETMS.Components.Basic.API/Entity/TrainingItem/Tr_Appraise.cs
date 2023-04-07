using System;
using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem
{
    public partial class Tr_Appraise : AbstractObject
    {
        public override string DefaultKeyName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object KeyValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Guid? TrainingItemID { get; set; }
        public bool IsCheckCourse { get; set; }
        public int? CourseRate { get; set; }
        public bool IsCheckStudying { get; set; }
        public int? StudyingRate { get; set; }
        public bool IsCheckActual { get; set; }
        public int? ActualRate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String ModifyUser { get; set; }
    }
}
