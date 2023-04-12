using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_TraningProjectManager_TraningProjectList : BasePage
{
    #region 页面参数

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

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

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
        this.PageSet1.QueryChange();
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

            CustomLinkButton lbtnDel = (CustomLinkButton)e.Row.FindControl("lbtnDel");
            lbtnDel = lbtnDel == null ? new CustomLinkButton() : lbtnDel;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            LinkButton lbtnCopy = (LinkButton)e.Row.FindControl("lbtnCopy");
            lbtnCopy = lbtnCopy == null ? new LinkButton() : lbtnCopy;
                        
            #endregion
            switch (Hf_ItemStatus.Value)
            {
                case "10":
                    Lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('编辑培训项目','{0}',650,500);javascript:return false;", this.ActionHref(string.Format("TraningProjectEdit.aspx?TrainingItemID={0}", itemID)));
                    Lbtn_Edit.Enabled = true;
                    lbtnDel.Enabled = true;
                    lbtnDel.EnableConfirm = true;
                    break;
                default:
                    Lbtn_Edit.Enabled = false;
                    Lbtn_Edit.CssClass = "link_colorGray";
                    lbtnDel.Enabled = false;
                    lbtnDel.CssClass = "link_colorGray";
                    break;
            }
            lbtnCopy.Attributes["onclick"] = string.Format("javascript:showWindow('复制培训项目','{0}',650,450);javascript:return false;", this.ActionHref(string.Format("TraningProjectCopy.aspx?TrainingItemID={0}", itemID)));
                    
            //查看
            lbtnView.PostBackUrl = this.ActionHref("TraningProjectView.aspx?TrainingItemID=" + itemID);
            lbtnView.Enabled = true;

            #region 培训项目发布，未规档的培训项目可以对某些基本信息修改，也可以向项目中追加课程。
            HiddenField hf_IsIssue = (HiddenField)e.Row.FindControl("hf_IsIssue");
            hf_IsIssue = hf_IsIssue == null ? new HiddenField() : hf_IsIssue;

            if (Hf_ItemStatus.Value.Trim() != "90" && hf_IsIssue.Value.Trim().ToLower() == "true")
            {
                Lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('培训项目调整','{0}','500','350');javascript:return false;", this.ActionHref(string.Format("TraningProjectAdjust.aspx?TrainingItemID={0}", itemID)));
                Lbtn_Edit.Enabled = true;
                Lbtn_Edit.CssClass = "";
            }
            #endregion
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
                itemLogic.doRemove(e.CommandArgument.ToGuid());
                ETMS.Utility.JsUtility.SuccessMessageBox("项目信息删除成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}