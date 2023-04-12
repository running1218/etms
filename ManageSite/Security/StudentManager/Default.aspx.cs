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
                    ETMS.Utility.JsUtility.SuccessMessageBox("����ѧԱ״̬�ɹ���");
                    break;
                case "Reset":
                    AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
                    AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
                    userLogic.ResetPassword(userID, ETMS.AppContext.UserContext.Current.RealName, strategy.Security_PassWord_Default);//�������ݿ�
                    
                    ETMS.Utility.JsUtility.SuccessMessageBox("ѧԱ�������óɹ���");
                    break;
                case "Remove":
                    Logic.Remove(userID);
                    ETMS.Utility.JsUtility.SuccessMessageBox("ѧԱɾ���ɹ���");
                    break;
            }
            //ˢ��
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
            ETMS.Utility.JsUtility.AlertMessageBox("���ٹ�ѡһ��ѧԱ��");
            return;
        }
        Logic.Remove(selectedValues);
        //ˢ������
        this.PageSet1.DataBind();
        ETMS.Utility.JsUtility.SuccessMessageBox("����������������ɹ���");

    }
   
    
 
    // GridView����Excel
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(GridViewList.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
  
    // ѧԱ�������ݵ���
    protected void btnExport_Click(object sender, EventArgs e)
    { 
        ExcelDataAccess EDA = new ExcelDataAccess();
        //copy template.xls��temp�ļ���
        string xlsFileName = "ѧԱ����.xls";
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //����excel
        System.IO.File.Copy(Server.MapPath("~/Security/StudentManager/Template/ѧԱ����.xls"), fullXlsFilePath, true);
        //���������ļ�
        EDA.SaveExcel(GetImportTemplateList(), "", fullXlsFilePath, "sheet1");
        //Excel����
        FileDownLoadUtility.ExportFile(fullXlsFilePath);

    }

    /// <summary>
    /// ���쵼��ģ��ĳ�ʼ����
    /// </summary>
    private DataTable GetImportTemplateList()
    {
        int pageSize = int.MaxValue - 1;
        int totalRecordCount = 0;
        DataTable tab = getDataSource1DataTable(1, pageSize, out totalRecordCount);

        DataTable newTab = new DataTable();
        newTab.Columns.Add("ѧԱ�˺�");
        newTab.Columns.Add("ѧԱ����");         
        newTab.Columns.Add("ѧԱ���");
        newTab.Columns.Add("����");
        newTab.Columns.Add("����");
        newTab.Columns.Add("�ֻ�");
        newTab.Columns.Add("�Ա�");
        newTab.Columns.Add("���֤��");
        newTab.Columns.Add("����ְ��");
        newTab.Columns.Add("ֱ���ϼ�");
        newTab.Columns.Add("��������", typeof(string));
        newTab.Columns.Add("�绰");
        newTab.Columns.Add("���ѧ��");
        newTab.Columns.Add("רҵ");
        newTab.Columns.Add("��ְ����", typeof(string));
        newTab.Columns.Add("״̬");

        foreach (DataRow row in tab.Rows)
        {
            DataRow newRow = newTab.NewRow();
            newRow["ѧԱ�˺�"] = row["LoginName"];
            newRow["ѧԱ����"] = row["RealName"];                        
            newRow["ѧԱ���"] = row["ResettlementWayName"];
            newRow["����"] = row["WorkerNo"];
            newRow["����"] = row["Email"];
            newRow["�ֻ�"] = row["MobilePhone"];
            newRow["�Ա�"] = row["SexTypeName"];
            newRow["���֤��"] = row["Identity"];
            newRow["����ְ��"] = row["TitleName"];
            newRow["ֱ���ϼ�"] = row["Superior"];
            newRow["��������"] = string.IsNullOrEmpty(row["Birthday"].ToString()) ? string.Empty : row["Birthday"].ToDate();
            newRow["�绰"] = row["Telphone"];
            newRow["���ѧ��"] = row["LastEducation"];
            newRow["רҵ"] = row["Specialty"];
            newRow["��ְ����"] = string.IsNullOrEmpty(row["JoinTime"].ToString()) ? string.Empty : row["JoinTime"].ToDate(); ;
            string strSaatus = row["Status"].ToString();
            newRow["״̬"] = strSaatus == "1" ? "����" : "����";
            newTab.Rows.Add(newRow);
        }
        return newTab;
    }

}

