using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Teacher;

public partial class Security_TeacherQuery_TeacherItemList :BasePage
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
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Site_TeacherLogic teacherLogic=new Site_TeacherLogic();
        List<Tr_Item> list = teacherLogic.GetTeacherTraningItemList(TeacherID, CourseBeginTimeBegin, CourseBeginTimeEnd, pageIndex, pageSize, out totalRecordCount);

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
            Guid TrainingItemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();

            //查看
            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            lbtnView.PostBackUrl = this.ActionHref(string.Format("TeacherItemView.aspx?TrainingItemID={0}&CourseBeginTimeBegin={1}&CourseBeginTimeEnd={2}&TeacherID={3}"
                , TrainingItemID
                , CourseBeginTimeBegin
                , CourseBeginTimeEnd
                ,TeacherID));
        }
    }
}