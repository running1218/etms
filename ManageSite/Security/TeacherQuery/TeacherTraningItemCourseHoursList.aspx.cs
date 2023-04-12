using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;

public partial class Security_TeacherQuery_TeacherTraningItemCourseHoursList : BasePage
{
    #region 页面参数
    /// <summary>
    /// 讲师ID
    /// </summary>
    public int TeacherID
    {
        get
        {
            if (ViewState["TeacherID"] == null)
                ViewState["TeacherID"] = 0;

            return (int)ViewState["TeacherID"];
        }
        set
        {
            ViewState["TeacherID"] = value;
        }
    }

    /// <summary>
    /// 课程开始时间 Begin
    /// </summary>
    public DateTime CourseBeginTimeBegin
    {
        get
        {
            if (ViewState["CourseBeginTimeBegin"] == null)
                ViewState["CourseBeginTimeBegin"] = "";
            return ViewState["CourseBeginTimeBegin"].ToDateTime();
        }
        set
        {
            ViewState["CourseBeginTimeBegin"] = value;
        }
    }

    /// <summary>
    /// 课程开始时间 End
    /// </summary>
    public DateTime CourseBeginTimeEnd
    {
        get
        {
            if (ViewState["CourseBeginTimeEnd"] == null)
                ViewState["CourseBeginTimeEnd"] = "";
            return ViewState["CourseBeginTimeEnd"].ToDateTime();
        }
        set
        {
            ViewState["CourseBeginTimeEnd"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            CourseBeginTimeBegin = BasePage.getSafeRequest(this.Page, "CourseBeginTimeBegin").ToDateTime();
            CourseBeginTimeEnd = BasePage.getSafeRequest(this.Page, "CourseBeginTimeEnd").ToDateTime();
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TeacherID")))
            {
                TeacherID = BasePage.getSafeRequest(this.Page, "TeacherID").ToInt();
                bind();
            }

            this.PageSet1.QueryChange();
        }
        lbtnReturn.PostBackUrl = this.ActionHref("TeacherList.aspx");
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Site_Teacher teacher = new PublicFacade().GetTeacherInfo(TeacherID);
        lblTeacherName.Text = teacher.UserInfo.RealName;
        if (teacher.TeacherSourceID == 1)//1 内部，2 外聘
        {
            lblOrgName.Text = "部门：";
            lblOrg.Text = new ETMS.Components.Basic.Implement.BLL.Security.OrganizationLogic().GetNodeByID(teacher.UserInfo.OrganizationID).NodeName;
        }
        else if (teacher.TeacherSourceID == 2)
        {
            lblOrgName.Text = "培训机构：";
            try
            {
                //如果外聘机构为空会报错
                lblOrg.Text = new ETMS.Components.Basic.Implement.BLL.TraningOrgnization.Tr_OuterOrgLogic().GetById(teacher.OuterOrgID).OuterOrgName;
            }
            catch { }
        }
        if (!string.IsNullOrEmpty(CourseBeginTimeBegin.ToDate()) || !string.IsNullOrEmpty(CourseBeginTimeEnd.ToDate()))
            lblItemDate.Text = string.Format("{0} 至 {1}", CourseBeginTimeBegin.ToDate(), CourseBeginTimeEnd.ToDate());
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Site_TeacherLogic teacherLogic = new Site_TeacherLogic();
        List<TeacherTrainingItemCourseHoursInfo> list = teacherLogic.GetTeacherTraningItemCourseHoursList(TeacherID, CourseBeginTimeBegin, CourseBeginTimeEnd, pageIndex, pageSize, out totalRecordCount);
        
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }


    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid itemCourseHoursID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获取控件
           
            //学员数
            Label lblStudentTotal = (Label)e.Row.FindControl("lblStudentTotal");
            lblStudentTotal = lblStudentTotal == null ? new Label() : lblStudentTotal;
            #endregion
          
            //课时学员数
            lblStudentTotal.Text = new Tr_ItemCourseHoursStudentLogic().GetItemCourseHoursStudentNumByItemCourseHoursID(itemCourseHoursID).ToString();
           
        }
    }
    
    /// <summary>
    /// 培训地点
    /// </summary>
    protected string GetAddress(object ClassRoomName, object Address)
    {
        string str = string.Empty;
        if (ClassRoomName != null)
            str += ClassRoomName.ToString();
        if (Address != null)
            str += "（" + Address.ToString() + "）";

        return str;
    }
}