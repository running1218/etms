using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试题查询实体
    /// </summary>
    [Serializable]
    public class QuestionSearch
    {
        /// <summary>
        /// 试题来源(我的题库,机构题库)
        /// </summary>
        public EnumQuestionSource Source { get; set; }

        /// <summary>
        /// 题型(试题类型)
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// 难度(=0为全部,>0表示具体难度)
        /// </summary>
        public short Difficulty { get; set; }

        /// <summary>
        /// 题目(试题名称)
        /// </summary>
        public string QuestionTitle { get; set; }


        /// <summary>
        /// 关键字(知识点)
        /// </summary>
        public string KnowledgePoints { get; set; }


        /// <summary>
        /// 适用对象
        /// </summary>
        public short ObjectID { get; set; }

        /// <summary>
        /// 学科
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid QuestionBankID { get; set; }

        /// <summary>
        /// 用户名称(所属人或机构)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditType AuditStatus { get; set; }

        /// <summary>
        /// 分享状态
        /// </summary>
        public ShareType ShareStatus { get; set; }
    }
}
