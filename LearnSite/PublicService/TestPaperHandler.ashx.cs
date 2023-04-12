using University.Mooc.AppContext;
using ETMS.Components.Exam.API.Entity.NewTestPaper;
using ETMS.Components.Exam.Implement.BLL.NewTestPaper;
using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;
using ETMS.Components.ExOnlineTest.API.Entity.ExOnlineTest;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.ExOnlineTest.Implement.BLL.ExOnlineTest;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for TestPaperHandler
    /// </summary>
    public class TestPaperHandler : IHttpHandler
    {
        private HttpContext currentContext = null;

        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "getpaper":
                    ReturnResponseContent(GetStudentPaperData());
                    break;
                case "getstudentpaper":
                    ReturnResponseContent(GetStudentPaperResult());
                    break;
                case "submitpaper":
                    ReturnResponseContent(SubmitStudentPaper());
                    break;
                case "savepaper":
                    ReturnResponseContent(SaveStudentPaper());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }
        /// <summary>
        /// 获取学生答卷信息
        /// </summary>
        /// <returns></returns>
        private string GetStudentPaperData()
        {
            //获取试卷信息
            Guid TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();//培训项目课程ID
            Guid TestPaperID = currentContext.Request["TestPaperID"].ToGuid();//试卷ID
            Guid OnlineTestID = currentContext.Request["OnlineTestID"].ToGuid();//测评【考试ID或者作业ID】 
            Guid StudentCourseID = currentContext.Request["StudentCourseID"].ToGuid();//学生选课ID 
            int TestType = currentContext.Request["TestType"].ToInt();//试卷测试类型：2-在线作业；5-在线测试

            AExamHomework examLogic = ExamHomeworkFactory.Create("ExamLogic");
            Ex_StudentOnlineTestLogic studentOnlineTestLogic = new Ex_StudentOnlineTestLogic();
            Ex_StudentOnlineTest studentOnlineTest = new Ex_StudentOnlineTest();
            IList<Question> questions;
            IList<Option> questionOptions;

            Paper PaperData = new Paper();
            DataTable dtPaper = new DataTable();
            int StudentID = UserContext.Current.UserID;

            try
            {
                if (TestType == 2)//2在线作业
                {
                    dtPaper = new Ex_OnLineJobLogic().GetOnlineJobExamTestPaper(TrainingItemCourseID, TestPaperID);
                    if (dtPaper != null && dtPaper.Rows.Count > 0)
                    {
                        PaperData.OnLineTestID = dtPaper.Rows[0]["OnLineJobID"].ToGuid();
                    }
                    AExamHomework HomeworkLogic = ExamHomeworkFactory.Create("HomeworkLogic");
                    Ex_StudentOnlineJobLogic studentOnlineJobLogic = new Ex_StudentOnlineJobLogic();
                    Guid strUserExamID = studentOnlineJobLogic.getUserExamID(OnlineTestID, TrainingItemCourseID, StudentID);
                    if (strUserExamID == Guid.Empty)
                    {
                        PaperData.UserExamID = HomeworkLogic.StartNewTest(StudentID, TestPaperID, TrainingItemCourseID, OnlineTestID, TrainingItemCourseID);
                    }
                    else
                    {
                        PaperData.UserExamID = strUserExamID.ToGuid();
                    }
                    //获取试卷的试题
                    questions = HomeworkLogic.GetTestPaperQuestion(TestPaperID, PaperData.UserExamID);
                    questionOptions = HomeworkLogic.GetQuestionOption(TestPaperID);
                }
                else//5在线测试
                {
                    dtPaper = new Ex_OnLineTestLogic().GetOnlineTestExamTestPaper(TrainingItemCourseID, TestPaperID, StudentID);
                    if (dtPaper != null && dtPaper.Rows.Count > 0)
                    {
                        PaperData.OnLineTestID = dtPaper.Rows[0]["OnLineTestID"].ToGuid();
                        PaperData.LimitTime = dtPaper.Rows[0]["LimitTime"].ToInt();
                        PaperData.UserTestCount = dtPaper.Rows[0]["UserTestCount"].ToInt();
                        PaperData.TestCount = string.IsNullOrEmpty(dtPaper.Rows[0]["TestCount"].ToString())? WebUtility.OrgMaxExamNum : dtPaper.Rows[0]["TestCount"].ToInt();
                    }
                    //判断学生是否提交试卷，并获取试卷考试ID
                    studentOnlineTest = studentOnlineTestLogic.GetByStudentOnlineTestSubmit(OnlineTestID, StudentID, TrainingItemCourseID);
                    if (studentOnlineTest != null)
                    {
                        //补充以前的答卷
                        PaperData.UserExamID = studentOnlineTest.UserExamID;
                        PaperData.LimitTime = PaperData.LimitTime * 60 - (int)TDiff(studentOnlineTest.BeginTime, System.DateTime.Now);
                    }
                    else
                    {
                        //学生进入多次考试
                        PaperData.UserExamID = examLogic.StartNewTest(StudentID, TestPaperID, TrainingItemCourseID, OnlineTestID, StudentCourseID);
                        PaperData.LimitTime = PaperData.LimitTime * 60;
                    }

                    //获取试卷的试题
                    questions = examLogic.GetTestPaperQuestion(TestPaperID, PaperData.UserExamID);
                    questionOptions = examLogic.GetQuestionOption(TestPaperID);
                }
                if (dtPaper != null && dtPaper.Rows.Count > 0)
                {
                    PaperData.TestPaperID = dtPaper.Rows[0]["TestPaperID"].ToGuid();
                    PaperData.TestPaperName = dtPaper.Rows[0]["TestPaperName"].ToString();
                    PaperData.TotalScore = dtPaper.Rows[0]["TotalScore"].ToString().ToDecimal();
                    PaperData.BeginTime = dtPaper.Rows[0]["BeginTime"].ToString();
                    PaperData.EndTime = dtPaper.Rows[0]["EndTime"].ToString();
                    PaperData.TestType = TestType;
                }
                foreach (Question eQuestion in questions)
                {
                    List<Option> options = questionOptions.Where(p => p.QuestionID == eQuestion.QuestionID).OrderBy(s=>s.OptionCode).ToList<Option>();
                    eQuestion.QuestionOption = options;
                }
                PaperData.QuestionCount = questions.Count;
                PaperData.PaperQuestion = questions;
                return JsonHelper.GetInvokeSuccessJson(PaperData);
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "数据加载失败，请联系管理员！");
            }

        }
        private string GetStudentPaperResult()
        {
            //获取试卷信息
            Guid TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();//培训项目课程ID
            Guid TestPaperID = currentContext.Request["TestPaperID"].ToGuid();//试卷ID
            Guid UserExamID = currentContext.Request["UserExamID"].ToGuid();//用户考试ID
            int TestType = currentContext.Request["TestType"].ToInt();//试卷测试类型：2-在线作业；5-在线测试
        
            IList<Question> questions;
            IList<Option> questionOptions;

            Paper PaperData = new Paper();
            DataTable dtPaper = new DataTable();
            int StudentID = UserContext.Current.UserID;
            PaperData.UserExamID = UserExamID;
            PaperData.TestPaperID = TestPaperID;
            try
            {
                if (TestType == 2)//2在线作业
                {
                    dtPaper = new Ex_OnLineJobLogic().GetOnlineJobExamTestPaper(TrainingItemCourseID, PaperData.TestPaperID);
                    if (dtPaper != null && dtPaper.Rows.Count > 0)
                    {
                        PaperData.OnLineTestID = dtPaper.Rows[0]["OnLineJobID"].ToGuid();
                    }

                    AExamHomework HomeworkLogic = ExamHomeworkFactory.Create("HomeworkLogic");
                    Ex_StudentOnlineJobLogic studentOnlineJobLogic = new Ex_StudentOnlineJobLogic();
                    if (!PaperData.UserExamID.IsEmpty())
                    {
                        PaperData.UserScore = HomeworkLogic.GetUserTestPaper(PaperData.UserExamID, PaperData.OnLineTestID).ExamScore;
                    }
                    //获取试卷的试题
                    questions = HomeworkLogic.GetTestPaperQuestion(PaperData.TestPaperID, PaperData.UserExamID);
                    questionOptions = HomeworkLogic.GetQuestionOption(PaperData.TestPaperID);
                }
                else//5在线测试
                {
                   
                    dtPaper = new Ex_OnLineTestLogic().GetOnlineTestExamTestPaper(TrainingItemCourseID, PaperData.TestPaperID, StudentID);
                    if (dtPaper != null && dtPaper.Rows.Count > 0)
                    {
                        PaperData.OnLineTestID = dtPaper.Rows[0]["OnLineTestID"].ToGuid();
                        PaperData.LimitTime = dtPaper.Rows[0]["LimitTime"].ToInt();
                        PaperData.UserTestCount = dtPaper.Rows[0]["UserTestCount"].ToInt();
                        PaperData.TestCount = dtPaper.Rows[0]["TestCount"].ToInt();
                    }

                    AExamHomework examLogic = ExamHomeworkFactory.Create("ExamLogic");
                    if(!PaperData.UserExamID.IsEmpty())
                    {
                        PaperData.UserScore = examLogic.GetUserTestPaper(PaperData.UserExamID, PaperData.OnLineTestID).ExamScore;
                    }
                    //获取试卷的试题
                    questions = examLogic.GetTestPaperQuestion(PaperData.TestPaperID, PaperData.UserExamID);
                    questionOptions = examLogic.GetQuestionOption(PaperData.TestPaperID);
                }
                if (dtPaper != null && dtPaper.Rows.Count > 0)
                {
                    PaperData.TestPaperID = dtPaper.Rows[0]["TestPaperID"].ToGuid();
                    PaperData.TestPaperName = dtPaper.Rows[0]["TestPaperName"].ToString();
                    PaperData.TotalScore = dtPaper.Rows[0]["TotalScore"].ToString().ToDecimal();
                    PaperData.BeginTime = dtPaper.Rows[0]["BeginTime"].ToString();
                    PaperData.EndTime = dtPaper.Rows[0]["EndTime"].ToString();
                    PaperData.TestType = TestType;
                }
                foreach (Question eQuestion in questions)
                {
                    List<Option> options = questionOptions.Where(p => p.QuestionID == eQuestion.QuestionID).OrderBy(s=>s.OptionCode).ToList<Option>();
                    eQuestion.QuestionOption = options;
                }
                PaperData.QuestionCount = questions.Count;
                PaperData.PaperQuestion = questions;
                return JsonHelper.GetInvokeSuccessJson(PaperData);
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "数据加载失败，请联系管理员！");
            }
        }
        /// <summary>
        /// 提交学生试卷
        /// </summary>
        /// <returns></returns>
        private string SubmitStudentPaper()
        {
            decimal ExamScore = 0.00M;
            try
            {
                var jsonPaperData = currentContext.Request["PaperData"];
                Paper PaperData = JsonHelper.DeserializeObject<Paper>(jsonPaperData);
                int StudentID = UserContext.Current.UserID;
                //保存
                SavePaper(PaperData, StudentID);
              

                //提交
                if(PaperData.TestType==5)//在线测试
                {
                    AExamHomework examLogic = ExamHomeworkFactory.Create("ExamLogic");
                    examLogic.SubmitTestPaper(PaperData.UserExamID, 2, StudentID, PaperData.TestPaperID);
                    ExamScore = examLogic.GetUserTestPaper(PaperData.UserExamID, PaperData.OnLineTestID).ExamScore;
                }
                else
                {
                    AExamHomework HomeworkLogic = ExamHomeworkFactory.Create("HomeworkLogic");
                    HomeworkLogic.SubmitTestPaper(PaperData.UserExamID, 2, StudentID, PaperData.TestPaperID);
                    ExamScore = HomeworkLogic.GetUserTestPaper(PaperData.UserExamID, PaperData.OnLineTestID).ExamScore;
                }
            }
            catch(Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "试卷提交失败，请联系管理员！");
            }
            //返回试卷总得分
            return JsonHelper.GetInvokeSuccessJson(ExamScore);

        }
        /// <summary>
        /// 保存学生试卷
        /// </summary>
        /// <returns></returns>
        private string SaveStudentPaper()
        {
            try
            {
                var jsonPaperData = currentContext.Request["PaperData"];
                Paper PaperData = JsonHelper.DeserializeObject<Paper>(jsonPaperData);
                int StudentID = UserContext.Current.UserID;
                //保存
                SavePaper(PaperData, StudentID);
            }
            catch (Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "试卷保存失败，请联系管理员！");
            }
            return JsonHelper.GetInvokeSuccessJson();
        }
        /// <summary>
        /// 保存试卷
        /// </summary>
        /// <param name="PaperData"></param>
        private void SavePaper(Paper PaperData,int StudentID)
        {
            Guid userExamID = PaperData.UserExamID;
            Guid testPaperID = PaperData.TestPaperID;
            foreach (Question question in PaperData.PaperQuestion)
            {
                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), question.QuestionType.ToString());
                Guid questionID = question.QuestionID;
                List<UserAnswer> userAnswers = new List<UserAnswer>();
                if (!string.IsNullOrEmpty(question.UserAnswer))
                {
                    UserAnswer userAnswer;
                    //选项分单选题、多选题、判断题
                    switch (questionType)
                    {
                        case QuestionType.Null:
                            break;
                        case QuestionType.SingleChoice:
                        case QuestionType.Judgement:
                            var x = question.QuestionOption.Where(p => p.OptionID == question.UserAnswer.ToGuid() && p.QuestionID == questionID).First();
                            userAnswer = new UserAnswer();
                            userAnswer.OptionID = x.OptionID;
                            userAnswer.OptionCode = x.OptionCode;
                            userAnswers.Add(userAnswer);
                            break;
                        case QuestionType.MultipleChoice:
                            //答案选项（多选有好几个，以逗号分隔）
                            var userOptions = new List<Option>();
                            string[] userAnswerStr = question.UserAnswer.Split(',');
                            for (int j = 0; j < userAnswerStr.Length; j++)
                            {
                                Option o = new Option();
                                o.OptionID = userAnswerStr[j].ToGuid();
                                userOptions.Add(o);
                            }
                            var m = from a in question.QuestionOption join b in userOptions on a.OptionID equals b.OptionID where a.QuestionID == questionID select new { OptionCode = a.OptionCode, OptionID = b.OptionID };
                            foreach (var n in m)
                            {
                                userAnswer = new UserAnswer();
                                userAnswer.OptionID = n.OptionID;
                                userAnswer.OptionCode = n.OptionCode;
                                userAnswers.Add(userAnswer);
                            }
                            break;
                        case QuestionType.TextEntry:
                            break;
                        case QuestionType.ExtendedText:
                            break;
                        case QuestionType.Match:
                            break;
                        case QuestionType.Group:
                            break;
                        default:
                            break;
                    }
                }
                new ExamLogic().SaveQuestionAnswer(userExamID, testPaperID, questionID, userAnswers, questionType, StudentID);
            }
        }
        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }

        /// <summary> 
        /// 返回时间差[返回秒] 
        /// </summary> 
        /// <param name="DateTimex">时间1（小）</param> 
        /// <param name="DateTimed">时间2（大）</param> 
        /// <returns>返回秒</returns> 
        public static double TDiff(DateTime DateTimex, DateTime DateTimed)
        {

            return ((TimeSpan)(DateTimed - DateTimex)).TotalSeconds;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}