using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
public partial class TraningImplement_CourseStudentManager_Controls_CourseNoSelect : System.Web.UI.UserControl
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
    /// 学员ID
    /// </summary>
    public int UserID
    {
        get
        {
            if (ViewState["UserID"] == null)
                ViewState["UserID"] = 0;

            return ViewState["UserID"].ToInt();
        }
        set
        {
            ViewState["UserID"] = value;
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
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //modify 2013-4-9 
        Guid[] selectedValues = null;//CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        List<Guid> ls = new List<Guid>();
        foreach (string p in CustomGridView1.AllCheckValues)
        {
            ls.Add(new Guid(p));
        }
        selectedValues = ls.ToArray();
       


        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的课程！");
            return;
        }
        else
         {
            try
            {
                //学员报名ID
                Guid StudentSignupID = ((HiddenField)CustomGridView1.Rows[0].FindControl("hfStudentSignupID")).Value.ToGuid();
                Sty_StudentCourseLogic courseLogic = new Sty_StudentCourseLogic();
                courseLogic.AddCourseSelectStudent(selectedValues, StudentSignupID, ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "课程添加成功！", "function(){window.location =window.location;}");
                //this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentSignupLogic.GetStudentNoSelectItemCourseListByTrainingItemID(UserID, TrainingItemID, pageIndex, pageSize, Crieria, out totalRecordCount);

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
            string TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
        }
    }
}