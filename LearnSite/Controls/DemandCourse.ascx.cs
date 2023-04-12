using ETMS.Components.Basic.Implement.BLL.Course;
using System;
using System.Data;

namespace ETMS.Studying.Controls
{
    public partial class DemandCourse : System.Web.UI.UserControl
    {
        #region 页面参数
        /// <summary>
        /// 查询条件 
        /// </summary>
        protected string Crieria
        {
            get
            {
                return (string)ViewState["Crieria"];
            }
            set
            {
                ViewState["Crieria"] = value;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        protected string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = " IsTop DESC,Sort ASC ";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DemandCourseDataBind();
            }
        }
        /// <summary>
        /// 点播课程列表
        /// </summary>
        private void DemandCourseDataBind()
        {
            int totalRecords = 0;
            DataTable dt = new Rec_CourseLogic().GetDemandCoursePagedList(1,20,SortExpression,Crieria, BaseUtility.SiteOrganizationID, out totalRecords);

            //foreach (DataRow row in dt.Rows)
            //{
               //row["FocusCount"] = new Sty_StudentCourseLogic().GetStudentCourseUserTotal(row["CourseID"].ToGuid()).ToString();
            //}
            CourseDataList.DataSource = dt;
            CourseDataList.DataBind();
        }
    }
}