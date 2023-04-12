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
using ETMS.Utility;
using ETMS.Components.Basic.Implement;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;

public partial class LearningManagement_ClassManager_SetsStudentList : ETMS.Controls.BasePage
{
    public static PublicFacade publicFacade = new PublicFacade();
    public static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    public static Sty_ClassStudent classStudent = new Sty_ClassStudent();
    public static Sty_ClassStudentLogic classStudentLogic = new Sty_ClassStudentLogic();

    //private static Guid[] classStudentIDs = new Guid[20];
    private static string classStudentIDStr = "";
    /// <summary>
    /// 班级ID
    /// </summary>
    public Guid ClassID
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
            this.PageSet1.QueryChange();
            InitialControlers();
        }
        btnAdd.PostBackUrl = this.ActionHref(string.Format("StudentAdd.aspx?ClassID={0}&TrainingItemID={1}", ClassID, TrainingItemID));
        //this.btnAgree.Attributes.Add("onclick", this.ActionHref(string.Format("javascript:showWindow('设置班委','SetsClassCommittee.aspx?ClassID={0}')", ClassID)));
        aBack.HRef = this.ActionHref(string.Format("ClassList.aspx?TrainingItemID={0}",TrainingItemID));
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[4].Visible = false;
        }
    }

    private void InitialControlers()
    {
        try
        {
            this.txtClassName.Text = classLogic.GetById(ClassID).ClassName;
            int total = 0;
            DataTable dt = classLogic.GetClassItemList(1, 1, string.Empty, string.Format(" and TrainingItemID='{0}'", TrainingItemID), out total);
            this.txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
        }
        catch { }
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBox("提示", "设置成功！", "function(){location.href=location.href;}");
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //string whereStr = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        classStudent = new Sty_ClassStudent();       
        classStudent.WorkerNo = this.txtWorkerNo.Text.Trim();       
        classStudent.RealName = this.txtRealName.Text.Trim();       
        classStudent.StudentTypeID = this.ddlStudentType.SelectedValue.ToInt();       
        List<Sty_ClassStudent> classStudentList = classLogic.GetClassStudentList(ClassID, classStudent, pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(classStudentList, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            Sty_ClassStudent entity = (Sty_ClassStudent)e.Row.DataItem;

            Label lblPostion = (Label)e.Row.FindControl("lblPostion");
            if (!string.IsNullOrEmpty(entity.ClassPostion))
            {
                lblPostion.Text = entity.ClassPostion;
            }
            else
            {
                lblPostion.Text = "普通学员";
            }

            LinkButton btnPostion = (LinkButton)e.Row.FindControl("btnPostion");
            btnPostion.Attributes.Add("onclick", string.Format("javascript:showWindow('设置班委','{0}',500,300);javascript: return false;", this.ActionHref(string.Format("SetsClassCommittee.aspx?ClassID={0}&ClassStudentID={1}&UserID={2}", ClassID, entity.ClassStudentID, entity.UserID))));

        }
    }

    /// <summary>
    /// 获取列表中选中的删除用
    /// </summary>
    public int InitialGuidGroup()
    {
        int j = 0;
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                classStudentIDStr += CustomGridView1.DataKeys[i].Values["ClassStudentID"].ToGuid().ToString() + "_";
                j++;
            }
        }
        return j;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }


    protected void cbtnDel_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (selectValue.Length > 0)
            {
                classStudentLogic.Delete(selectValue);
                this.PageSet1.DataBind();
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请选择学员");
            }           
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

    protected void btnAgree_Click(object sender, EventArgs e)
    {
        Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectValue.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "setclassCommittee", string.Format("<script language=javascript>showWindow('设置班委','{0}')</script>", this.ActionHref(string.Format("SetsClassCommittee.aspx?ClassID={0}&ClassStudentID={1}", ClassID, classStudentIDStr))));
            return;
        }
        else
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择学员");
        }

    }
    protected void btnBanboo_Click(object sender, EventArgs e)
    {       
        try
        {
            Guid[] selectValue = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (selectValue.Length > 0)
            {
                for(int i=0;i<selectValue.Length;i++)
                {
                    classStudent = classStudentLogic.GetById(selectValue[i]);
                    classStudent.IsBamboo = true;
                    classStudentLogic.Save(classStudent);
                }
                    ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("版主设置成功！");
                this.PageSet1.DataBind();
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请选择学员");
            }                   
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

}