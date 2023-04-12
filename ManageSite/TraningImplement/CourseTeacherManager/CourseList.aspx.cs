using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Controls;
using ETMS.Utility;

public partial class TraningImplement_CourseTeacherManager_CourseList : System.Web.UI.Page
{
    #region

    /// <summary>
    /// 项目ID
    /// </summary>
    public string TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = "";
            return ViewState["TrainingItemID"].ToString();
        }
        set { ViewState["TrainingItemID"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
            {
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID");
            }
            bind();
            this.PageSet1.QueryChange();
        }

        btnSearch.Attributes.Add("onclick", string.Format("return CheckSelectData('{0}')", ddl_ItemName.ClientID));
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        //本组织机构创建的待审核、审核通过的启用状态的培训项目
        string Crieria = string.Format(" AND ItemStatus in (10,20) AND IsUse=1  AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_ItemName.DataSource = itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);
        ddl_ItemName.DataTextField = "ItemName";
        ddl_ItemName.DataValueField = "TrainingItemID";
        ddl_ItemName.DataBind();

        if (!string.IsNullOrEmpty(TrainingItemID))
            ddl_ItemName.SelectedValue = TrainingItemID;
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(ddl_ItemName.SelectedValue.ToGuid(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); upList.Update();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();

            //讲师数
            Label lbl_TeacherTotal = (Label)e.Row.FindControl("lbl_TeacherTotal");
            if (lbl_TeacherTotal != null)
            {
                lbl_TeacherTotal.Text = new Tr_ItemCourseTeacherLogic().GetTeacherTotal(TrainingItemCourseID).ToString();
            }
            //讲师管理
            LinkButton lbtn_SetTeacher = (LinkButton)e.Row.FindControl("lbtn_SetTeacher");
            if (lbtn_SetTeacher != null)
            {
                //lbtn_SetTeacher.Attributes["onclick"] = string.Format("javascript:showWindow('讲师管理','{0}');javascript:return false;", this.ActionHref(string.Format("SetTeacher.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
                
                lbtn_SetTeacher.PostBackUrl = this.ActionHref(string.Format("SetTeacher.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
            }
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}