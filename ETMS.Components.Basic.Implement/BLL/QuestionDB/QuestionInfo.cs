using System;
using System.Data;

using ETMS.Components.Basic.Implement.DAL.QuestionDB;
using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Basic.Implement.BLL.QuestionDB
{
    public partial class QuestionInfo
    {
        private static readonly QuestionDataAccess DAL = new QuestionDataAccess();
		

        /// <summary>
        /// 获取题型名称
        /// </summary>
        /// <param name="QuestionType"></param>
        /// <returns></returns>
        public string QuestionTypeName(QuestionType questionType)
        {
            string returnValue = "";
            switch (questionType)
            {
                case QuestionType.SingleChoice:
                    returnValue = "单选题";
                    break;
                case QuestionType.MultipleChoice:
                    returnValue = "多选题";
                    break;
                case QuestionType.Judgement:
                    returnValue = "判断题";
                    break;
                case QuestionType.TextEntry:
                    returnValue = "填空题";
                    break;
                case QuestionType.ExtendedText:
                    returnValue = "简答题";
                    break;
                default:
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// 获取作业类型名称
        /// </summary>
        /// <param name="ExerciseType"></param>
        /// <returns></returns>
        public string ExerciseName(BizExerciseType ExerciseType)
        {
            string returnValue = "";
            switch (ExerciseType)
            {
                case BizExerciseType.ExContest:
                    returnValue = "闯关竞赛";
                    break;
                case BizExerciseType.ExOfflineHomework:
                    returnValue = "离线作业";
                    break;
                case BizExerciseType.ExOnlineHomework:
                    returnValue = "在线作业";
                    break;
                case BizExerciseType.ExOnlinePractice:
                    returnValue = "在线练习";
                    break;
                case BizExerciseType.ExOnlineTest:
                    returnValue = "在线测试";
                    break;
                case BizExerciseType.ExRandomPractice:
                    returnValue = "抽题练习";
                    break;
                default:
                    break;
            }

            return returnValue;

        }

        /// <summary>
        /// 调查问卷类型
        /// </summary>
        /// <param name="QuestionnaireType"></param>
        /// <returns></returns>
        public string QuestionnaireName(BizQuestionnaireType QuestionnaireType)
        {
            string returnValue = "";
            switch (QuestionnaireType)
            {
                case BizQuestionnaireType.Questionnaire:
                    returnValue = "问卷调查";
                    break;
                case BizQuestionnaireType.Satisfaction:
                    returnValue = "满意度调查";
                    break;
                case BizQuestionnaireType.Demand:
                    returnValue = "培训需求调查";
                    break;
                default:
                    break;
            }

            return returnValue;

        }

        /// <summary>
        /// 获取课程下题库的题目列表，分题目类型。
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <param name="QuestionTypeID">题型</param>
        /// <returns></returns>
        public DataTable getQuestionBankQuestionListByCourseIDAndQuestionType(Guid CourseID, int QuestionTypeID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND a.CourseID='{0}' ", CourseID);

            return DAL.getQuestionBankQuestionListByCourseIDAndQuestionType(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }  


    }
}
