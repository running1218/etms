//==================================================================================================
//Version 1.0, auto-generated.
//Generated By liuyx.
//Date: 2012-3-27 11:48:25.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.ExOfflineHomework.API.Entity
{
    /// <summary>
    /// 离线作业表业务实体
    /// </summary>
    [Serializable]
    public partial class Res_e_OffLineJob : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "JobID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.JobID;
            }
            set
            {
                this.JobID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 离线作业ID
        /// </summary>
        public Guid JobID { get; set; }

        /// <summary>
        /// 作业名称
        /// </summary>
        public String JobName { get; set; }

        /// <summary>
        /// 作业描述
        /// </summary>
        public String JobDescription { get; set; }

        /// <summary>
        /// 作业附件
        /// </summary>
        public String JobFileName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

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

        /// <summary>
        /// 创建人
        /// </summary>
        public String TeacherID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String JobFileURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ModifyUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 JobFileSize { get; set; }

        #endregion Fields, Properties

    }
}
