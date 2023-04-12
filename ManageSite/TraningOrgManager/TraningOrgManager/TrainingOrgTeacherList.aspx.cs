using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Teacher;

public partial class TraningOrgManager_TraningOrgManager_TrainingOrgTeacherList : System.Web.UI.Page
{
    public static Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();
    public static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();

    /// <summary>
    /// 外部培训机构ID
    /// </summary>
    public Guid OuterOrgID
    {
        get { return Request.QueryString["OuterOrgID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
        aBack.HRef = "TraningOrgList.aspx";
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = site_TeacherLogic.GetOuterTeacherList(ETMS.AppContext.UserContext.Current.OrganizationID, OuterOrgID, -1, -1, string.Empty,-1);
        totalRecordCount = dt.Rows.Count;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    private void Initial()
    {
        int total = 0;
        string strQuery = string.Format(" and OuterOrgID='{0}'", OuterOrgID);
        List<Tr_OuterOrg> source = outerOrgLogic.GetOuterOrgList(1, 1, " CreateTime desc", strQuery, out total);
        this.lblOuterOrgCode.Text = source[0].OuterOrgCode;
        this.lblOuterOrgName.Text = source[0].OuterOrgName;
        this.lblTeachNum.Text = source[0].TeacherNum.ToString();
        
    }
}