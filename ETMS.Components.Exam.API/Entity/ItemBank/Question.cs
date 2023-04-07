using System;
using System.Collections.Generic;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试题
    /// </summary>
    [Serializable]
    public class Question
    {
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// 试题类型(0全部;1单选题;2多选题;3填空;4判断;5问答;6匹配;7归类)
        /// 通过Autumn.Business.LMS.ServiceRepository.DictionaryService.GetAllItems(EnumBizDictionary.dic_QuestionType)取得字典数据
        /// </summary>
        public QuestionType QuestionType { get; set; }
        /// <summary>
        /// 试题内容(题面)
        /// </summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// 父级试题ID(没有父级试题则为NULL)
        /// </summary>
        public Guid? ParentQuestionID { get; set; }
        /// <summary>
        /// 适应对象ID(0空|全部；1幼儿教育；2初等教育；3中等教育；4中等职业教育；5高等教育；6高等职业教育；7继续教育；8职业培训)
        /// 通过Autumn.Business.LMS.ServiceRepository.DictionaryService.GetAllItems(EnumBizDictionary.dic_Object)取得字典数据
        /// </summary>
        public short ObjectID { get; set; }
        /// <summary>
        /// 题库ID
        /// </summary>
        public Guid QuestionBankID { get; set; }
        /// <summary>
        /// 所属学科
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 知识点
        /// </summary>
        public string KnowledgePoints { get; set; }
        /// <summary>
        /// 试题难度(0：Null，1：易，2：中，3：难)
        /// </summary>
        public short Difficulty { get; set; }
        /// <summary>
        /// 正确答案
        /// </summary>
        public string Answers { get; set; }
        /// <summary>
        /// 选项是否随机显示(false:否;true:是)
        /// </summary>
        public bool RandomFlag { get; set; }
        /// <summary>
        /// 子题序号(没有子题则为NULL)
        /// </summary>
        public int? SubItemIndex { get; set; }
        /// <summary>
        /// 试题来源ID(自建的试题来源ID为NULL)
        /// </summary>
        public Guid? SourceQuestionID { get; set; }
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
        /// 最后修改日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// 试题分类名称
        /// </summary>
        public string QuestionBankName { get; set; }
        /// <summary>
        /// 试题大小(单位 K)
        /// </summary>
        public int QuestionSize { get; set; }

        /// <summary>
        /// 状态(0:空；1：等待审核；2：审核通过；3：审核不通过)
        /// </summary>
        public AuditType AuditStatus { get; set; }

        /// <summary>
        /// 状态(0:空；1:未分享,2:完全公开,3:已分享好友)
        /// </summary>
        public ShareType ShareStatus{ get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="questionType">试题类型(0全部;1单选题;2多选题;3填空;4判断;5问答;6匹配;7归类)</param>
        public Question(QuestionType questionType)
        {
            this.QuestionType = questionType;
            this.ParentQuestionID = null;
            this.SubItemIndex = null;
            this.RandomFlag = false;
            this.SourceQuestionID = null;
            this.AuditStatus = AuditType.Waiting;
            this.ShareStatus = ShareType.NotShare;
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Question()
        {
            this.ParentQuestionID = null;
            this.SubItemIndex = null;
            this.RandomFlag = false;
            this.SourceQuestionID = null;
            this.AuditStatus = AuditType.Waiting;
            this.ShareStatus = ShareType.NotShare;
        }
        #region --试题的其它关联属性--
        /////<summary>
        ///// 试题反馈
        /////</summary>
        //private QuestionFeedback QuestionFeedback { get; set; }

        ///// <summary>
        ///// 试题答案
        ///// </summary>
        //public AnswerBase AnswerBase
        //{
        //    get;
        //    set;
        //}
        #endregion
    }

    /// <summary>
    /// 通用试题实体（包含了题面、解题思路、答题反馈、选项反馈）
    /// </summary>
    [Serializable]
    public class CommonQuestion
    {
        /// <summary>
        /// 试题类型
        /// </summary>
        public QuestionType QuestionType { get; set; }
        /// <summary>
        /// 试题题面信息
        /// </summary>
        public Question Question { get; set; }
        /// <summary>
        /// 试题答题反馈
        /// </summary>
        public QuestionFeedback QuestionFeedback { get; set; }
        /// <summary>
        /// 试题选项反馈
        /// </summary>
        public IList<OptionFeedback> LstOptionFeedbacks { get; set; }
        /// <summary>
        /// 试题解题思路
        /// </summary>
        public QuestionExtend QuestionExtend { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonQuestion()
        {
            this.QuestionType = QuestionType.Null;
            this.Question = new Question();
            this.QuestionFeedback = new QuestionFeedback();
            this.LstOptionFeedbacks = new List<OptionFeedback>();
            this.QuestionExtend = new QuestionExtend();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public CommonQuestion(QuestionType type)
        {
            this.QuestionType = type;
            this.Question = new Question();
            this.QuestionFeedback = new QuestionFeedback();
            this.LstOptionFeedbacks = new List<OptionFeedback>();
            this.QuestionExtend = new QuestionExtend();
        }
    }
    /// <summary>
    /// 试题来源
    /// 0空|全部（查询时）,1我的题库，2机构题库，3共享题库
    /// </summary>
    public enum EnumQuestionSource
    {
        /// <summary>
        /// 空|全部（查询时）
        /// </summary>
        Null = 0,
        /// <summary>
        /// 我的题库
        /// </summary>
        MyQuestionLibrary = 1,
        /// <summary>
        /// 机构题库
        /// </summary>
        OrgQuestionLibrary = 2,
        /// <summary>
        /// 共享题库
        /// </summary>
        ShareQuestionLibrary = 3,
    }
    /// <summary>
    /// 试题难度(0：Null，1：易，2：中，3：难)
    /// </summary>
    public enum EnumDifficulty
    {
        Null = 0,
        Easy = 1,
        Middle = 2,
        Difficult = 3
    }
}
