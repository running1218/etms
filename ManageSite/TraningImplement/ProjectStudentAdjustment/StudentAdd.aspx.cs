using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class TraningImplement_ProjectStudentAdjustment_StudentAdd : BasePage
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

    /// <summary>
    /// 点击确定后选择课程的URL
    /// </summary>
    public string SelectCourseUrl {
        get {
            return this.ActionHref(string.Format("TraningProjectCourseList.aspx?TrainingItemID={0}", TrainingItemID));
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
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("ProjectStudentAdjustment.aspx?TrainingItemID={0}", TrainingItemID));
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
        string orgs = " AND OrganizationID in (";
        foreach (ListItem item in ddl_OrganizationID.Items)
        {
            if (item.Value.Trim() != "" && item.Value.Trim() != "-1")
                orgs += item.Value + ",";
        }
        orgs += "-1)";
        Crieria = string.Format(" {0} {1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), orgs);

        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentSignupLogic.GetNoSelectStudentListByTrainingItemID(TrainingItemID,pageIndex
            , pageSize
            , SortExpression
            , Crieria
            , out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        StudentSignupAdd();
        string ics = "";
        #region 因返回的值有重复此过滤重复
        string[] itemCourseIDs = hfItemCourseIDs.Value.Trim().Split(',');
        for (int i = 0; i < itemCourseIDs.Length; i++)
        {
            if (itemCourseIDs[i].Trim() != "")
            {
                if (ics.IndexOf(itemCourseIDs[i].Trim() + ",") < 0)
                {
                    ics += itemCourseIDs[i].Trim() + ",";
                }
            }
        }
        hfItemCourseIDs.Value = "";
        #endregion

        #region 查出学员报名ID
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
        string userIDs = "0,";
        foreach (int u in selectedValues)
        {
            userIDs += u + ",";
        }
        string crieria = string.Format(" and Site_User.UserID in ({0})", userIDs.Trim(','));
        int totalRecordCount = 0;

        DataTable tab = new Sty_StudentSignupLogic().GetStudentListALLByTrainingItemID(TrainingItemID, 1, int.MaxValue, crieria, out totalRecordCount);

        Guid[] StudentSignupIDs = new Guid[tab.Rows.Count];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            StudentSignupIDs[i] = tab.Rows[i]["StudentSignupID"].ToGuid();
        }
        #endregion

        string[] TrainingItemCourseIDs = ics.Trim(',').Split(',');
        try
        {
            //添加学员选课
            Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
            for (int i = 0; i < TrainingItemCourseIDs.Length; i++)
            {
                StudentCourseLogic.AddStudentSelectCourse(TrainingItemCourseIDs[i].ToGuid()
                    , StudentSignupIDs
                    , ETMS.AppContext.UserContext.Current.UserID
                    , ETMS.AppContext.UserContext.Current.RealName);
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
        this.PageSet1.DataBind();
        ETMS.Utility.JsUtility.SuccessMessageBox("提示", "学员添加成功并且已选中对应的课程！");
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        StudentSignupAdd();
        this.PageSet1.DataBind();
        ETMS.Utility.JsUtility.SuccessMessageBox("提示", "学员添加成功！");
    }

    /// <summary>
    /// 添加学员报名方法
    /// </summary>
    private void StudentSignupAdd() {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
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
            //ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！", "function(){window.location = '" + this.ActionHref("ProjectStudentAdjustment.aspx?TrainingItemID=" + TrainingItemID.ToString()) + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
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