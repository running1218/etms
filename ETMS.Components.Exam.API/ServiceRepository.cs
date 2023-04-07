using Autumn.Context.Support;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.API
{
    /// <summary>
    /// 服务仓库
    /// 每个API服务提供一个单例，供外部应用调用
    /// </summary>
    public abstract class ServiceRepository
    {
        /// <summary>
        /// 应用上下文名称
        /// </summary>
        public static string AppContextName = "Exam";

        #region ItemBank 题库服务

        /// <summary>
        /// 分类服务
        /// </summary>
        public static ITreeCategoryLogic TreeCategoryService
        {
            get
            {
                return (ITreeCategoryLogic)ContextRegistry.GetContext(AppContextName).GetObject("TreeCategoryService");
            }
        }

        /// <summary>
        /// 试题解题思路接口
        /// </summary>
        public static IQuestionExtendLogic QuestionExtendService
        {
            get
            {
                return (IQuestionExtendLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionExtendService");
            }
        }

        /// <summary>
        /// 试题反馈接口
        /// </summary>
        public static IQuestionFeedbackLogic QuestionFeedbackService
        {
            get
            {
                return (IQuestionFeedbackLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionFeedbackService");
            }
        }
        public static IOptionFeedbackLogic OptionFeedbackService
        {
            get
            {
                return (IOptionFeedbackLogic)ContextRegistry.GetContext(AppContextName).GetObject("OptionFeedbackService");
            }
        }

        /// <summary>
        /// 试题选项相关的接口
        /// </summary>
        public static IQuestionOptionLogic QuestionOptionService
        {
            get
            {
                return (IQuestionOptionLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionOptionService");
            }
        }
        /// <summary>
        /// 试题选项服务相关的接口
        /// </summary>
        public static IOptionServiceLogic OptionService
        {
            get
            {
                return (IOptionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("OptionService");
            }
        }

        /// <summary>
        /// 试题选项组逻辑接口
        /// </summary>
        public static IOptionGroupLogic OptionGroupLogic
        {
            get
            {
                return (IOptionGroupLogic)ContextRegistry.GetContext(AppContextName).GetObject("OptionGroupLogic");
            }
        }
        /// <summary>
        /// 试题选项组服务逻辑接口
        /// </summary>
        public static IOptionGroupServiceLogic OptionGroupService
        {
            get
            {
                return (IOptionGroupServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("OptionGroupService");
            }
        }
        /// <summary>
        /// 试题服务
        /// </summary>
        public static IQuestionLogic QuestionService
        {
            get
            {
                return (IQuestionLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionService");
            }
        }
        /// <summary>
        /// 题库服务
        /// </summary>
        public static IQuestionBankLogic QuestionBankService
        {
            get
            {
                return (IQuestionBankLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionBankService");
            }
        }
        #region --各种基本题型的服务接口--
        /// <summary>
        /// 单选题服务
        /// </summary>
        public static IQuestionServiceLogic SingleChoiceQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("SingleChoiceQuestionService");
            }
        }
        /// <summary>
        /// 多选题服务
        /// </summary>
        public static IQuestionServiceLogic MultipleChoiceQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("MultipleChoiceQuestionService");
            }
        }
        /// <summary>
        /// 判断题服务
        /// </summary>
        public static IQuestionServiceLogic JudgementQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("JudgementQuestionService");
            }
        }
        /// <summary>
        /// 匹配题服务
        /// </summary>
        public static IQuestionServiceLogic MatchQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("MatchQuestionService");
            }
        }

        /// <summary>
        /// 归类题服务
        /// </summary>
        public static IQuestionServiceLogic GroupQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("GroupQuestionService");
            }
        }

        /// <summary>
        /// 添空题服务
        /// </summary>
        public static IQuestionServiceLogic TextEntryQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("TextEntryQuestionService");
            }
        }
        /// <summary>
        /// 问答题服务
        /// </summary>
        public static IQuestionServiceLogic ExtendTextQuestionService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("ExtendTextQuestionService");
            }
        }
        /// <summary>
        /// 试题搜索服务
        /// </summary>
        public static IQuestionSearchLogic QuestionSearchService
        {
            get
            {
                return (IQuestionSearchLogic)ContextRegistry.GetContext(AppContextName).GetObject("QuestionSearchService");
            }
        }
        /// <summary>
        /// 课程和试题引用
        /// </summary>
        public static ITestToQuestionLogic TestToQuestionService
        {
            get
            {
                return (ITestToQuestionLogic)ContextRegistry.GetContext(AppContextName).GetObject("TestToQuestionService");
            }
        }
        #endregion
        #region --面向所有试题类型的试题服务--
        /// <summary>
        /// 面向所有试题类型的试题服务，前端直接使用该类即可，不必根据类型使用具体的题型服务类。
        /// </summary>
        public static IQuestionServiceLogic AllQuestionTypeService
        {
            get
            {
                return (IQuestionServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("AllQuestionTypeService");
            }
        }
        #endregion
        #endregion

        #region Test 考试服务
        /// <summary>
        /// 试卷服务
        /// </summary>
        public static ITestPaperLogic TestPaperService
        {
            get
            {
                return (ITestPaperLogic)ContextRegistry.GetContext(AppContextName).GetObject("TestPaperService");
            }
        }
        /// <summary>
        /// 试卷试题关系服务
        /// </summary>
        public static IPaperQuestionLogic PaperQuestionService
        {
            get
            {
                return (IPaperQuestionLogic)ContextRegistry.GetContext(AppContextName).GetObject("PaperQuestionService");
            }
        }
        /// <summary>
        /// 试卷答题反馈选项组服务逻辑接口
        /// </summary>
        public static ITestFeedbackServiceLogic TestFeedbackService
        {
            get
            {
                return (ITestFeedbackServiceLogic)ContextRegistry.GetContext(AppContextName).GetObject("TestFeedbackService");
            }
        }
        /// <summary>
        /// 考生考试服务逻辑接口
        /// </summary>
        public static IUserTestLogic UserTestService
        {
            get
            {
                return (IUserTestLogic)ContextRegistry.GetContext(AppContextName).GetObject("UserTestLogic");
            }
        }
        /// <summary>
        /// 试卷策略服务
        /// </summary>
        public static ITestPaperRuleLogic TestPaperRuleService
        {
            get
            {
                return (ITestPaperRuleLogic)ContextRegistry.GetContext(AppContextName).GetObject("TestPaperRuleService");
            }
        }

        /// <summary>
        /// 试题试卷和素材资源关系服务
        /// </summary>
        public static IExamResourceLogic ExamResourceService
        {
            get
            {
                return (IExamResourceLogic)ContextRegistry.GetContext(AppContextName).GetObject("ExamResourceService");
            }
        }
        #endregion
    }
}
