using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;

public partial class TraningImplement_StudentCourseManager_Controls_StudentSelect : System.Web.UI.UserControl
{
    #region 页面参数
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
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
                ViewState["TrainingItemCourseID"] = Guid.Empty;

            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
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
                ViewState["SortExpression"] = " u.OrganizationID,u.DepartmentID,u.RealName";
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
            bind();
            //单机构版本隐藏机构列
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                trOrg1.Visible = false;
                ddl_u999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }
            this.PageSet1.QueryChange();
        }
    }
    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourse ItemCourse = new Tr_ItemCourseLogic().GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            //邦定组织机构信息
            ddl_u999OrganizationID.DataSource = new Sty_StudentSignupLogic().GetTrainingItemOrganizationList(ItemCourse.TrainingItemID);
            ddl_u999OrganizationID.DataTextField = "DisplayPath";
            ddl_u999OrganizationID.DataValueField = "OrganizationID";
            ddl_u999OrganizationID.DataBind();
            Tr_Item item = new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID);
            if (item != null && item.IsIssue)
            {
                lbtnDel.Enabled = false;
                //cbtnDel.CssClass = "link_colorGray";
            }
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
        string orgs = " AND u.OrganizationID in (";
        foreach (ListItem item in ddl_u999OrganizationID.Items)
        {
            if (item.Value.Trim() != "" && item.Value.Trim() != "-1")
                orgs += item.Value + ",";
        }
        orgs += "-1)";
        Crieria = string.Format(" {0} {1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), orgs);

        Sty_StudentSignupLogic StudentCourseLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentCourseLogic.GetStudentListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 删除
    /// </summary>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourses = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (StudentCourses.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要删除的学员！");
            return;
        }
        else
        {
            try
            {
                Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
                StudentCourseLogic.DeleteStudentSelectCourse(StudentCourses);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员删除成功！", "function(){window.location =window.location;triggerParentSearchEvent();}");
                //this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string StudentCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            #region 获取控件
            HiddenField hfCourseStatus = (HiddenField)e.Row.FindControl("hfCourseStatus");
            hfCourseStatus = hfCourseStatus == null ? new HiddenField() : hfCourseStatus;

            LinkButton lbtn_Open = (LinkButton)e.Row.FindControl("lbtn_Open");
            lbtn_Open = lbtn_Open == null ? new LinkButton() : lbtn_Open;

            LinkButton lbtn_Close = (LinkButton)e.Row.FindControl("lbtn_Close");
            lbtn_Close = lbtn_Close == null ? new LinkButton() : lbtn_Close;
            #endregion

            switch (hfCourseStatus.Value.Trim())
            {
                case "1":
                    lbtn_Open.Visible = false;
                    lbtn_Close.Visible = true;
                    break;
                case "0":
                    lbtn_Open.Visible = true;
                    lbtn_Close.Visible = false;
                    break;
            }
        }
    }
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Open" || e.CommandName == "Close")
        {
            try
            {
                Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
                StudentCourseLogic.SetStudentCourseIsUse(e.CommandArgument.ToGuid(), e.CommandName == "Open" ? 1 : 0);
                ETMS.Utility.JsUtility.SuccessMessageBox("操作成功！");
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