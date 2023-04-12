using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.WebApp.Manage;

public partial class TraningImplement_TraningProjectManager_Controls_TraningProjectList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        if (Op == 1)
        {
            btnAdd.Visible = true;
        }
    }

    #region 页面参数

    /// <summary>
    /// 1 项目管理,2 项目学员管理
    /// </summary>
    public int Op
    {
        get
        {
            if (ViewState["Op"] == null)
                ViewState["Op"] = 1;
            return (int)ViewState["Op"];
        }
        set { ViewState["Op"] = value; }
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
                ViewState["SortExpression"] = " CreateTime DESC";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //1 项目管理 2设置学员
        if (Op == 2)
        {
            Crieria = " AND ItemStatus=20 ";
        }
        Crieria += string.Format(" {0} AND OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemLogic item = new Tr_ItemLogic();
        DataTable dt = item.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
            string itemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            #region 获取控件
            HiddenField Hf_ItemStatus = (HiddenField)e.Row.FindControl("Hf_ItemStatus");
            Hf_ItemStatus = Hf_ItemStatus == null ? new HiddenField() : Hf_ItemStatus;

            LinkButton Lbtn_Edit = (LinkButton)e.Row.FindControl("Lbtn_Edit");
            Lbtn_Edit = Lbtn_Edit == null ? new LinkButton() : Lbtn_Edit;

            LinkButton Lbtn_CourseList = (LinkButton)e.Row.FindControl("Lbtn_CourseList");
            Lbtn_CourseList = Lbtn_CourseList == null ? new LinkButton() : Lbtn_CourseList;

            CustomLinkButton lbtnDel = (CustomLinkButton)e.Row.FindControl("lbtnDel");
            lbtnDel = lbtnDel == null ? new CustomLinkButton() : lbtnDel;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            LinkButton lbtnStudent = (LinkButton)e.Row.FindControl("lbtnStudent");
            lbtnStudent = lbtnStudent == null ? new LinkButton() : lbtnStudent;
            #endregion
            //1 项目管理 2设置学员
            if (Op == 1 && Hf_ItemStatus != null)
            {
                switch (Hf_ItemStatus.Value)
                {
                    case "10":
                        Lbtn_Edit.Attributes["onclick"] = "javascript:return showWindowPage('编辑培训项目','" + this.ActionHref("../TraningProjectEdit.aspx?TrainingItemID=" + itemID) + "')";
                        Lbtn_Edit.Enabled = true;
                        Lbtn_CourseList.PostBackUrl = this.ActionHref("../SetsCourse.aspx?TrainingItemID=" + itemID);
                        Lbtn_CourseList.Enabled = true;
                        lbtnDel.Enabled = true;
                        lbtnDel.EnableConfirm = true;
                        break;
                }
                //查看
                lbtnView.PostBackUrl = this.ActionHref("../TraningProjectView.aspx?TrainingItemID=" + itemID);
                lbtnView.Enabled = true;
            }
            else if (Hf_ItemStatus != null)
            {
                Lbtn_Edit.Visible = false;
                Lbtn_CourseList.Visible = false;
                lbtnDel.Visible = false;
                lbtnView.Visible = false;
                lbtnStudent.PostBackUrl = this.ActionHref("../SetsStudentList.aspx?TrainingItemID=" + itemID);
                lbtnStudent.Visible = true;
            }
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            try
            {
                Tr_ItemLogic itemLogic = new Tr_ItemLogic();
                itemLogic.Remove(e.CommandArgument.ToGuid());
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("项目信息删除成功！");
                this.PageSet1.QueryChange(); upList.Update();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
            catch {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage("项目信息删除失败！");
            }
        }
    }
}