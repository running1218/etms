using University.Mooc.AppContext;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using System;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;

namespace ETMS.Studying.Self
{
    public partial class MyHistory : System.Web.UI.Page
    {   
        public string ImgUrl { get; set;}
           
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {             
                BindData();
            }
        }

        protected void BindData()
        {
            UserLogic userLogic = new UserLogic();         
            User user = userLogic.GetUserBaseData(UserContext.Current.UserID);
            //ArchivesRepter Bind
            Sty_StudentSignupLogic sty_StudentSignupLogic = new Sty_StudentSignupLogic();
            DataTable dt = sty_StudentSignupLogic.GetUserStudentArchives(UserContext.Current.UserID);

            foreach (DataRow row in dt.Rows)
            {
                var result = GetCourseProgressPercentage(row["TrainingItemCourseID"].ToGuid());
                row["Score"] = result;
                if (result >= 100)
                    row["IsCompleted"] = 1;
            }
            ArchivesRepter.DataSource = dt;
            ArchivesRepter.DataBind();
        }

        public decimal GetCourseProgressPercentage(Guid trainingItemCourseID)
        {

            var progressLogic = new Sty_UserStudyProgressLogic();
            int contentComplete = 0, contentNotstarted = 0, contentUnfinished = 0, testjobComplete = 0;
            var contentList = progressLogic.GetCourseProgressByTrainingItemCourseID(trainingItemCourseID, UserContext.Current.UserID, ref contentComplete, ref contentNotstarted, ref contentUnfinished);
            var ds = progressLogic.GetTestAndPaperProgress(UserContext.Current.UserID, trainingItemCourseID);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                int count = r["UserTestCount"] != null ? (string.IsNullOrWhiteSpace(r["UserTestCount"].ToString()) ? 0 : Convert.ToInt32(r["UserTestCount"])) : 0;
                if (count > 0)
                    testjobComplete += 1;
            }
            foreach (DataRow r in ds.Tables[1].Rows)
            {
                string status = r["TestStatus"] != null ? (string.IsNullOrWhiteSpace(r["TestStatus"].ToString()) ? "未提交" : r["TestStatus"].ToString()) : "未提交";
                if (status == "已提交")
                    testjobComplete += 1;
            }

            var total = contentList.Count + ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count;
            var percentage = total == 0 ? 0 : Math.Round((decimal)((contentComplete + testjobComplete) * 100 / total), 2, MidpointRounding.AwayFromZero);
            return percentage;
        }
    }
}