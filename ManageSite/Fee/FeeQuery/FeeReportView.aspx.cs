using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;

public partial class FeeReportView : System.Web.UI.Page
{
    #region 页面条件参数存放

    /// <summary>
    /// 课时安排ID
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get
        {
            if (ViewState["ItemCourseHoursID"] == null)
            {
                ViewState["ItemCourseHoursID"] = Guid.Empty;
            }
            return (Guid)ViewState["ItemCourseHoursID"];
        }
        set
        {
            ViewState["ItemCourseHoursID"] = value;
        }
    }

    #endregion
    public static PublicFacade publicFacade = new PublicFacade();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ItemCourseHoursID = Request.QueryString["ItemCourseHoursID"].ToGuid();
            InitData();
        }
    }

    private void InitData()
    {
        string Crieria = string.Format(" And a.PayStatus=1 And a.CourseHoursStatusID=1 And c.TeacherSourceID = 1 And a.ItemCourseHoursID='{0}'", ItemCourseHoursID);

        Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

        int totalRecordCount = 0;
        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(1, 1, "", Crieria, out totalRecordCount);

        if (dt.Rows.Count > 0)
        {
            ltlTeacherName.Text = dt.Rows[0]["TeacherName"].ToString();
            ltlOrgName.Text = publicFacade.GetOrgNameByID(dt.Rows[0]["OrganizationID"].ToInt());
            lblDepartment.FieldIDValue = dt.Rows[0]["DepartmentID"].ToString(); 
            ltlItemTime.Text = dt.Rows[0]["TrainingDate"].ToDate();
            ltlTeacherLevel.FieldIDValue = dt.Rows[0]["TeacherLevelID"].ToString();
            ltlItemName.Text = dt.Rows[0]["ItemName"].ToString();
            ltlCourseName.Text = dt.Rows[0]["CourseName"].ToString();
            ltlTrainingTimeDesc.Text = dt.Rows[0]["CourseName"].ToString();
            ltlTrainingTimeDesc.FieldIDValue = dt.Rows[0]["TrainingTimeDescID"].ToString();
            ltlTimeBeginEnd.Text = dt.Rows[0]["TrainingBeginTime"].ToDateTime().ToShortTimeString() + " - " + dt.Rows[0]["TrainingEndTime"].ToDateTime().ToShortTimeString();
            ltlCourseFee.Text = dt.Rows[0]["CourseFee"].ToString();
            ltlCourseHours.Text = string.Format("{0:N1}", dt.Rows[0]["CourseHours"].ToString().ToDecimal());
            ltlRealCourseHours.Text = string.Format("{0:N1}", dt.Rows[0]["RealCourseHours"].ToString().ToDecimal());
            ltlRealCourseFee.Text = dt.Rows[0]["RealCourseFee"].ToString();
            ltlCreateTime.Text = dt.Rows[0]["ModifyTime"].ToDateTime().ToString();
            ltlCreator.Text = dt.Rows[0]["ModifyUser"].ToString();
            
        }

    }
}