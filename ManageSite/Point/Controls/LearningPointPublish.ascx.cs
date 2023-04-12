using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;

public partial class Point_Controls_LearningPointPublish : System.Web.UI.UserControl
{
    public static Point_Student_IssueDetailLogic IssueDetailLogic = new Point_Student_IssueDetailLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            ddl_u999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            ddl_Organization_SelectedIndexChanged(sender, e);//触发Selected事件
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
            }
            this.PageSet1.QueryChange();
            
        }
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.GridViewList.Columns[1].Visible = false;
        }
    }   

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {

        string crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        crieria += string.Format(" and c.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = IssueDetailLogic.GetStudentInputPointAllInfoListFromIssueDetail(pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
   
    protected void ddl_Organization_SelectedIndexChanged(object sender, EventArgs e)
    {
        //组织机构联动
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = this.ddl_u999OrganizationID.SelectedValue.ToInt();
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_u999DepartmentID.DataSource = dt;
        this.ddl_u999DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_u999DepartmentID.DataValueField = "ColumnCodeValue";
        this.ddl_u999DepartmentID.DataBind();
        this.ddl_u999DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_u999DepartmentID.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
}