using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 题库
    /// </summary>
    [Serializable]
    public class QuestionBank
    {
        /// <summary>
        /// 题库ID
        /// </summary>
        public Guid QuestionBankID { get; set; }
        /// <summary>
        /// 题库名称(不允许包含"/"字符,不能为空)
        /// </summary>
        public string QuestionBankName { get; set; }
        /// <summary>
        /// 父级题库ID
        /// </summary>
        public Guid ParentQuestionBankID { get; set; }
        /// <summary>
        /// 所在的层级(最多3层)
        /// </summary>
        public short Depth { get; set; }
        /// <summary>
        /// 路径编码，如：0001/0004/0001
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 路径名称(用/分隔)
        /// </summary>
        public string PathName { get; set; }
        /// <summary>
        /// 子级的最大标识
        /// </summary>
        public short ChildrenMaxIdentity { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public short DisplayOrder { get; set; }
        /// <summary>
        /// 题库拥有者类型(1个人;2机构)
        /// </summary>
        public short OwnerType { get; set; }
        /// <summary>
        /// 题库拥有者ID（UserID或OrganizationID）
        /// </summary>
        public int OwnerID{get;set;}
        /// <summary>
        /// 课程ID（如果题库为非课程题库，可以为NULL）
        /// </summary>
        public Guid? CourseID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public int UpdatedUserID { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ownerID">题库拥有者ID（UserID或OrganizationID）</param>
        /// <param name="ownerType">题库拥有者类型(1个人;2机构)</param>
        /// <param name="parentID">父级题库ID</param>
        /// <param name="courseID">课程ID（如果题库为非课程题库，可以为NULL）</param>
        public QuestionBank(int ownerID, short ownerType, Guid parentID, Guid? courseID=null)
        {
            this.OwnerID = ownerID;
            this.OwnerType = ownerType;
            this.ParentQuestionBankID = Guid.Empty;
            this.CourseID = courseID; 
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public QuestionBank()
        {
            this.ParentQuestionBankID = Guid.Empty;
            this.CourseID = null;
        }
    }
}
