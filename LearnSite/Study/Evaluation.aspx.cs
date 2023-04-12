using ETMS.Components.ExOnlineTest.Implement.BLL;
using System;
using System.Data;
using System.Web.UI;
using ETMS.Utility;
using University.Mooc.AppContext;
using System.Configuration;
using ETMS.Components.ExOfflineHomework.Implement.BLL;

namespace ETMS.Studying.Study
{
    public partial class Evaluation : System.Web.UI.Page
    {
        #region 页面参数
        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        private Guid TrainingItemCourseID
        {
            get {
                return Request.QueryString["trainingItemCourseID"].ToGuid();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EvaluationDataBind();
            }
        }
        /// <summary>
        /// 测评列表
        /// </summary>
        private void EvaluationDataBind()
        {
            Ex_StudentEvaluationLogic EvaluationLogic = new Ex_StudentEvaluationLogic();
            DataSet dsEvaluation = EvaluationLogic.GetStudentEvaluationListByUserID(UserContext.Current.UserID, TrainingItemCourseID);
            //设置测试的允许提交次数
            DataTable dt = dsEvaluation.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TestCount"].ToInt() == 0)
                {
                    dr["TestCount"] = string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrgMaxExamNum"]) ? 3 : ConfigurationManager.AppSettings["OrgMaxExamNum"].ToInt();
                }
            }
            this.rptEvaluationTest.DataSource = dsEvaluation.Tables[0];
            this.rptEvaluationTest.DataBind();

            this.rptEvaluationHomework.DataSource = dsEvaluation.Tables[1];
            this.rptEvaluationHomework.DataBind();

            var offlineJobs = new Res_e_OffLineJobLogic().GetUserCourseOffLineJobs(UserContext.Current.UserID, TrainingItemCourseID);
            this.rptOffLineJob.DataSource = offlineJobs;
            this.rptOffLineJob.DataBind();
        }
        /// <summary>
        /// 做作业的地址
        /// </summary>
        /// <param name="TestPaperID">试卷ID</param>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="OnlineTestID">测试ID</param>
        /// <param name="StudentCourseID">学生选课ID</param>
        /// <param name="testType">测评类型：2-作业；5-测试</param>
        /// <returns></returns>
        protected string getUrl(Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID,int testType)
        {
            if (testPaperID == Guid.Empty)
            {
                return "#";
            }
            else
            {
                return this.ActionHref(string.Format("~/Study/DoHomework.aspx?TestPaperID={0}&TrainingItemCourseID={1}&OnlineTestID={2}&StudentCourseID={3}&TestType={4}", testPaperID, trainingItemCourseID, onlineTestID, studentCourseID,testType));
            }
        }
        /// <summary>
        /// 获取查看测试
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="trainingItemCourseID"></param>
        /// <param name="onlineTestID"></param>
        /// <param name="studentCourseID"></param>
        /// <param name="testType"></param>
        /// <returns></returns>
        protected string getViewUrl(Guid testPaperID, Guid trainingItemCourseID, Guid onlineTestID, Guid studentCourseID, int testType,string UserExamID="")
        {
            if (testPaperID == Guid.Empty)
            {
                return "#";
            }
            else
            {
                if (testType == 5)
                {
                    return this.ActionHref(string.Format("~/Study/TestResultList.aspx?TestPaperID={0}&TrainingItemCourseID={1}&OnlineTestID={2}&StudentCourseID={3}&TestType={4}", testPaperID, trainingItemCourseID, onlineTestID, studentCourseID,testType));
                }
                else
                {
                    return this.ActionHref(string.Format("~/Study/SeeAnswerResult.aspx?TestPaperID={0}&TrainingItemCourseID={1}&OnlineTestID={2}&StudentCourseID={3}&TestType={4}&UserExamID={5}", testPaperID, trainingItemCourseID, onlineTestID, studentCourseID, testType,UserExamID));
                }
                
            }
        }
    }
}