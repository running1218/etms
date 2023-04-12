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

using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_User_View : BasePage
{
    private static UserLogic Logic = new UserLogic();
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
            * ��Ҫ��֤��ҳ�����������
            *    ������  ������Χ
            * 1��  id    {0,1,2..}
            * 2��  op    {add,edit,delete,view}
            */
            return new RequestParameter[]
            {
                 RequestParameter.CreateRangeRequestParameter("id",RequestParameter.NaturalInt32RangeVerify)
                ,RequestParameter.CreateRangeRequestParameter("op",RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
            };
        }
    }

    /// <summary>
    /// ��ͼ��Ӧ�Ĳ���ģʽ������ҳ����ʾ��
    /// </summary>
    protected string Operator
    {
        get
        {
            string op = "";
            switch (Request.QueryString["op"].ToLower())
            {
                case "add":
                    op = "���";
                    break;
                case "edit":
                    op = "�޸�";
                    break;
                case "delete":
                    op = "ɾ��";
                    break;
                case "view":
                    op = "�鿴";
                    break;
            }
            return op;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //���ݲ���������ʾ��ͬ��������
            string sOperType = Request.QueryString["op"];
            switch (sOperType)
            {
                case "add":
                    this.UserControl1.BindFromData(new User(), ViewMode.Edit);
                    break;
                case "edit":
                    this.UserControl1.BindFromData(Logic.GetUserByID(int.Parse(Request.QueryString["id"])), ViewMode.Edit);
                    break;
                case "view":
                    this.UserControl1.BindFromData(Logic.GetUserByID(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
                    break;
                case "delete":
                    this.UserControl1.BindFromData(Logic.GetUserByID(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
                    break;
            }
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        //btn��ʾ����
        string sOperType = Request.QueryString["op"];
        this.btnAdd.Visible = false;
        this.btnUpdate.Visible = false;
        this.btnDelete.Visible = false;
        switch (sOperType)
        {
            case "add":
                this.btnAdd.Visible = true;
                break;
            case "edit":
                this.btnUpdate.Visible = true;
                break;
            case "view":
                this.btnReturn.Text = "�ر�";
                break;
            case "delete":
                this.btnDelete.Visible = true;
                break;
        }
        base.OnPreRender(e);
    }
    protected void btn_ClickHandle(object sender, EventArgs e)
    {
        string sOperType = ((Button)sender).CommandName;
        User entity = null;
        switch (sOperType)
        {
            case "add":
                entity = (User)this.UserControl1.DomainModel;
                Logic.Save(entity);
                break;
            case "edit":
                entity = (User)this.UserControl1.DomainModel;
                Logic.Save(entity);
                break;
            case "delete":
                Logic.Remove(new User() { UserID = int.Parse(Request.QueryString["id"]) });
                break;
        }
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(Operator + "�û���Ϣ�ɹ���");

    }
}

