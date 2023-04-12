using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class TraningImplement_TraningProjectQuery_ProjectStudentList : System.Web.UI.Page
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
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
            this.PageSet1.QueryChange();
        }
        lbtnBack.PostBackUrl = this.ActionHref("TraningProjectList.aspx");
    }
    
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            lbl_ItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());
        }
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
        //Crieria = string.Format(" AND Site_User.OrganizationID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();

        DataTable tab = StudentSignupLogic.GetStudentListALLByTrainingItemID(TrainingItemID, pageIndex, pageSize, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(tab, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    
    /// <summary>
    /// 行邦定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnCourseTotal = (LinkButton)e.Row.FindControl("lbtnCourseTotal");
            lbtnCourseTotal = lbtnCourseTotal == null ? new LinkButton() : lbtnCourseTotal;

            lbtnCourseTotal.Text = new Sty_StudentSignupLogic().GetStudentCourseNumByTrainingItemID(lbtnCourseTotal.CommandArgument.ToInt(), TrainingItemID).ToString();
            lbtnCourseTotal.PostBackUrl = this.ActionHref( string.Format("ProjectStudentCourseList.aspx?TrainingItemID={0}&UserID={1}",TrainingItemID,lbtnCourseTotal.CommandArgument));
        }
    }
}