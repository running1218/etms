using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Exam.API.Entity.NewTestPaper;
using ETMS.Components.Exam.Implement.DAL.NewTestPaper;
using System.Web.Script.Serialization;
using ETMS.Utility;
using System.Data;

namespace ETMS.Components.Exam.Implement.BLL.NewTestPaper
{

    public abstract class AExamHomework
    {

        public AExamHomeWorkData aExamHomeWorkData { get; set; }

        /// <summary>
        /// 查询试题列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Question> GetTestPaperQuestion(Guid testPaperID,Guid UserExamID)
        {
            IList<Question> questions = new List<Question>();
            questions = aExamHomeWorkData.GetTestPaperQuestion(testPaperID, UserExamID);
            //string cacheKey = "Q" + testPaperID;
            //object o = CacheHelper.Get(cacheKey);
            //if (o == null)
            //{
            //    questions = aExamHomeWorkData.GetTestPaperQuestion(testPaperID, UserExamID);
            //    CacheHelper.Add(cacheKey, questions, TimeSpan.FromDays(1));
            //}
            //else
            //{
            //    questions = (IList<Question>)o;
            //}
            return questions;
        }
        /// <summary>
        /// 查询试题列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Question> GetTestPaperQuestion(Guid testPaperID)
        {
            IList<Question> questions = new List<Question>();
            string cacheKey = "Q" + testPaperID;
            object o = CacheHelper.Get(cacheKey);
            if (o == null)
            {
                questions = aExamHomeWorkData.GetTestPaperQuestion(testPaperID);
                CacheHelper.Add(cacheKey, questions, TimeSpan.FromDays(1));
            }
            else
            {
                questions = (IList<Question>)o;
            }
            return questions;
        }
        /// <summary>
        /// 查询试题选项列表
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        public IList<Option> GetQuestionOption(Guid testPaperID)
        {
            IList<Option> options = new List<Option>();
            string cacheKey = "O" + testPaperID;
            object o = CacheHelper.Get(cacheKey);
            if (o == null)
            {
                options = aExamHomeWorkData.GetQuestionOption(testPaperID);
                CacheHelper.Add(cacheKey, options, TimeSpan.FromDays(1));
            }
            else
            {
                options = (IList<Option>)o;
            }
            return options;
        }

        /// <summary>
        /// 获取用户的考试答案
        /// </summary>
        /// <param name="userExamID"></param>
        /// <returns></returns>
        public IList<QuestionResult> GetQuestionResult(Guid userExamID)
        {
            return aExamHomeWorkData.GetQuestionResult(userExamID);
        }


        /// <summary>
        /// 保存试题分数
        /// </summary>
        /// <param name="userExamID"></param>
        /// <param name="testPaperID"></param>
        /// <param name="questionID"></param>
        /// <param name="userAnswer"></param>
        /// <param name="questionType"></param>
        public void SaveQuestionAnswer(Guid userExamID, Guid testPaperID, Guid questionID, List<UserAnswer> userAnswer, QuestionType questionType,int UserID)
        {
            JavaScriptSerializer MySerializer = new JavaScriptSerializer();
            string questionName = "";
            string strQuestionAnswer = "";
            string strUserAnswer = "";
            decimal score = 0;
            if (questionType == QuestionType.MultipleChoice)
            {
                score = pingMultipleScore(testPaperID, userAnswer, questionID, out questionName, out strQuestionAnswer, out strUserAnswer);
            }
            else
            {
                UserAnswer ua = new UserAnswer();
                if (userAnswer.Count > 0)
                {
                    ua = userAnswer.First();
                }
                score = pingSingleScore(testPaperID, ua, questionID, out questionName, out strQuestionAnswer, out strUserAnswer);
            }
            aExamHomeWorkData.SaveQuestionAnswer(userExamID, testPaperID, questionID, questionName, strQuestionAnswer, strUserAnswer, score, UserID);
        }

        /// <summary>
        /// 计算单选题，判断题的分数
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="userAnwser"></param>
        /// <param name="questionID"></param>
        /// <param name="questionName"></param>
        /// <param name="strQuestionAnswer"></param>
        /// <param name="strUserAnswer"></param>
        /// <returns></returns>
        private decimal pingSingleScore(Guid testPaperID, UserAnswer userAnwser, Guid questionID, out string questionName, out string strQuestionAnswer, out string strUserAnswer)
        {
            questionName = "";
            strQuestionAnswer = "";
            strUserAnswer = "";
            decimal score = 0;
            UserAnswer questionAnswer = new UserAnswer();
            JavaScriptSerializer MySerializer = new JavaScriptSerializer();
            Question question = GetTestPaperQuestion(testPaperID).Where(p => p.QuestionID == questionID).First();
            questionAnswer = MySerializer.Deserialize<UserAnswer>(question.QuestionAnswer);
            questionName = question.QuestionTitle;
            strQuestionAnswer = question.QuestionAnswer;
            if(!userAnwser.OptionID.IsEmpty())
            {
                strUserAnswer = MySerializer.Serialize(userAnwser);
            }
            if (userAnwser.OptionID == questionAnswer.OptionID)
            {
                score = question.QuestionScore;
            }
            return score;
        }

        /// <summary>
        /// 计算多选题的分数
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="userAnswers"></param>
        /// <param name="questionID"></param>
        /// <param name="questionName"></param>
        /// <param name="strQuestionAnswer"></param>
        /// <param name="strUserAnswer"></param>
        /// <returns></returns>
        private decimal pingMultipleScore(Guid testPaperID, List<UserAnswer> userAnswers, Guid questionID, out string questionName, out string strQuestionAnswer, out string strUserAnswer)
        {
            questionName = "";
            strQuestionAnswer = "";
            strUserAnswer = "";
            decimal score = 0;
            List<UserAnswer> questionAnswers = new List<UserAnswer>();
            JavaScriptSerializer MySerializer = new JavaScriptSerializer();
            Question question = GetTestPaperQuestion(testPaperID).Where(p => p.QuestionID == questionID).First();
            questionName = question.QuestionTitle;
            strQuestionAnswer = question.QuestionAnswer;
            strUserAnswer = MySerializer.Serialize(userAnswers);
            questionAnswers = MySerializer.Deserialize<List<UserAnswer>>(question.QuestionAnswer);
            int u = userAnswers.Count;
            int q = questionAnswers.Count;

            if (u != q)
            {
                score = 0;
            }
            else
            {
                var xx = from x in questionAnswers join y in userAnswers on x.OptionID equals y.OptionID select new { x.OptionID };
                if (xx.Count() == q)
                {
                    score = question.QuestionScore;
                }
            }

            return score;
        }

        /// <summary>
        /// 复制试卷试题信息
        /// </summary>
        /// <param name="testPaperID"></param>
        public void CopyTestPaperQuestionInfo(Guid testPaperID)
        {
            aExamHomeWorkData.CopyTestPaperQuestionInfo(testPaperID);
        }

        public abstract Paper GetExamTestPaper(Guid testPaperID, Guid onlineTestID);

        public abstract Guid StartNewTest(int studentID, Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID);

        public abstract void SubmitTestPaper(Guid userExamID, int status, int userID, Guid testPaperID);

        public abstract UserTestPaper GetUserTestPaper(Guid userExamID, Guid onLineTestID);

    }
}
