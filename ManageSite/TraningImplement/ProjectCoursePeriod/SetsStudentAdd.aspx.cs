using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_ProjectCoursePeriod_SetsStudentAdd : System.Web.UI.Page
{
    #region 页面参数
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
    /// 项目课程课时ID
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get
        {
            if (ViewState["ItemCourseHoursID"] == null)
                ViewState["ItemCourseHoursID"] = Guid.Empty;
            return ViewState["ItemCourseHoursID"].ToGuid();
        }
        set
        {
            ViewState["ItemCourseHoursID"] = value;
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
        if (!IsPostBack)
        {
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_u999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }

            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
                bind();
            }

            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID")))
            {
                ItemCourseHoursID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID"));
            }
            ddl_u999OrganizationID_SelectedIndexChanged(sender, e);//触发Selected事件
            this.PageSet1.QueryChange(); 
        }
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("SetsStudentList.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, ItemCourseHoursID));
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            //邦定组织机构信息
            ddl_u999OrganizationID.DataSource = new Sty_StudentSignupLogic().GetTrainingItemOrganizationList(ItemCourse.TrainingItemID);
            ddl_u999OrganizationID.DataTextField = "DisplayPath";
            ddl_u999OrganizationID.DataValueField = "OrganizationID";
            ddl_u999OrganizationID.DataBind();
         
            //项目名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
            {
                lblItemName.Text = item.ItemName;
                bindClass(item.TrainingItemID);
            }
            // 课程相关信息
            lblCourseName.Text = new Res_CourseLogic().GetById(ItemCourse.CourseID).CourseName;
        }
    }

    /// <summary>
    /// 邦定当前项目班级信息
    /// </summary>
    private void bindClass(Guid TrainingItemID) {
        Sty_ClassLogic classLogic = new Sty_ClassLogic();
        ddl_c999ClassID.DataSource = classLogic.GetClassListByTrainingItemID(TrainingItemID);
        ddl_c999ClassID.DataTextField = "ClassName";
        ddl_c999ClassID.DataValueField = "ClassID";
        ddl_c999ClassID.DataBind();
        ddl_c999ClassID.Items.Insert(0, new ListItem("全部", ""));
    }

    #region 机构 部门 岗位
    
    /// <summary>
    /// 机构选中事件
    /// </summary>
    protected void ddl_u999OrganizationID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddl_u999OrganizationID.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_u999DepartmentID.DataSource = dt;
        this.ddl_u999DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_u999DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_u999DepartmentID.DataBind();
        this.ddl_u999DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_u999DepartmentID.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddl_u999PostID.DataSource = dt;
        this.ddl_u999PostID.DataTextField = "ColumnNameValue";
        this.ddl_u999PostID.DataValueField = "ColumnCodeValue";
        this.ddl_u999PostID.DataBind();
        this.ddl_u999PostID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_u999PostID.SelectedIndex = 0;
    }
    #endregion

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); 
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Tr_ItemCourseHoursLogic courseHoursLogic = new Tr_ItemCourseHoursLogic();
        DataTable dt = courseHoursLogic.GetItemCourseHours_GetNoSelectStudentLis(ItemCourseHoursID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的学员！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseHoursStudentLogic hoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
                hoursStudentLogic.BatchAddStudentListToCourseHours(ItemCourseHoursID, selectedValues, ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "学员信息添加成功！", "function (){window.location='" + this.ActionHref(string.Format("SetsStudentList.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, ItemCourseHoursID)) + "'}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
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

        if (e.Row.RowType == DataControlRowType.DataRow) {
            //班级
            DictionaryLabel lblClassName = (DictionaryLabel)e.Row.FindControl("lblClassName");
            lblClassName = lblClassName == null ? new DictionaryLabel() : lblClassName;

            if (lblClassName.FieldIDValue.Trim() != "" && lblClassName.FieldIDValue.ToGuid() != Guid.Empty)
            {
                lblClassName.Text = new Sty_ClassLogic().GetById(lblClassName.FieldIDValue.ToGuid()).ClassName;
            }
        }
    }
}