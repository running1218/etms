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
using ETMS.Components.Basic.API.Entity;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_FunctionGroup_Function_View : ETMS.Controls.BasePage
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
 
    private string LoginName
    {
        get
        {
            return ETMS.AppContext.UserContext.Current.UserName;
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
    /// <summary>
    /// 页面验证参数定义
    /// </summary>
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            /*
             * 需要验证的页面参数包含：
             *    参数名  参数范围
             * 1、  id    {0,1,2..}
             * 2、  op    {add,edit,delete,view}
             * 3、groupid  {0,1,2..}
             */
            return new RequestParameter[]
            {
                 RequestParameter.CreateRangeRequestParameter("id", Request.QueryString["id"],RequestParameter.NaturalInt32RangeVerify)
                ,RequestParameter.CreateRangeRequestParameter("op", Request.QueryString["op"],RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
                ,RequestParameter.CreateRangeRequestParameter("groupid", Request.QueryString["groupid"],RequestParameter.NaturalInt32RangeVerify)
            };   
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    { 
        ///*
        //* 检查当前角色是否越权
        //*/
        //if (!WebSession.Administrator.MapRole.IsSysAdminRole)//仅系统管理员有此页访问权限
        //{
        //    ETMS.WebBase.JsUtility.MessageBoxAndRedirect("访问被拒绝，原因：权限不足！", string.Format("{0}/Admin/Default.aspx", ETMS.Utility.WebUtility.AppPath), this.Page);
        //    //return;
        //}
        if (!IsPostBack)
        {
            //根据操作类型显示不同操作界面
            string sOperType = Request.QueryString["op"];
            switch (sOperType)
            {
                case "add":
                    {
                        this.dvRole.DefaultMode = DetailsViewMode.Insert;

                        //不显示“删除”按钮
                        Button ibDeleteOk0 = (Button)this.dvRole.FindControl("ImageButtonDeleteOk");
                        if (ibDeleteOk0 != null)
                        {
                            ibDeleteOk0.Visible = false;
                        }

                        //不显示“更新”按钮
                        Button ibUpdateOk0 = (Button)this.dvRole.FindControl("ImageButtonUpdateOk");
                        if (ibUpdateOk0 != null)
                        {
                            ibUpdateOk0.Visible = false;
                        }
                    }
                    break;
                case "edit":
                    {
                        this.dvRole.DefaultMode = DetailsViewMode.Edit;

                        //显示“更新”按钮
                        Button ibUpdateOk2 = (Button)this.dvRole.FindControl("ImageButtonUpdateOk");
                        if (ibUpdateOk2 != null)
                        {
                            ibUpdateOk2.Visible = true;
                        }

                        //不显示“增加”按钮
                        Button ibInsertOk1 = (Button)this.dvRole.FindControl("ImageButtonInsertOk");
                        if (ibInsertOk1 != null)
                        {
                            ibInsertOk1.Visible = false;
                        }

                        //不显示“删除”按钮
                        Button ibDeleteOk1 = (Button)this.dvRole.FindControl("ImageButtonDeleteOk");
                        if (ibDeleteOk1 != null)
                        {
                            ibDeleteOk1.Visible = false;
                        }
                    }
                    break;
                case "view":
                    {
                        this.dvRole.DefaultMode = DetailsViewMode.ReadOnly;

                        //不显示“增加”按钮
                        Button ibInsertOk2 = (Button)this.dvRole.FindControl("ImageButtonInsertOk");
                        if (ibInsertOk2 != null)
                        {
                            ibInsertOk2.Visible = false;
                        }

                        //不显示“删除”按钮
                        Button ibDeleteOk2 = (Button)this.dvRole.FindControl("ImageButtonDeleteOk");
                        if (ibDeleteOk2 != null)
                        {
                            ibDeleteOk2.Visible = false;
                        }

                        //不显示“更新”按钮
                        Button ibUpdateOk2 = (Button)this.dvRole.FindControl("ImageButtonUpdateOk");
                        if (ibUpdateOk2 != null)
                        {
                            ibUpdateOk2.Visible = false;
                        }
                        break;
                    }
                case "delete":
                    {
                        this.dvRole.DefaultMode = DetailsViewMode.ReadOnly;

                        //显示“删除”按钮
                        Button ibDeleteOk2 = (Button)this.dvRole.FindControl("ImageButtonDeleteOk");
                        if (ibDeleteOk2 != null)
                        {
                            ibDeleteOk2.Visible = true;
                        }

                        //不显示“增加”按钮
                        Button ibInsertOk3 = (Button)this.dvRole.FindControl("ImageButtonInsertOk");
                        if (ibInsertOk3 != null)
                        {
                            ibInsertOk3.Visible = false;
                        }

                        //不显示“更新”按钮
                        Button ibUpdateOk3 = (Button)this.dvRole.FindControl("ImageButtonUpdateOk");
                        if (ibUpdateOk3 != null)
                        {
                            ibUpdateOk3.Visible = false;
                        } 
                    }
                    break;
            }
        }

    }
    protected void dvRole_DataBound(object sender, EventArgs e)
    {
        if (this.dvRole.DefaultMode == DetailsViewMode.Insert)
        {
            DropDownList ddl = (DropDownList)this.dvRole.FindControl("ddlOrderNo");

            FunctionGroup parentNode = new FunctionGroup();
            parentNode.NodeID = this.PageRequestArgs[0].ParameterValue.ToInt();
            FunctionGroupFunctionRelationLogic functionGroupFunctionRelationLogic = new FunctionGroupFunctionRelationLogic(parentNode);
            functionGroupFunctionRelationLogic.Manager.KeyName = "FunctionGroupID";

            Function[] functions = (Function[])functionGroupFunctionRelationLogic.GetAllMembers("");
            if (functions.Length == 0)
            {
                ddl.SelectedIndex = 0;
            }
            else
            {
                Array.Sort<Function>(functions, new Comparison<Function>(delegate(Function A, Function B) { return A.OrderNo.CompareTo(B.OrderNo); }));
                ddl.SelectedValue = (functions[functions.Length - 1].OrderNo + 1).ToString();
            }
        }

    }
    protected void ObjectDataSourceSite_Role_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Function entity = (Function)e.InputParameters[0];
        Function oldEntity = (Function)new FunctionLogic().GetFunctionByID(entity.FunctionID);
        entity.FunctionGroupID = oldEntity.FunctionGroupID;
        entity.HelpID = oldEntity.HelpID;

        entity.Creator = oldEntity.Creator;
        entity.CreateTime = oldEntity.CreateTime;

        entity.ModifyTime = DateTime.Now;
        entity.Modifier = this.LoginName; 
    }
    protected void ObjectDataSourceSite_Role_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Function entity = (Function)e.InputParameters[0];
        entity.CreateTime = DateTime.Now;
        entity.Creator = this.LoginName;
        entity.ModifyTime = DateTime.Now;
        entity.Modifier = entity.Creator;
        entity.HelpID = 0; 
        entity.FunctionGroupID = this.PageRequestArgs[0].ParameterValue.ToInt();
    }
    protected void ObjectDataSourceSite_Role_Save(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
            throw e.Exception.InnerException;
        else
        {
            ETMS.Utility.JsUtility.CloseWindow("function(){window.parent.location.href=window.parent.location.href;}");//刷新页面
        }
    }
    protected void ObjectDataSourceSite_Role_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Function entity = (Function)e.InputParameters[0];
        entity.FunctionID = this.PageRequestArgs[0].ParameterValue.ToInt();
    }
}
