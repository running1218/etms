using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using System.Collections;
using System.Text;

public partial class Grade_GradeManage_Controls_GradeEntry : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// 操作类型 1 Add 2 Edit 3 View
    /// </summary>
    public Int32 Operation
    {
        get
        {
            if (ViewState["Operation"] == null)
            {
                ViewState["Operation"] = 1;
            }
            return (Int32)ViewState["Operation"];
        }
        set
        {
            ViewState["Operation"] = value;
        }
    }

    #endregion

    private static Sty_StudentOffLineJob studentJobRecord = new Sty_StudentOffLineJob();
    public static PublicFacade publicFaced = new PublicFacade();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Tr_Item item = new Tr_Item();
    private static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    private static Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();
    private static Hashtable gradeHashtable = new Hashtable();

    /// <summary>
    /// 培训项目编号
    /// </summary>
    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }

    /// <summary>
    /// 课程编号
    /// </summary>
    private Guid CourseID
    {
        get { return Request.QueryString["CourseID"].ToGuid(); }
    }

    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    private Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    private int PublishView
    {
        get
        {
            return Request.ToparamValue<int>("view");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[3].Visible = false;
        }
    }


    protected void Initial()
    {
        this.lblCourseName.Text = publicFaced.GetCourseNameByID(CourseID);
        item = itemLogic.GetById(TrainingItemID);
        this.lblItemName.Text = item.ItemName;
        this.lblItemCode.Text = item.ItemCode;
        int total = 0;
        DataTable dt = itemCourseLogic.GetGradeIssueList(1, 1, null, string.Format(" and Tr_ItemCourse.TrainingItemCourseID='{0}'", TrainingItemCourseID.ToString()), out total);
        this.lblCourseCode.Text = dt.Rows[0]["CourseCode"].ToString();
        this.lblCourseName.Text = dt.Rows[0]["CourseName"].ToString();
        this.lblTeachModel.FieldIDValue = dt.Rows[0]["TeachModelID"].ToString();
        this.lblStudentNum.Text = studentcourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();
    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        DataTable dt = itemCourseLogic.GetItemCourseStudentScoreList(TrainingItemCourseID, pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", whereQuery.ToString(), out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender,EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton lblView = e.Row.FindControl("lblView") as LinkButton;
            string pathStr = this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeView.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}&UserID={4}&StudentCourse={5}", WebUtility.AppPath, TrainingItemCourseID, CourseID, TrainingItemID,drv["UserID"].ToInt(), drv["StudentCourse"].ToGuid()));
            lblView.Attributes.Add("href", string.Format("javascript:showWindow('成绩查询','{0}')", pathStr));
            TextBox txtSumGrade = e.Row.FindControl("txtSumGrade") as TextBox;
            if (string.IsNullOrEmpty(drv["Remark"].ToString()) && drv["SumGrade"].ToString().Split('.')[0].ToInt() == 0)
            {
                txtSumGrade.Text = "";
            }
            else
            {
                txtSumGrade.Text = drv["SumGrade"].ToString().Split('.')[0].ToInt().ToString();
            }
            txtSumGrade.Attributes.Add("onchange", "checkTextValue(this);");           
            
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Guid[] trainitemIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (trainitemIDs.Length < 1)
        {
            JsUtility.AlertMessageBox("请选择学员！");
        }
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                TextBox txtGradeGrid=(TextBox)CustomGridView1.Rows[i].FindControl("txtSumGrade");
                switch (((Button)sender).CommandName)
                {
                    case "input":
                        txtGradeGrid.Text = this.txtInputGrade.Text.Trim();
                        //txtGradeGrid.Attributes.Add("t_value", txtGradeGrid.Text);
                        break;
                }                
            }
        }
    }

    private int InitialHashTable()
    {
        int inputRow =0;
        gradeHashtable.Clear();
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            //CheckBox chkSelect = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            //if (chkSelect.Checked)
            //{
                TextBox txtGrade=(TextBox)CustomGridView1.Rows[i].FindControl("txtSumGrade");
                
                gradeHashtable.Add(CustomGridView1.DataKeys[i].Values["StudentCourse"].ToString(), txtGrade.Text);
                //判断是不是有录入
                if (string.IsNullOrEmpty(txtGrade.Text.Trim()))
                {
                    inputRow++;
                }

            //}
        }
        return inputRow;
        
    }   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //判断是不是有录入
            int inputrow= InitialHashTable();
            if (inputrow == this.CustomGridView1.Rows.Count)
            {
                JsUtility.AlertMessageBox("请录入成绩！");
                return;
            }
            studentcourseLogic.BatchSetGrade(gradeHashtable,ETMS.AppContext.UserContext.Current.RealName);
            //itemCourseLogic.Tr_ItemCourse_GradeIssue(TrainingItemCourseID, 1, ETMS.AppContext.UserContext.Current.RealName);
            ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("保存成功");
            this.PageSet1.DataBind();
            //upList.Update();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
       
    }   
   
}