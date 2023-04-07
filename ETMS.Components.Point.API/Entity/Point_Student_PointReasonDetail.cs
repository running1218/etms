using System;

using ETMS.AppContext;
namespace ETMS.Components.Point.API.Entity
{
    /// <summary>
    /// 学员学习过程获得积分表业务实体
    /// </summary>
    public partial class Point_Student_PointReasonDetail:AbstractObject
	{
        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }
        /// <summary>
        /// 培训项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 培训项目开始时间
        /// </summary>
        public DateTime ItemBeginTime { get; set; }
        /// <summary>
        /// 培训项目结束时间
        /// </summary>
        public DateTime ItemEndTime { get; set; }
        /// <summary>
        /// 待发布项目的学员数
        /// </summary>
        public int StudentNum { get; set; }
        /// <summary>
        /// 待发布项目学员总发布积分
        /// </summary>
        public Int64 TotalPoints { get; set; }

        #region 学员信息
        #endregion
    }

    public partial class PointStudentReasonStudentInfo : Point_Student_PointReasonDetail
    {
        public string RealName { get; set; }
        public int OrganizationID { get; set; }
        public int DepartmentID { get; set; }
    }
}
