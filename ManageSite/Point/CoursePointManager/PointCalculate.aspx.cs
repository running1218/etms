using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using System.Text.RegularExpressions;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
public partial class Point_CoursePointManager_PointCalculate : System.Web.UI.Page
{
    #region 页面参数
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
        get
        {
            if (ViewState["TrainingItemCourseName"] == null)
                ViewState["TrainingItemCourseName"] = string.Empty;
            return ViewState["TrainingItemCourseName"].ToString();
        }
        set
        {
            ViewState["TrainingItemCourseName"] = value;
        }
    }
    #endregion
    StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
    private const int Mark = 1;//标识积分来源
    private Regex regex = new Regex("^\\d+$");
    protected void Page_Load(object sender, EventArgs e)
    {
        StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
        PageSet2.pageInit(this.CustomGridView2, NotPointPageDataSource);
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);       
        int totalRecordCount = 0;
        string criteria = "";

        if (!Page.IsPostBack)
        {
            ddl_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            ddl_NotCalculated_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            ddl_NotCalculated_Organization_SelectedIndexChanged(sender, e);//触发Selected事件
            ddl_Organization_SelectedIndexChanged(sender, e);//触发Selected事件
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                this.tr_NotCalculated_Org.Visible = false;              
            }
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
                criteria += string.Format(" And tc.TrainingItemCourseID='{0}'", TrainingItemCourseID);
            }

            DataTable dt = pointLogic.GetCanComputePointCourseList(1, 10, "", criteria, out totalRecordCount);
            this.lbl_ItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lbl_CourseName.Text = dt.Rows[0]["CourseName"].ToString();
            this.lbl_CourseHours.Text = dt.Rows[0]["CourseHours"].ToString();
            TrainingItemCourseName = this.lbl_CourseName.Text;
            this.lblCourseAttrID.FieldIDValue = dt.Rows[0]["CourseAttrID"].ToString();
            this.PageSet2.QueryChange();
            this.PageSet1.QueryChange();            

        }
        lbtn_Return.PostBackUrl = "ProjectCourseList.aspx";
    }

    protected void ddl_NotCalculated_Organization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //组织机构联动
        //组织机构联动
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = this.ddl_NotCalculated_OrganizationID.SelectedValue.ToInt();
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_NotCalculated_DepartmentID.DataSource = dt;
        this.ddl_NotCalculated_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_NotCalculated_DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_NotCalculated_DepartmentID.DataBind();
        this.ddl_NotCalculated_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_NotCalculated_DepartmentID.SelectedIndex = 0;

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
    /// <summary>
    /// 已计算数据列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
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
        dt = pointLogic.GetNoIssueStudentCourseComputePointListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);

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
        string crieria ="";
        //按学员姓名排序
        string sortExpression = "u.RealName";
        DataTable dt = null;
        if (this.ddl_OrganizationID.SelectedItem.Text != "全部")
        {
            crieria += string.Format(" And u.OrganizationID={0}",this.ddl_OrganizationID.SelectedValue);
        }
        if (this.ddl_DepartmentID.SelectedItem.Text != "全部")
        {
            crieria += string.Format(" And u.DepartmentID={0}",this.ddl_DepartmentID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_NotCalculated_StudentName.Text))
        {
            crieria += string.Format(" And u.RealName like '%{0}%'",this.txt_NotCalculated_StudentName.Text.Trim());
        }
        if (!string.IsNullOrEmpty(this.txt_Point.Text))
        {
            crieria += string.Format(" And a.SumGrade>={0}", this.txt_Point.Text);
        }
        dt = pointLogic.GetNoStudentCoursePointListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, sortExpression, crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 计算积分
    /// </summary>
    protected void btnPoint_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView2);
        if (StudentCourseIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要计算的积分学员！");
            return;
        }
        if (txt_Point.Text == "0")
        {
            ETMS.Utility.JsUtility.AlertMessageBox("获得积分成绩分数线必须大于零！");
            return;
        }          
             else
             {
                 try
                 {
                     int count = pointLogic.GetTrainingItemCourseHourseGivePoint(TrainingItemCourseID);
        if (count<=0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(string.Format("课程“{0}”的课时和对应的课程属性积分规则没有设置，不能计算积分！", TrainingItemCourseName));
            return;
        }
                     pointLogic.BatchSetStudentCoursePoints(TrainingItemCourseID, StudentCourseIDs, Mark, txt_Point.Text.ToInt(), ETMS.AppContext.UserContext.Current.RealName);
                     ETMS.Utility.JsUtility.SuccessMessageBox("提示", "积分计算成功！", "");
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
    /// 全部计算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllPoint_Click(object sender, EventArgs e)
    {

        if (txt_Point.Text == "0")
        {
            ETMS.Utility.JsUtility.AlertMessageBox("获得积分成绩分数线必须大于零！");
            return;
        }
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
               int resultCount= pointLogic.ComputeCoursePointByTrainingItemCourseID(TrainingItemCourseID, Mark, txt_Point.Text.ToInt(), ETMS.AppContext.UserContext.Current.RealName);
               if (resultCount > 0)
               {
                   ETMS.Utility.JsUtility.SuccessMessageBox("提示","积分计算成功！", "");
               }
               else
               {
                   ETMS.Utility.JsUtility.AlertMessageBox("提示", "抱歉，没有可以计算的积分列表！", "");
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
    /// 已计算查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(this.txt_beginAccessPoints.Text))
        {
            if (!regex.IsMatch(this.txt_beginAccessPoints.Text))
            {
                ETMS.Utility.JsUtility.AlertMessageBox("获得积分只可以输入正整数！");
                return;
            }
        }
        if (!string.IsNullOrEmpty(this.txt_endAccessPoints.Text))
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

    protected void btnScoreSeach_Click(object sender, EventArgs e)
    {
        if (!regex.IsMatch(this.txt_Point.Text))
        {
            ETMS.Utility.JsUtility.AlertMessageBox("成绩分数线只可以输入正整数！");
            return;
        }

        this.PageSet2.QueryChange();
    }

    /// <summary>
    /// 批量删除积分
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (StudentCourseIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除积分的课程！");
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
                int resultCount = pointLogic.DeleteStudentCoursePointsByTrainingItemCourseID(TrainingItemCourseID, Mark, ETMS.AppContext.UserContext.Current.RealName);
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
    
}