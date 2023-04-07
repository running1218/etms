//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-23 16:08:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem
{
    /// <summary>
    /// 培训项目表业务实体
    /// </summary>
    [Serializable]
    public partial class Tr_Item : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "TrainingItemID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.TrainingItemID;
            }
            set
            {
                this.TrainingItemID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }

        /// <summary>
        /// 专业类别编码
        /// </summary>
        public String SpecialtyTypeCode { get; set; }

        /// <summary>
        /// 是否来自培训计划
        /// </summary>
        public Boolean IsPlanItem { get; set; }

        /// <summary>
        /// 培训计划ID
        /// </summary>
        public Guid PlanID { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

        /// <summary>
        /// 培训级别
        /// </summary>
        public Int32 TrainingLevelID { get; set; }

        /// <summary>
        /// 组织部门ID
        /// </summary>
        public Int32 DutyDeptID { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public String ItemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public String ItemName { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public Int32 ItemStatus { get; set; }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime ItemBeginTime { get; set; }

        /// <summary>
        /// 项目结束时间
        /// </summary>
        public DateTime ItemEndTime { get; set; }

        /// <summary>
        /// 预算
        /// </summary>
        public Decimal BudgetFee { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public String DutyUser { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public String Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public String EMAIL { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public String ItemTarget { get; set; }

        /// <summary>
        /// 目标学员
        /// </summary>
        public String ItemObjectStudent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public String AuditUser { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public String AuditOpinion { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public Boolean IsIssue { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public String IssueUser { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 项目结束方式
        /// </summary>
        public Int32 ItemEndModeID { get; set; }

        /// <summary>
        /// 报名方式
        /// </summary>
        public Int32 SignupModeID { get; set; }

        /// <summary>
        /// 项目结束备注
        /// </summary>
        public String ItemEndReMark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Int32 IsUse { get; set; }

        /// <summary>
        /// 是否机构内项目
        /// </summary>
        public Boolean IsOrgItem { get; set; }

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

        /// <summary>
        /// 删除标识
        /// </summary>
        public Boolean DelFlag { get; set; }

        /// <summary>
        /// 报名开始时间
        /// </summary>
        public DateTime SignupBeginTime { get; set; }

        /// <summary>
        /// 报名结束时间
        /// </summary>
        public DateTime SignupEndTime { get; set; }

        /// <summary>
        /// 是否允许学生报名
        /// </summary>
        public Boolean IsAllowSignup { get; set; }

        /// <summary>
        /// 是否发布积分
        /// </summary>
        public Boolean IsIssuePoint { get; set; }

        /// <summary>
        /// 积分发布人
        /// </summary>
        public String PointIssueUser { get; set; }

        /// <summary>
        /// 积分发布时间
        /// </summary>
        public DateTime PointIssueTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayFrom { get; set; }
        
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailURL { get; set; }
        
        #endregion Fields, Properties

    }
}
