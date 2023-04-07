using System;
namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    /// <summary>
    /// 班级学员表业务实体
    /// </summary>
    public partial class Sty_ClassStudent
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerNo { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public int RankID { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 组织结构
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 班级职务
        /// </summary>
        public string ClassPostion { get; set; }

        /// <summary>
        /// 学员职务ID
        /// </summary>
        public int StudentTypeID { get; set; }
        /// <summary>
        /// 学员照片
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 是否群组队长
        /// </summary>
        public bool IsLeader { get; set; }
        /// <summary>
        /// 班级群组
        /// </summary>
        public string ClassSubgroupName { get; set; }
        /// <summary>
        /// 班级群组学员ID
        /// </summary>
        public Guid SubgroupStudentID { get; set; }
    }
}
