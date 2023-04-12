using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.API.Entity.Teacher;

public partial class Security_TeacherQuery_TeacherTeachCourseList : System.Web.UI.Page
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
                ViewState["TeacherID"] =0;

            return (int)ViewState["TeacherID"];
        }
        set
        {
            ViewState["TeacherID"] = value;
        }
    }

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
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
                ViewState["SortExpression"] = "";
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
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            if(!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TeacherID")))
            {
               TeacherID =BasePage.getSafeRequest(this.Page, "TeacherID").ToInt();
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
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Res_TeacherCourseLogic TeacherCourseLogic = new Res_TeacherCourseLogic();
        List<Res_Course> list = TeacherCourseLogic.GetTeacherTeachCourse(TeacherID,pageIndex,pageSize,out totalRecordCount);

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
            Guid CourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();

            //查看
            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            lbtnView.PostBackUrl = this.ActionHref(string.Format("TeacherTeachCourseView.aspx?CourseID={0}&TeacherID={1}", CourseID, TeacherID));
        }
    }
}