using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试题查询结果
    /// </summary>
    [Serializable]
    public class QuestionSearchResult
    {
        private ExamResource resource;
        public QuestionSearchResult(){
           resource = new ExamResource();
        }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 试题类型(1单选题;2多选题;3填空;4判断;5问答;6匹配;7归类)
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// 适应对象ID(0空|全部；1幼儿教育；2初等教育；3中等教育；4中等职业教育；5高等教育；6高等职业教育；7继续教育；8职业培训)
        /// </summary>
        public short ObjectID { get; set; }

        /// <summary>
        /// 试题难度(0：Null，1：易，2：中，3：难)
        /// </summary>
        public short Difficulty { get; set; }

        /// <summary>
        /// 试题内容(题面)
        /// </summary>
        public string QuestionTitle { get; set; }

        /// <summary>
        /// 知识点
        /// </summary>
        public string KnowledgePoints { get; set; }

        /// <summary>
        /// 学科
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 试题大小
        /// </summary>
        public int QuestionSize { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public int CreatedUserID { get; set; }

        /// <summary>
        /// 拥有者用户ID
        /// </summary>
        public Guid OwnerID { get; set; }

        /// <summary>
        /// 题库类型(1我的题库,2机构题库,3共享题库)
        /// </summary>
        public short OwnerType { get; set; }

        /// <summary>
        /// 题库名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 所属人/机构
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 审核状态(0未知,1等待,99审核通过,98审核不通过)
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 分享状态(0未知,1未分享,2完全公开,3已分享好友)
        /// </summary>
        public int ShareStatus { get; set; }

        /// <summary>
        /// 相关的资源数据
        /// </summary>
        public ExamResource Resource 
        {
            get { return this.resource; }
            set { this.resource = value; }
        }
    }
}
