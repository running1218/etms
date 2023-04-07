//==================================================================================================
//Version 1.0, auto-generated.
//Generated By liuyx.
//Date: 2012-3-27 16:24:20.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.ExOfflineHomework.API.Entity
{
    /// <summary>
    /// 课程离线作业表业务实体
    /// </summary>
    [Serializable]
    public partial class Res_ItemCourseOffLineJob : AbstractObject
	{
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "ItemCourseOffLineJobID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.ItemCourseOffLineJobID;
            }
            set
            {
                this.ItemCourseOffLineJobID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 课程离线作业ID
        /// </summary>
        public Guid ItemCourseOffLineJobID { get; set; }

        /// <summary>
        /// 离线作业ID
        /// </summary>
        public Guid JobID { get; set; }

        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }

        public string TrainingItemName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion Fields, Properties

    }
}
