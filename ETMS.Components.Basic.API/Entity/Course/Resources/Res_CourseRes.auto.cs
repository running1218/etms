
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Course.Resources
{
    /// <summary>
    /// 课程资源表业务实体
    /// </summary>
    [Serializable]
    public partial class Res_CourseRes : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "CourseResID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.CourseResID;
            }
            set
            {
                this.CourseResID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 课程资源ID
        /// </summary>
        public Guid CourseResID { get; set; }

        /// <summary>
        /// 课程资源类型
        /// </summary>
        public Int32 CourseResTypeID { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public String ResName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// 资源开始时间
        /// </summary>
        public DateTime ResBeginTime { get; set; }

        /// <summary>
        /// 资源结束时间
        /// </summary>
        public DateTime ResEndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public String ResID { get; set; }

        #endregion Fields, Properties

    }
}
