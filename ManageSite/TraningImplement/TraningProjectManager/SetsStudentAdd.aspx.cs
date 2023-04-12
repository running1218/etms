using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;

public partial class TraningImplement_TraningProjectManager_SetsStudentAdd : BasePage
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
                ViewState["Crieria"] = string.Format(" AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件  按组织机构、姓名排序
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " OrganizationID,RealName";
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
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            ddl_OrganizationID_SelectedIndexChanged(sender, e);//触发Selected事件
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
                ddl_OrganizationID_SelectedIndexChanged(sender, e);
            }
            this.PageSet1.QueryChange();
        }
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("SetsStudentList.aspx?TrainingItemID={0}", TrainingItemID));
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    #region 机构 部门 岗位

    /// <summary>
    /// 机构选中事件
    /// </summary>
    protected void ddl_OrganizationID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddl_OrganizationID.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_DepartmentID.DataSource = dt;
        this.ddl_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_DepartmentID.DataBind();
        this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_DepartmentID.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddl_PostID.DataSource = dt;
        this.ddl_PostID.DataTextField = "ColumnNameValue";
        this.ddl_PostID.DataValueField = "ColumnCodeValue";
        this.ddl_PostID.DataBind();
        this.ddl_PostID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_PostID.SelectedIndex = 0;
    }
    #endregion

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = GetData(pageIndex,pageSize,out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    //获取数据
    private DataTable GetData(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string orgs = " AND OrganizationID in (";
        foreach (ListItem item in ddl_OrganizationID.Items)
        {
            if (item.Value.Trim() != "" && item.Value.Trim() != "-1")
                orgs += item.Value + ",";
        }
        orgs += "-1)";
        Crieria = string.Format(" {0} {1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), orgs);

        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        return StudentSignupLogic.GetNoSelectStudentListByTrainingItemID(TrainingItemID, pageIndex
            , pageSize
            , SortExpression
            , Crieria
            , out totalRecordCount);
    }
    
    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的学员！");
            return;
        }
        else
        {
            try
            {
                Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
                StudentSignupLogic.TrainingItemCenterSignUp(TrainingItemID
                    , selectedValues
                    , DateTime.Now
                    , ETMS.AppContext.UserContext.Current.UserID
                    , ETMS.AppContext.UserContext.Current.RealName
                    , DateTime.Now
                    , ETMS.AppContext.UserContext.Current.RealName
                    , "");
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！");
                this.PageSet1.QueryChange(); 
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 添加全部
    /// </summary>
    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        try
        {
            int totalStudent = 0;
            DataTable dt = GetData(1, int.MaxValue - 1, out totalStudent);
            if (totalStudent > 0)
            {
                int[] selectedValues = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++) {
                    selectedValues[i] = dt.Rows[i]["UserID"].ToInt();
                }
                Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
                StudentSignupLogic.TrainingItemCenterSignUp(TrainingItemID
                    , selectedValues
                    , DateTime.Now
                    , ETMS.AppContext.UserContext.Current.UserID
                    , ETMS.AppContext.UserContext.Current.RealName
                    , DateTime.Now
                    , ETMS.AppContext.UserContext.Current.RealName
                    , "");

                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", string.Format("已将 {0} 条学员信息添加到项目中！", totalStudent));
                this.PageSet1.QueryChange();
            }
            else
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("提示", "没有满足条件的学员信息！");
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
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
    }

}