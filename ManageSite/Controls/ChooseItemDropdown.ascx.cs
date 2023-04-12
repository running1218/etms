using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using System.Data;

public partial class Controls_ChooseItemDropdown : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    //课件编号
    public String ItemName
    {
        get
        {
            if (ViewState["ItemName"] == null)
                ViewState["ItemName"] = "";
            return (String)ViewState["ItemName"];
        }
        set
        {
            ViewState["ItemName"] = value;
        }
    }
    public Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = new Guid();
            return (Guid)ViewState["TrainingItemID"];
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    #endregion

    private string ScriptKey
    {
        get
        {
            return "ListData";
        }
    }

    private string Script
    {
        get
        {
            return @"<script language='javascript' type='text/javascript'>
                        {0}
                    </script>";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ScriptKey))
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ScriptKey, string.Format(this.Script, ListData()));
        }
        if (!IsPostBack)
        {
            txtSearch.Text = ItemName;
            txtItemID.Value = TrainingItemID.ToString();
        }

        txtSearch.Attributes.Add("onkeyup", "searchList()");
    }

    private string ListData()
    {
        string title = "var arrListData = new Array();arrListData = [";
        string data = string.Format(",['{0}','{1}']", Guid.NewGuid().ToString(), "请选择项目后查询");
        Tr_ItemLogic ItemLogic = new Tr_ItemLogic();
        int totalRecords = 0;
        string criteria = " AND OrgID=" + ETMS.AppContext.UserContext.Current.OrganizationID;
        DataTable tab = ItemLogic.GetPagedList(1, int.MaxValue - 1,"",criteria, out totalRecords);

        foreach (DataRow row in tab.Rows)
        {
            data += string.Format(",['{0}','{1}']", row["TrainingItemID"].ToString(), row["ItemName"].ToString());
        }

        return data.Length > 0 ? string.Format("{0}{1}];", title, data.Substring(1)) : string.Format("{0}];", title);
    }

    public Guid getItemID()
    {
        if (string.IsNullOrEmpty(txtItemID.Value))
            return Guid.NewGuid();
        else
            return new Guid(txtItemID.Value);
    }
}