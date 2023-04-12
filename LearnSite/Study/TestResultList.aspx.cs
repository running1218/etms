using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility;
using System;
using System.Data;
using System.Web.UI;

namespace ETMS.Studying.Study
{
    public partial class TestResultList : System.Web.UI.Page
    {
        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        private Guid TrainingItemCourseID
        {
            get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
        }
        /// <summary>
        /// 试卷ID
        /// </summary>
        private Guid TestPaperID
        {
            get { return Request.QueryString["TestPaperID"].ToGuid(); }
        }
        /// <summary>
        /// 学生选课ID
        /// </summary>
        private Guid StudentCourseID
        {
            get { return Request.QueryString["StudentCourseID"].ToGuid(); }
        }
        /// <summary>
        /// 测评【考试ID或者作业ID】 
        /// </summary>
        private Guid OnlineTestID
        {
            get { return Request.QueryString["OnlineTestID"].ToGuid(); }
        }
        /// <summary>
        /// 试卷测试类型：2-在线作业；5-在线测试
        /// </summary>
        private int TestType
        {
            get { return Request.QueryString["TestType"].ToInt(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TestResultDataBind();
            }
        }
        /// <summary>
        /// 测评的在线测试列表
        /// </summary>
        private void TestResultDataBind()
        {
            DataTable dtTestResult = new Sty_StudentSignupLogic().GetStudentCourseOnLineTestLisByTestID(UserContext.Current.UserID, StudentCourseID,OnlineTestID);
            this.rptTestResultList.DataSource = dtTestResult;
            this.rptTestResultList.DataBind();

        }
        /// <summary>
        /// 查看测试的地址
        /// </summary>
        /// <returns></returns>
        protected string getUrl(Guid UserExamID)
        {
            if (TestPaperID == Guid.Empty)
            {
                return "#";
            }
            else
            {
                return this.ActionHref(string.Format("~/Study/SeeAnswerResult.aspx?TestPaperID={0}&TrainingItemCourseID={1}&OnlineTestID={2}&StudentCourseID={3}&TestType={4}&UserExamID={5}", TestPaperID, TrainingItemCourseID, OnlineTestID, StudentCourseID, TestType, UserExamID));
            }
        }
    }
}