using System;


using ETMS.Components.Exam.API.Entity.Test;
namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 树形分类
    /// </summary>
    [Serializable]
    public class TreeCategory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ownerID">拥有者ID</param>
        /// <param name="ownerType">拥有者类型</param>
        /// <param name="parentID">父级分类ID</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="cType">分类的类型(题库或试卷等)</param>
        public TreeCategory(int ownerID, OwnerType ownerType,
            Guid parentID, String categoryName, TreeCategoryType cType)
        {
            this.OwnerID = ownerID;
            this.OwnerType = ownerType;
            this.ParentID = parentID;
            this.CategoryName = categoryName;
            this.CategoryType = cType;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeCategory()
        {
            this.CategoryType = TreeCategoryType.TestPaper;
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// 分类名称(不允许包含"/"字符)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 父级分类ID
        /// </summary>
        public Guid ParentID { get; set; }

        /// <summary>
        /// 当前层级(最多3层)
        /// </summary>
        public short Depth { get; set; }

        /// <summary>
        /// 分类编码路径
        /// </summary>
        public string PathID { get; set; }

        /// <summary>
        /// 分类名称路径
        /// </summary>
        public string PathName { get; set; }

        /// <summary>
        /// 拥有子节点个数
        /// </summary>
        public short ChildrenCount { get; set; }

        /// <summary>
        /// 显示排列次序
        /// </summary>
        public short DisplayOrder { get; set; }

        /// <summary>
        /// 题库拥有者类型(1个人;2机构)
        /// </summary>
        public OwnerType OwnerType { get; set; }

        /// <summary>
        /// 题库拥有者ID(UserID或OrganizationID)
        /// </summary>
        public int OwnerID { get; set; }

        /// <summary>
        /// 分类的类型(题库或试卷等)
        /// </summary>
        public TreeCategoryType CategoryType { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// 分类类型
    /// </summary>
    public enum TreeCategoryType
    {
        /// <summary>
        /// 试题分类
        /// </summary>
        Question = 1,
        /// <summary>
        /// 试卷分类
        /// </summary>
        TestPaper = 2
    }
}
