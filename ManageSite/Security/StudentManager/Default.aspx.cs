using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility.Data;
public partial class Admin_Site_Student_Default : BasePage
{
    UserLogic userLogic = new UserLogic();
    Site_StudentLogic Logic = new Site_StudentLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new ETMS.Controls.IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    protected void UserOpeator_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int userID = e.CommandArgument.ToInt();
            switch (e.CommandName)
            {
                case "SwitchStatus":
                    userLogic.SwitchUserStatus(userID, ETMS.AppContext.UserContext.Current.RealName);
                    ETMS.Utility.JsUtility.SuccessMessageBox("更改学员状态成功！");
                    break;
                case "Reset":
                    AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
                    AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
                    userLogic.ResetPassword(userID, ETMS.AppContext.UserContext.Current.RealName, strategy.Security_PassWord_Default);//基于数据库
                    
                    ETMS.Utility.JsUtility.SuccessMessageBox("学员密码重置成功！");
                    break;
                case "Remove":
                    Logic.Remove(userID);
                    ETMS.Utility.JsUtility.SuccessMessageBox("学员删除成功！");
                    break;
            }
            //刷新
            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        int rankID = -1;
        int postID = int.Parse(this.ddlResettlementWay.SelectedValue);
        int userStatus = this.ddl_UserStatus.SelectedValue.ToInt();
        DataTable dataList = Logic.GetCurrentOrgManagePagedList(this.txtLoginName.Text.Trim(), this.txtRealName.Text.Trim(), this.txtWorkNo.Text.Trim(),-1, rankID, postID, userStatus, pageIndex, pageSize, out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    private DataTable getDataSource1DataTable(int pageIndex, int pageSize, out int totalRecords)
    {
        int rankID = -1;
        int postID = int.Parse(this.ddlResettlementWay.SelectedValue);
        int userStatus = this.ddl_UserStatus.SelectedValue.ToInt();
        DataTable dataList = Logic.GetCurrentOrgManagePagedList(this.txtLoginName.Text.Trim(), this.txtRealName.Text.Trim(), this.txtWorkNo.Text.Trim(), -1, rankID, postID, userStatus, pageIndex, pageSize, out totalRecords);

        return dataList;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.GridViewList);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("至少勾选一个学员！");
            return;
        }
        Logic.Remove(selectedValues);
        //刷新数据
        this.PageSet1.DataBind();
        ETMS.Utility.JsUtility.SuccessMessageBox("批量重置密码操作成功！");

    }
   
    
 
    // GridView导出Excel
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(GridViewList.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
  
    // 学员管理数据导出
    protected void btnExport_Click(object sender, EventArgs e)
    { 
        ExcelDataAccess EDA = new ExcelDataAccess();
        //copy template.xls到temp文件夹
        string xlsFileName = "学员管理.xls";
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/Security/StudentManager/Template/学员管理.xls"), fullXlsFilePath, true);
        //保存物理文件
        EDA.SaveExcel(GetImportTemplateList(), "", fullXlsFilePath, "sheet1");
        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);

    }

    /// <summary>
    /// 制造导出模板的初始数据
    /// </summary>
    private DataTable GetImportTemplateList()
    {
        int pageSize = int.MaxValue - 1;
        int totalRecordCount = 0;
        DataTable tab = getDataSource1DataTable(1, pageSize, out totalRecordCount);

        DataTable newTab = new DataTable();
        newTab.Columns.Add("学员账号");
        newTab.Columns.Add("学员姓名");         
        newTab.Columns.Add("学员身份");
        newTab.Columns.Add("工号");
        newTab.Columns.Add("邮箱");
        newTab.Columns.Add("手机");
        newTab.Columns.Add("性别");
        newTab.Columns.Add("身份证号");
        newTab.Columns.Add("工作职务");
        newTab.Columns.Add("直接上级");
        newTab.Columns.Add("出生日期", typeof(string));
        newTab.Columns.Add("电话");
        newTab.Columns.Add("最高学历");
        newTab.Columns.Add("专业");
        newTab.Columns.Add("入职日期", typeof(string));
        newTab.Columns.Add("状态");

        foreach (DataRow row in tab.Rows)
        {
            DataRow newRow = newTab.NewRow();
            newRow["学员账号"] = row["LoginName"];
            newRow["学员姓名"] = row["RealName"];                        
            newRow["学员身份"] = row["ResettlementWayName"];
            newRow["工号"] = row["WorkerNo"];
            newRow["邮箱"] = row["Email"];
            newRow["手机"] = row["MobilePhone"];
            newRow["性别"] = row["SexTypeName"];
            newRow["身份证号"] = row["Identity"];
            newRow["工作职务"] = row["TitleName"];
            newRow["直接上级"] = row["Superior"];
            newRow["出生日期"] = string.IsNullOrEmpty(row["Birthday"].ToString()) ? string.Empty : row["Birthday"].ToDate();
            newRow["电话"] = row["Telphone"];
            newRow["最高学历"] = row["LastEducation"];
            newRow["专业"] = row["Specialty"];
            newRow["入职日期"] = string.IsNullOrEmpty(row["JoinTime"].ToString()) ? string.Empty : row["JoinTime"].ToDate(); ;
            string strSaatus = row["Status"].ToString();
            newRow["状态"] = strSaatus == "1" ? "启用" : "禁用";
            newTab.Rows.Add(newRow);
        }
        return newTab;
    }

}

