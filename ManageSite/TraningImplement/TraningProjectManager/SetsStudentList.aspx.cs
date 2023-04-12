using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.API.Entity.Security;

public partial class TraningImplement_TraningProjectManager_SetsStudentList : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null) {
                ViewState["Crieria"] = "";
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_Site_User999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }

            this.PageSet1.QueryChange();
        }
        this.lbtnAdd.PostBackUrl = this.ActionHref(string.Format("SetsStudentAdd.aspx?TrainingItemID={0}", TrainingItemID));
        this.lbtnImport.Attributes["onclick"] = string.Format("javascript:showWindow('导入学员','{0}',500,350);javascript:return false;", this.ActionHref("ImportStudent.aspx?TrainingItemID="+TrainingItemID ));
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;
            //如果项目发布后不能删除
            if (item.IsIssue)
            {
                cbtnDel.Enabled = false;
                cbtnDel.CssClass = "btn_Del_eanbled";
                lbtnImport.Enabled = false;
                lbtnImport.CssClass = "btn_Import_enabled";
            }
            //邦定组织机构信息
            ddl_Site_User999OrganizationID.DataSource = new Sty_StudentSignupLogic().GetTrainingItemOrganizationList(TrainingItemID);
            ddl_Site_User999OrganizationID.DataTextField = "DisplayPath";
            ddl_Site_User999OrganizationID.DataValueField = "OrganizationID";
            ddl_Site_User999OrganizationID.DataBind();
        }
    }
    
    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {        
        string orgs = " AND Site_User.OrganizationID in (";
        foreach (ListItem item in ddl_Site_User999OrganizationID.Items)
        {
            if (item.Value.Trim() != "" && item.Value.Trim() != "-1")
                orgs += item.Value + ",";
        }
        orgs += "-1)";

        Crieria = string.Format(" {0} {1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), orgs);

        DataTable tab = new Sty_StudentSignupLogic().GetStudentListALLByTrainingItemID(TrainingItemID, pageIndex, pageSize, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(tab, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    
    /// <summary>
    /// 删除学员关系信息
    /// </summary>
    protected void cbtnDel_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除的学员！");
            return;
        }
        else
        {
            try
            {
                Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
                StudentSignupLogic.Remove(selectedValues);
                ETMS.Utility.JsUtility.SuccessMessageBox("信息删除成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
    }       

    /// <summary>
    /// 导出学员信息
    /// </summary>
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        ExcelDataAccess EDA = new ExcelDataAccess();
        //copy template.xls到temp文件夹
        string xlsFileName = string.Format("培训项目“{0}”学员.xls", lblItemName.Text);
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/TraningImplement/TraningProjectManager/Template/导出培训项目学员模板.xls"), fullXlsFilePath, true);
        //保存物理文件
        EDA.SaveExcel(GetImportTemplateList(), "", fullXlsFilePath, "sheet1");
        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }

    /// <summary>
    /// 制造导入模板的初始数据
    /// </summary>
    private DataTable GetImportTemplateList()
    {
        //邦定列表数据
        int total = 0;
        int pageIndex = 1;
        int pageSize = int.MaxValue - 1;

        DataTable tab = new Sty_StudentSignupLogic().GetStudentListALLByTrainingItemID(TrainingItemID, pageIndex, pageSize, Crieria, out total);
        
        DataTable newTab = new DataTable();
        newTab.Columns.Add("项目编码");
        newTab.Columns.Add("项目名称");
        newTab.Columns.Add("机构编码");
        newTab.Columns.Add("机构名称");
        newTab.Columns.Add("学员账号");
        newTab.Columns.Add("学员姓名");
        //newTab.Columns.Add("部门");
        //newTab.Columns.Add("岗位");
        //newTab.Columns.Add("职级");
        newTab.Columns.Add("邮箱");
        newTab.Columns.Add("手机");
        newTab.Columns.Add("身份证号", typeof(String));
        
        foreach(DataRow row in tab.Rows){
            //Node node = new OrganizationLogic().GetNodeByID(row["OrganizationID"].ToInt());
           // Organization org = (Organization)new OrganizationLogic().GetNodeByID(row["OrganizationID"].ToInt());
            DataRow newRow = newTab.NewRow();
            newRow["项目编码"] = lblItemCode.Text;
            newRow["项目名称"] = lblItemName.Text;
            newRow["机构编码"] = row["OrganizationCode"];
            newRow["机构名称"] = row["OrganizationName"];
            newRow["学员账号"] = row["LoginName"];
            newRow["学员姓名"] = row["RealName"];
            //newRow["部门"] = row["DepartmentName"];
            //newRow["岗位"] = row["PostName"];
            //newRow["职级"] = row["RankName"] ;
            newRow["邮箱"] = row["Email"];
            newRow["手机"] = row["MobilePhone"];
            newRow["身份证号"] = row["Identity"];
            newTab.Rows.Add(newRow);
        }
        return newTab;
    }

    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Status")
        {
            var siginupID = e.CommandArgument.ToGuid();
            var data = new Sty_StudentSignupLogic().GetById(siginupID);

            if (data.IsUse == 0)
                data.IsUse = 1;
            else
                data.IsUse = 0;

            try
            {
                new Sty_StudentSignupLogic().Update(data);
                ETMS.Utility.JsUtility.SuccessMessageBox("操作成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}