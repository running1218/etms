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

public partial class TraningImplement_StudentCourseManager_Controls_StudentNoSelect : System.Web.UI.UserControl
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
    /// 项目ID
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
            //获得项目课程信息
            Tr_ItemCourse ItemCourse = new Tr_ItemCourseLogic().GetById(TrainingItemCourseID);
            if (ItemCourse != null)
            {
                //邦定组织机构信息
                ddl_u999OrganizationID.DataSource = new Sty_StudentSignupLogic().GetTrainingItemOrganizationList(ItemCourse.TrainingItemID);
                ddl_u999OrganizationID.DataTextField = "DisplayPath";
                ddl_u999OrganizationID.DataValueField = "OrganizationID";
                ddl_u999OrganizationID.DataBind();
            }
            //单机构版本隐藏机构列
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                trOrg2.Visible = false;
                ddl_u999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }
            this.PageSet1.QueryChange();
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

        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentSignupLogic.GetNoSelectCourseAllInfoListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的学员！");
            return;
        }
        else
        {
            try
            {
                Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
                StudentCourseLogic.AddStudentSelectCourse(TrainingItemCourseID
                    , selectedValues
                    , ETMS.AppContext.UserContext.Current.UserID
                    , ETMS.AppContext.UserContext.Current.RealName);

                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！", "function(){window.location = window.location;triggerParentSearchEvent();}");
                //this.PageSet1.QueryChange(); 
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
    }
}