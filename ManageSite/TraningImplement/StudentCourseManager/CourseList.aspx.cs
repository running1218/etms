using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_StudentCourseManager_CourseList : System.Web.UI.Page
{
    #region
    
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
            {
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID");
            }
            bind();
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        string Crieria = string.Format(" AND ItemStatus=20 AND OrgID={0} AND SignupModeID not in (1,2)", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_ItemName.DataSource = itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);
        ddl_ItemName.DataTextField = "ItemName";
        ddl_ItemName.DataValueField = "TrainingItemID";
        ddl_ItemName.DataBind();
        if (!string.IsNullOrEmpty(TrainingItemID))
            ddl_ItemName.SelectedValue = TrainingItemID;

        IsVisibleBtn(ddl_ItemName.SelectedValue.ToGuid());
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        labProjectStudentTotal.Text = new Sty_StudentSignupLogic().GetTrainingItemStudentTotal(ddl_ItemName.SelectedValue.ToGuid()).ToString();
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(ddl_ItemName.SelectedValue.ToGuid(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        IsVisibleBtn(ddl_ItemName.SelectedValue.ToGuid());
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid TrainingItemCourseID =CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();

            //已报名学员数
            Label lbl_SignUpStudentTotal = (Label)e.Row.FindControl("lbl_SignUpStudentTotal");
            if (lbl_SignUpStudentTotal != null)
            {
                lbl_SignUpStudentTotal.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(TrainingItemCourseID).ToString();
            }

            //设置学员
            LinkButton lbtn_SetStudent = (LinkButton)e.Row.FindControl("lbtn_SetStudent");
            if (lbtn_SetStudent != null)
            {
                lbtn_SetStudent.PostBackUrl= this.ActionHref(string.Format("SetStudent.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
            }

            LinkButton lbtnAddAll2 = (LinkButton)e.Row.FindControl("lbtnAddAll2");
            lbtnAddAll2 = lbtnAddAll2 == null ? new LinkButton() : lbtnAddAll2;
            lbtnAddAll2.Attributes["onclick"] = string.Format("javascript:showWindow('添加全部学员','{0}',650,550);javascript:return false;", this.ActionHref(string.Format("StudentAddAll.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddAll")
        {
            if (labProjectStudentTotal.Text.ToInt() == 0) {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("该项目下没有学员！");
                return;
            }
            try
            {
                Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
                StudentCourseLogic.AddStudentSelectCourse(e.CommandArgument.ToGuid()
                    , ETMS.AppContext.UserContext.Current.UserID
                    , ETMS.AppContext.UserContext.Current.RealName);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("全部学员添加成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
            catch
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage("全部学员添加失败！");
            }
        }
    }

    /// <summary>
    /// 如果允许学员报名 隐藏项目学员开放所有课程与关闭所有课程
    /// </summary>
    private void IsVisibleBtn(Guid trainingItemID)
    {
        //bool isAllowSignup = true;
        //if (trainingItemID != Guid.Empty)
        //{
        //    isAllowSignup = new Tr_ItemLogic().GetById(trainingItemID).IsAllowSignup;
        //}
        //cbtnOpenAllCourse.Visible = cbtnCloseAllCourse.Visible = !isAllowSignup;
    }

    /// <summary>
    /// 为项目所有学员开放所有课程
    /// </summary>
    protected void cbtnOpenAllCourse_Click(object sender, EventArgs e) {
        int totalRecordCount = 0;
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(ddl_ItemName.SelectedValue.ToGuid(), 1, int.MaxValue, out totalRecordCount);
        foreach (DataRow row in dt.Rows)
        {
            Sty_StudentCourseLogic StudentCourseLogic = new Sty_StudentCourseLogic();
            StudentCourseLogic.AddStudentSelectCourse(row["TrainingItemCourseID"].ToGuid()
                , ETMS.AppContext.UserContext.Current.UserID
                , ETMS.AppContext.UserContext.Current.RealName);
        }
        ETMS.WebApp.Manage.Extention.SuccessMessageBox("项目所有学员开放所有课程成功！");
        this.PageSet1.DataBind();
    }
        
    /// <summary>
    /// 为项目所有学员开放所有课程
    /// </summary>
    protected void cbtnCloseAllCourse_Click(object sender, EventArgs e)
    {
        new Sty_StudentCourseLogic().DeleteStudentCourseByTrainingItemID(ddl_ItemName.SelectedValue.ToGuid());

        ETMS.WebApp.Manage.Extention.SuccessMessageBox("项目课程下所有学员清除成功！");
        this.PageSet1.DataBind();
    }
}