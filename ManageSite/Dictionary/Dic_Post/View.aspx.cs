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
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
public partial class Dic_Post_View : BasePage
{
    private static Dic_PostLogic Logic = new Dic_PostLogic();
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
            * 需要验证的页面参数包含：
            *    参数名  参数范围
            * 1、  id    {0,1,2..}
            * 2、  op    {add,edit,delete,view}
            */
            return new RequestParameter[]
            {
                 RequestParameter.CreateRangeRequestParameter("id",RequestParameter.NaturalInt32RangeVerify)
                ,RequestParameter.CreateRangeRequestParameter("op",RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
            };
        }
    }

    /// <summary>
    /// 视图对应的操作模式，用于页面提示。
    /// </summary>
    protected string Operator
    {
        get
        {
            string op = "";
            switch (Request.QueryString["op"].ToLower())
            {
                case "add":
                    op = "添加";
                    break;
                case "edit":
                    op = "修改";
                    break;
                case "delete":
                    op = "删除";
                    break;
                case "view":
                    op = "查看";
                    break;
            }
            return op;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //根据操作类型显示不同操作界面
            string sOperType = Request.QueryString["op"];
            switch (sOperType)
            {
                case "add":
                    this.UserControl1.BindFromData(new Dic_Post(), ViewMode.Edit);
                    break;
                case "edit":
                    this.UserControl1.BindFromData(Logic.GetById(int.Parse(Request.QueryString["id"])), ViewMode.Edit);
                    break;
                case "view":
                    this.UserControl1.BindFromData(Logic.GetById(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
                    break;
                case "delete":
                    this.UserControl1.BindFromData(Logic.GetById(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
                    break;
            }
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        //btn显示控制
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
                this.btnReturn.Text = "关闭";
                break;
            case "delete":
                this.btnDelete.Visible = true;
                break;
        }
        base.OnPreRender(e);
    }
    protected void btn_ClickHandle(object sender, EventArgs e)
    {
        try
        {
            string sOperType = ((Button)sender).CommandName;
            switch (sOperType)
            {
                case "add":
                case "edit":
                    Dic_Post entity = (Dic_Post)this.UserControl1.DomainModel;
                    if (entity.PostID > 0)
                    {
                        Logic.Save(entity);
                    }
                    else
                    {
                        //增加时设置所属机构
                        entity.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                        Logic.Add(entity);
                    }
                    break;
                case "delete":
                    Logic.Remove(int.Parse(Request.QueryString["id"]));
                    break;
            }

            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(Operator + "成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}

