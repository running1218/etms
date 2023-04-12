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

public partial class Security_TeacherQuery_TeacherTraningItemCourseList : BasePage
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
        List<TeacherTraniningItemCourseInfo> list = teacherLogic.GetTeacherTraningItemCourseList(TeacherID, CourseBeginTimeBegin, CourseBeginTimeEnd, pageIndex, pageSize, out totalRecordCount);

        //负责项目总课时
        decimal courseHours=0;
        foreach (TeacherTraniningItemCourseInfo itemCourse in list)
        {
            courseHours += itemCourse.CourseHours;
        }
        lblCourseHours.Text = courseHours.ToString();

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}