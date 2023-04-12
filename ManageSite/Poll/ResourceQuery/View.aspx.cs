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

public partial class Poll_ResourceQuery_View : BasePage
{
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
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
                    this.UserControl1.BindFromData(new Poll_Query(), ViewMode.Edit);
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
            Poll_Query entity = null;
            switch (sOperType)
            {
                case "add":
                    entity = (Poll_Query)this.UserControl1.DomainModel;
                    //设置所属机构
                    entity.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                    entity.CreateTime = DateTime.Now;
                    entity.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                    entity.ModifyTime = DateTime.Now;
                    entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    Logic.Save(entity);

                    //管理问卷与资源关系
                    Poll_QueryPublishObject queryPublishObject = new Poll_QueryPublishObject()
                    {
                        ResourceCode = Request.QueryString["ResourceCode"],
                        ResourceTypeCode = Request.QueryString["ResourceType"],
                        QueryID = entity.QueryID,
                        CreateTime = DateTime.Now,
                        Creator = ETMS.AppContext.UserContext.Current.RealName,
                        Modifier = ETMS.AppContext.UserContext.Current.RealName,
                        ModifiyTime = DateTime.Now
                    };
                    ResourceQueryLogic.Add(queryPublishObject);

                    //“R2需求调查”设置资源调查问卷发布范围（默认范围）
                    if (queryPublishObject.ResourceTypeCode.Equals("R2", StringComparison.InvariantCultureIgnoreCase))
                    {
                        QueryAreaLogic.Add(new Poll_QueryArea()
                        {
                            AreaType = EnumQueryAreaType.CurrentOrg.ToString(),//默认仅本机构
                            AreaCode = ETMS.AppContext.UserContext.Current.OrganizationID.ToString(),
                            CreateTime = DateTime.Now,
                            Creator = ETMS.AppContext.UserContext.Current.RealName,
                            QueryPublishID = queryPublishObject.QueryPublishID,
                        });
                    }
                    break;
                case "edit":
                    entity = (Poll_Query)this.UserControl1.DomainModel;
                    entity.ModifyTime = DateTime.Now;
                    entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    Logic.Save(entity);
                    break;
                case "delete":
                    Logic.Remove(int.Parse(Request.QueryString["id"]));
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

