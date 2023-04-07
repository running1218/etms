//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-02 11:00:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================


using ETMS.AppContext;
namespace ETMS.Components.Mentor.API.Entity.Mentor
{
    /// <summary>
    /// 导师表业务实体
    /// </summary>
    public partial class Site_Mentor:AbstractObject
	{
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string WorkerNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 员工部门
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 员工职级
        /// </summary>
        public int RankID { get; set; }
        /// <summary>
        /// 职级
        /// </summary>
        public string RankName { get; set; }
        /// <summary>
        /// 员工岗位
        /// </summary>
        public int PostID { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 员工职务
        /// </summary>
        public string TitleName { get; set; }
	}
}
