using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class TraningImplement_CourseStudentManager_StudentList : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
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
            {
                ViewState["Crieria"] = "";
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
   }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic item = new Tr_ItemLogic();
        //对本组织机构创建的已发布未归档的启用的培训项目
        string crieriaItem = string.Format(" AND Tr_Item.ItemStatus in (10,20,40) AND IsIssue=1 AND IsUse=1 AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_TrainingItemID.DataSource = item.GetPagedList(1, int.MaxValue - 1, "  CreateTime DESC", crieriaItem, out total);
        ddl_TrainingItemID.DataTextField = "ItemName";
        ddl_TrainingItemID.DataValueField = "TrainingItemID";
        ddl_TrainingItemID.DataBind();
    }


    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); 
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable tab = StudentSignupLogic.GetStudentListALLByTrainingItemID(ddl_TrainingItemID.SelectedValue.ToGuid(), pageIndex, pageSize, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(tab, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    //行邦定
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow) {
            int UserID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToInt();

            //学员课程数
            Label lblCourseTotal = (Label)e.Row.FindControl("lblCourseTotal");
            lblCourseTotal = lblCourseTotal == null ? new Label() : lblCourseTotal;
            //设置课程
            LinkButton lbtnCourseManager = (LinkButton)e.Row.FindControl("lbtnCourseManager");
            lbtnCourseManager = lbtnCourseManager == null ? new LinkButton() : lbtnCourseManager;

            lblCourseTotal.Text = new Sty_StudentSignupLogic().GetStudentCourseNumByTrainingItemID(UserID, ddl_TrainingItemID.SelectedValue.ToGuid()).ToString();

            lbtnCourseManager.PostBackUrl = this.ActionHref(string.Format("SetCourse.aspx?UserID={0}&TrainingItemID={1}", UserID, ddl_TrainingItemID.SelectedValue));
        }
    }
}