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


public partial class Point_PointReasonTypeList : System.Web.UI.Page
{
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binding();
        }
    }
    private void binding()
    {
        int total=0;
        DataTable dt=pointReasonTypeLogic.GetPagedList(1,int.MaxValue-1,string.Empty,string.Format(" and OrgID={0}",ETMS.AppContext.UserContext.Current.OrganizationID),out total);
        this.GridViewList.DataSource = dt.DefaultView;
        this.GridViewList.DataBind();
    }
    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //编辑
            if (e.CommandName == "del")
            {

                pointReasonTypeLogic.doRemove(e.CommandArgument.ToInt());
                ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
                binding();

            }           
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        binding();
    }
}