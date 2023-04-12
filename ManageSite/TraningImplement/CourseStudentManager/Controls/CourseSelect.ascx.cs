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

public partial class TraningImplement_CourseStudentManager_Controls_CourseSelect : System.Web.UI.UserControl
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
                ViewState["UserID"] =0;

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


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentSignupLogic.GetStudentCourseListByTrainingItemID(UserID, TrainingItemID, pageIndex, pageSize, "", Crieria, out totalRecordCount);

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

    /// <summary>
    /// 行操作
    /// </summary>
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