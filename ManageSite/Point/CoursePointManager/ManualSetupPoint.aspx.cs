using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using System.Text.RegularExpressions;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
public partial class Point_CoursePointManager_ManualSetupPoint : System.Web.UI.Page
{
    /// <summary>
    /// 项目课程IDs 
    /// </summary>
    protected Guid TrainingItemCourseID
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
    /// 项目课程名称
    /// </summary>
    protected string TrainingItemCourseName
    {
        get {
            if (ViewState["TrainingItemCourseName"] == null)
                ViewState["TrainingItemCourseName"] = string.Empty;
            return ViewState["TrainingItemCourseName"].ToString();
        }
        set
        {
            ViewState["TrainingItemCourseName"] = value;
        }
    }
    StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();

    private const int Mark = 2;//标识积分来源
    protected void Page_Load(object sender, EventArgs e)
    {
        StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
        string criteria = string.Format(" AND t.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        PageSet2.pageInit(this.CustomGridView2, NotPointPageDataSource);
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        int totalRecordCount = 0;
        if (!IsPostBack)
        {
            ddl_NotSet_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            ddl_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            ddl_Organization_SelectedIndexChanged(sender, e);//触发Selected事件
            ddl_NotSet_Organization_SelectedIndexChanged(sender, e);//触发Selected事件

            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.NotSettrOrg.Visible = false;
                this.trOrg.Visible = false;                
               
            }
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
                criteria += string.Format(" And tc.TrainingItemCourseID='{0}'", TrainingItemCourseID);
            }

            DataTable dt = pointLogic.GetCanComputePointCourseList(1, 10, "", criteria, out totalRecordCount);
            this.lbl_ItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lbl_CourseName.Text = dt.Rows[0]["CourseName"].ToString();
            TrainingItemCourseName = this.lbl_CourseName.Text;
            this.lbl_CourseHours.Text = dt.Rows[0]["CourseHours"].ToString();
            this.lblCourseAttrID.FieldIDValue = dt.Rows[0]["CourseAttrID"].ToString();
            this.PageSet2.QueryChange();
            this.PageSet1.QueryChange();            
        }
        lbtn_Return.PostBackUrl = "ProjectCourseList.aspx";
    }
    protected void ddl_NotSet_Organization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //组织机构联动
        //组织机构联动
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = this.ddl_NotSet_OrganizationID.SelectedValue.ToInt();
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_NotSet_DepartmentID.DataSource = dt;
        this.ddl_NotSet_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_NotSet_DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_NotSet_DepartmentID.DataBind();
        this.ddl_NotSet_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_NotSet_DepartmentID.SelectedIndex = 0;

    }
    protected void ddl_Organization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //组织机构联动
        //组织机构联动
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = this.ddl_OrganizationID.SelectedValue.ToInt();
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_DepartmentID.DataSource = dt;
        this.ddl_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_DepartmentID.DataBind();
        this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_DepartmentID.SelectedIndex = 0;

    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //当前组织机构下的数据
        string criteria = "";

        //按学员姓名、获得积分时间排序
        string sortExpression = "u.RealName,a.AccessPointsTime";
        DataTable dt = null;
        if (this.ddl_OrganizationID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.OrganizationID={0}", this.ddl_OrganizationID.SelectedValue);
        }
        if (this.ddl_DepartmentID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.DepartmentID={0}", this.ddl_DepartmentID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_StudentName.Text))
        {
            criteria += string.Format(" And u.RealName like  '%{0}%'", this.txt_StudentName.Text.Trim());
        }
        if (!string.IsNullOrEmpty(this.txt_beginAccessPoints.Text))
        {
            criteria += string.Format(" And a.AccessPoints>={0}", txt_beginAccessPoints.Text);
        }
        if (!string.IsNullOrEmpty(this.txt_endAccessPoints.Text))
        {
            criteria += string.Format(" And a.AccessPoints<={0}", txt_endAccessPoints.Text);
        }
        dt = pointLogic.GetNoIssueStudentCourseInputPointListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 未计算数据列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList NotPointPageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string criteria = "";
        //按学员姓名排序
        string sortExpression = "u.RealName";
        DataTable dt = null;
        if (this.ddl_NotSet_OrganizationID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.OrganizationID={0}", this.ddl_NotSet_OrganizationID.SelectedValue);
        }
        if (this.ddl_NotSet_DepartmentID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.DepartmentID={0}", this.ddl_NotSet_DepartmentID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_NotSet_StudentName.Text))
        {
            criteria += string.Format(" And u.RealName like '%{0}%'", this.txt_NotSet_StudentName.Text.Trim());
        }
        dt = pointLogic.GetNoStudentCoursePointListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void btnNotSetSeach_Click(object sender, EventArgs e)
    {
        this.PageSet2.QueryChange();
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       Regex regex=new Regex("^\\d+$");
       if (!string.IsNullOrEmpty(this.txt_beginAccessPoints.Text))
       {
           if (!regex.IsMatch(this.txt_beginAccessPoints.Text))
           {
               ETMS.Utility.JsUtility.AlertMessageBox("获得积分只可以输入正整数！");
               return;
           }
       }
        if(!string.IsNullOrEmpty(this.txt_endAccessPoints.Text))
        {
           if (!regex.IsMatch(this.txt_endAccessPoints.Text))
           {
               ETMS.Utility.JsUtility.AlertMessageBox("获得积分只可以输入正整数！");
               return;
           }
       }
        if (this.txt_beginAccessPoints.Text != "" && this.txt_endAccessPoints.Text != "")
        {
            if (Convert.ToInt32(this.txt_beginAccessPoints.Text) > Convert.ToInt32(this.txt_endAccessPoints.Text))
            {
                ETMS.Utility.JsUtility.AlertMessageBox("积分开始值不能大于积分结束值！");
                return;
            }
        }
       
        this.PageSet1.QueryChange();
    }
    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //查看明细
            LinkButton lblCoursePointRule = (LinkButton)e.Row.FindControl("lblCoursePointRule");
            lblCoursePointRule = lblCoursePointRule == null ? new LinkButton() : lblCoursePointRule;


            lblCoursePointRule.Enabled = true;
            lblCoursePointRule.PostBackUrl = this.ActionHref(string.Format("CoursePointRuleList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));

        }
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Tr_ItemCourseResLogic tr_ItemCourseResLogic = new Tr_ItemCourseResLogic();
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //查看明细
            Label lblLearningProgress = (Label)e.Row.FindControl("lblLearningProgress");
            Label lblUserID = (Label)e.Row.FindControl("lblUserID");
            if (lblLearningProgress.Text != null)
            {
                lblLearningProgress.Text = tr_ItemCourseResLogic.GetStudentCoursewareStudyProgressByUserID_TrainingItemCourseID(Convert.ToInt32(lblUserID.Text), TrainingItemCourseID).ToString() + "%";
            }
        }
    }
    /// <summary>
    /// 设置积分
    /// </summary>
    protected void btnPoint_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView2);
        if (StudentCourseIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要设置积分的学员！");
            return;
        }        
        else
        {

            try
            {
                int count = pointLogic.GetTrainingItemCourseHourseGivePoint(TrainingItemCourseID);
                if (count <= 0)
                {
                    ETMS.Utility.JsUtility.AlertMessageBox(string.Format("课程“{0}”的课时和对应的课程属性积分规则没有设置，不能计算积分！", TrainingItemCourseName));
                    return;
                }
                pointLogic.BatchSetStudentCoursePoints(TrainingItemCourseID,StudentCourseIDs, Mark, count, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "积分设置成功！", "");
                this.PageSet2.QueryChange();
                this.PageSet1.QueryChange();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 全部设置积分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllPoint_Click(object sender, EventArgs e)
    {
        int count = pointLogic.GetTrainingItemCourseHourseGivePoint(TrainingItemCourseID);
        if (count <= 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(string.Format("课程“{0}”的课时和对应的课程属性积分规则没有设置，不能计算积分！", TrainingItemCourseName));
            return;
        }
        else
        {
            try
            {
                int resultCount = pointLogic.ComputeCoursePointByTrainingItemCourseID(TrainingItemCourseID, Mark, count, ETMS.AppContext.UserContext.Current.RealName);
                if (resultCount > 0)
                {
                    ETMS.Utility.JsUtility.SuccessMessageBox("提示","积分设置成功！", "");                   
                }
                else
                {
                    ETMS.Utility.JsUtility.AlertMessageBox("提示", "抱歉，没有可以设置的积分列表！", "");
                }
                this.PageSet2.QueryChange();
                this.PageSet1.QueryChange();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
    /// <summary>
    /// 批量删除积分
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (StudentCourseIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除积分的学员！");
            return;
        }
        else
        {
            try
            {
                StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
                pointLogic.BatchDeleteStudentCoursePoints(StudentCourseIDs, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "积分删除成功！", "");
                this.PageSet1.QueryChange();
                this.PageSet2.QueryChange();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }

    }

    /// <summary>
    /// 全部删除积分
    /// </summary>
    protected void btnAllDelete_Click(object sender, EventArgs e)
    { 
            try
            {
                StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
               int resultCount= pointLogic.DeleteStudentCoursePointsByTrainingItemCourseID(TrainingItemCourseID, Mark, ETMS.AppContext.UserContext.Current.RealName);
               if (resultCount > 0)
               {
                   ETMS.Utility.JsUtility.SuccessMessageBox("提示","积分删除成功！", "");
               }
               else
               {
                   ETMS.Utility.JsUtility.AlertMessageBox("提示", "抱歉，没有可以删除的积分列表！", "");
               }
               this.PageSet2.QueryChange();
                this.PageSet1.QueryChange();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
    }
}