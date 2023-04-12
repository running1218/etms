using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;

public partial class Point_CoursePointManager_Controls_UnPublishedStudentListView : System.Web.UI.UserControl
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
    #endregion
    StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
    string criteria = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            ddl_Organization_SelectedIndexChanged(sender, e);//触发Selected事件
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();

            }
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            }
            DataTable dt = pointLogic.GetCanIssueStudentCoursePointListByTrainingItemCourseID(TrainingItemCourseID, 1, 10, "", criteria, out totalRecordCount);
            if (dt.Rows.Count > 0)
            {
                this.lbl_ItemName.Text = dt.Rows[0]["ItemName"].ToString();
                this.lbl_CourseName.Text = dt.Rows[0]["CourseName"].ToString();
                this.lbl_CourseHours.Text = dt.Rows[0]["CourseHours"].ToString();
                this.lblCourseAttrID.FieldIDValue = dt.Rows[0]["CourseAttrID"].ToString();
            }
            this.PageSet1.QueryChange();
        }
    }
    protected void ddl_Organization_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        //按学员姓名排序
        string sortExpression = "u.RealName";
        if (this.ddl_OrganizationID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.OrganizationID={0}", this.ddl_OrganizationID.SelectedValue);
        }
        if (this.ddl_DepartmentID.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And u.DepartmentID={0}", this.ddl_DepartmentID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_RealName.Text))
        {
            criteria += string.Format(" And u.RealName like '%{0}%'", this.txt_RealName.Text.Trim());
        }
        if (this.ddl_AccessPointsMode.SelectedItem.Text != "全部")
        {
            criteria += string.Format(" And a.AccessPointsMode={0}", this.ddl_AccessPointsMode.SelectedValue);
        }
        DataTable dt = pointLogic.GetCanIssueStudentCoursePointListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Tr_ItemCourseResLogic tr_ItemCourseResLogic = new Tr_ItemCourseResLogic();
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[2].Visible = false;
        }
    }
    /// <summary>
    /// 获得积分方式
    /// </summary>
    /// <param name="AccessPointsMode"></param>
    /// <returns></returns>
    protected string GetAccessPointModeValue(string AccessPointsMode)
    {
        string PointsMode = string.Empty;
        switch (AccessPointsMode)
        {
            case "1":
                PointsMode = "系统计算";
                break;
            case "2":
                PointsMode = "手动设置";
                break;
        }
        return PointsMode;
    }
}