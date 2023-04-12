using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ETMS.Controls;
using System.Text;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;
using ETMS.Utility;

public partial class QS_ResourceQueryAreaAdd : System.Web.UI.Page
{
    protected QS_QueryAreaLogic QueryAreaBiz = new QS_QueryAreaLogic();


    public string QueryID
    {
        get { return Request.QueryString["queryID"]; }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.OrgGridView, new IPageDataSource(getDataSource));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private IList getDataSource(int pageIndex, int pageSize, out int totalRecords)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(txtOrgCode.Text))
        {
            builder.AppendFormat(" AND OrganizationCode='{0}'", txtOrgCode.Text);
        }
        if (!string.IsNullOrWhiteSpace(txtOrgName.Text))
        {
            builder.AppendFormat(" AND OrganizationName like '%{0}%'", txtOrgCode.Text);
        }
        totalRecords = 0;
        System.Data.DataTable dt = QueryAreaBiz.GetNoSelectInfoByOrg(new Guid(QueryID), ETMS.AppContext.UserContext.Current.OrganizationID, int.Parse(this.ddl_OrganizationID.SelectedValue), pageIndex, 10, "OrganizationID", builder.ToString(), out totalRecords);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }


    protected void btnSearchOrgClick(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int[] orgIDList = CustomGridView.GetSelectedValues<int>(this.OrgGridView);
        string[] output = Array.ConvertAll<int, string>(orgIDList, i => i.ToString());

        if (orgIDList.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择调查范围机构！");
        }
        else
        {
            try
            {
                QueryAreaBiz.BatchAdd(new Guid(QueryID),EnumQueryAreaType.OrgStudent, output, ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "机构添加成功！", "function(){window.location = '" + this.ActionHref("ResourceQueryArea.aspx?QueryID=" + QueryID.ToString()) + "'}");

            }
            catch (Exception ex)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));

            }

        }
    }
}