using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using System.Collections;
using System.Text;
using ETMS.Utility;
public partial class Poll_ResourceQuery_ResourceQueryAreaAdd : System.Web.UI.Page
{
    Poll_QueryAreaLogic PollQueryAreaBll = new Poll_QueryAreaLogic();


    public int QueryID
    {
        get { return int.Parse(Request.QueryString["queryID"]); }
    }

    public int PublishObjectID { get { return int.Parse(Request.QueryString["PublishObjectID"]); } }

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
        System.Data.DataTable dt = PollQueryAreaBll.GetNoSelectInfoByOrg(QueryID, ETMS.AppContext.UserContext.Current.OrganizationID, int.Parse(this.ddl_OrganizationID.SelectedValue), 1, 10, "OrganizationID", builder.ToString(), out totalRecords);

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
                PollQueryAreaBll.BatchAdd(QueryID, PublishObjectID, EnumQueryAreaType.OrgStudent, output, ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "机构添加成功！", "function(){window.location = '" + this.ActionHref("ResourceQueryArea.aspx?QueryID=" + QueryID.ToString()) + "'}");

            }
            catch (Exception ex)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));

            }

        }
    }

    protected void qq(object sender, EventArgs e)
    {
        //载入选中机构下部门，岗位数据
        int i = 0;
    }

}