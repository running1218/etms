using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Components.Basic.Implement;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;

public partial class LearningManagement_ClassManager_StudentAdd : ETMS.Controls.BasePage
{
    public static PublicFacade publicFacade = new PublicFacade();
    public static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    private static Sty_ClassStudent classStudent = new Sty_ClassStudent();
    private static Sty_ClassStudentLogic classStudentLogic = new Sty_ClassStudentLogic();
    //private static List<Sty_ClassStudent> classStudentList = new List<Sty_ClassStudent>();
    private static OrganizationLogic organizationLogic = new OrganizationLogic();
    /// <summary>
    /// 班级ID
    /// </summary>
    private Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }       

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
             //多机构时显示 
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                trorg.Visible = false;
                this.CustomGridView1.Columns[3].Visible = false;
            }
            BindOrganizationList();
            this.PageSet1.QueryChange();
        }
        aBack.HRef = this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}", ClassID, TrainingItemID));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //string whereStr = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        classStudent.OrganizationID = this.ddlOrg.SelectedValue.ToInt();
        classStudent.PostID = this.ddlPost.SelectedValue.ToInt();
        classStudent.RankID = this.ddlRank.SelectedValue.ToInt();
        classStudent.RealName = this.txtRealName.Text.Trim();
        classStudent.DepartmentID = this.ddlDepartment.SelectedValue.ToInt();
        List<Sty_ClassStudent> classStudentList = classLogic.ChoseClassStudentList(TrainingItemID, classStudent, pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(classStudentList, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            Sty_ClassStudent entity = (Sty_ClassStudent)e.Row.DataItem;

        }
    }

    /// <summary>
    /// 获取列表中选中的
    /// </summary>
    private void InitialEntity()
    {
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                classStudent = new Sty_ClassStudent();
                //CustomGridView1.DataKeys[i].Values["StudentCourse"].ToString()
                classStudent.ClassID = ClassID;
                classStudent.UserID = CustomGridView1.DataKeys[i].Values["UserID"].ToInt();
                classStudent.StudentSignupID=CustomGridView1.DataKeys[i].Values["StudentSignupID"].ToGuid();
                //classStudent.IsDuty = "0";
                classStudent.IsBamboo = false;
                classStudent.CreateTime = DateTime.Now;
                classStudent.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                classStudent.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                classStudent.DelFlag = false;
                classStudent.ClassPostion = "普通学员";

                classStudentLogic.Save(classStudent);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InitialEntity();
            ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "保存成功！", "function(){window.location = '" + this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}", ClassID, TrainingItemID)) + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        this.upList.Update();
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}", ClassID, TrainingItemID)));
    }

    #region 机构 部门 岗位

    protected void BindOrganizationList()
    {
        ddlOrg.DataSource = classLogic.GetTrainingItemOrganizationList(TrainingItemID);
        ddlOrg.DataTextField = "DisplayPath";
        ddlOrg.DataValueField = "OrganizationID";
        ddlOrg.DataBind();
    }
    /// <summary>
    /// 机构选中事件
    /// </summary>
    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddlOrg.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddlDepartment.DataSource = dt;
        this.ddlDepartment.DataTextField = "ColumnNameValue";
        this.ddlDepartment.DataValueField = "ColumnCodeValue";
        this.ddlDepartment.DataBind();
        this.ddlDepartment.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddlDepartment.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddlPost.DataSource = dt;
        this.ddlPost.DataTextField = "ColumnNameValue";
        this.ddlPost.DataValueField = "ColumnCodeValue";
        this.ddlPost.DataBind();
        this.ddlPost.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddlPost.SelectedIndex = 0;
    }
    #endregion
}