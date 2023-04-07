using System;
using ETMS.AppContext;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 课程开放机构实体类
    /// </summary>
    [Serializable]
    public class Res_CourseOpenRange : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "OpenRangeID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.OpenRangeID;
            }
            set
            {
                this.OpenRangeID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 开放机构ID
        /// </summary>
        public Guid OpenRangeID { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }
        
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Int32 CreateUserID { get; set; }

        #endregion Fields, Properties
    }
}
