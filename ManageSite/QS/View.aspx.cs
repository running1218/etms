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
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;

public partial class Poll_ResourceQuery_View : BasePage
{
    private static QS_QueryLogic LogicBiz = new QS_QueryLogic();
    private QS_Query queryEntity = new QS_Query();
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
                 RequestParameter.CreateRangeRequestParameter("op",RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
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

    protected int PollType
    {
        get
        {
            return int.Parse(Request.QueryString["PollTypeID"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Guid id = Guid.Parse(Request.Params["id"]);

        if (!IsPostBack)
        {
            //根据操作类型显示不同操作界面
            string sOperType = Request.QueryString["op"];
            switch (sOperType)
            {
                case "add":
                    this.UserControl1.BindFromData(queryEntity, ViewMode.Edit);
                    break;
                case "edit":
                    this.UserControl1.BindFromData(LogicBiz.GetById(new Guid(Request.Params["id"])), ViewMode.Edit);
                    break;
                case "view":
                    this.UserControl1.BindFromData(LogicBiz.GetById(new Guid(Request.Params["id"])), ViewMode.Browse);
                    break;
                case "delete":
                    this.UserControl1.BindFromData(LogicBiz.GetById(new Guid(Request.Params["id"])), ViewMode.Browse);
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
                    queryEntity = (QS_Query)this.UserControl1.DomainModel;
                    //设置所属机构
                    queryEntity.QueryID = Guid.NewGuid();
                    queryEntity.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                    queryEntity.CreateTime = DateTime.Now;
                    queryEntity.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                    queryEntity.ModifyTime = DateTime.Now;
                    queryEntity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    queryEntity.PollTypeID = PollType;
                    LogicBiz.Save(queryEntity, ETMS.AppContext.OperationAction.Add);
                    break;
                case "edit":
                    queryEntity = (QS_Query)this.UserControl1.DomainModel;
                    queryEntity.ModifyTime = DateTime.Now;
                    queryEntity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    LogicBiz.Save(queryEntity, ETMS.AppContext.OperationAction.Edit);
                    break;
                case "delete":
                    LogicBiz.Remove(new Guid(Request.Params["id"]));
                    break;
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(Operator + "问卷调查成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}

