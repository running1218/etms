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


using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
public partial class Admin_InnerRole_View : ETMS.Controls.BasePage
{
    private static RoleLogic roleLogic = new RoleLogic();

    private Node parentNode = null;

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
             */
            return new RequestParameter[]
            {
                 RequestParameter.CreateRangeRequestParameter("id",RequestParameter.NaturalInt32RangeVerify)
                ,RequestParameter.CreateRangeRequestParameter("op",RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
            };
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int parentID = this.PageRequestArgs[0].ParameterValue.ToInt();

        ///*
        // * 检查当前角色是否越权
        // */
        //Node currentRequestNode = (parentID == 0 ? null : roleLogic.GetNodeByID(parentID));
        //IRole userRoleNode = WebSession.Administrator.MapRole;
        ////如果当前用户角色编码不是以用户登陆的角色编码开始，则提示,并返回前一个操作页面
        //if ((!userRoleNode.IsSysAdminRole && parentID == 0) || (currentRequestNode != null && !currentRequestNode.NodeCode.StartsWith(this.UserRole)))
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
                        //显示“关闭”按钮
                        Button ibReturn = (Button)this.dvRole.FindControl("ImageButtonReturn");
                        if (ibReturn != null)
                        {
                            ibReturn.Text = "关闭";
                        }
                        break;
                    }
                case "delete":
                    {
                        if (Request.QueryString["id"] == "1")
                        {
                            throw new Exception("内置“超级管理员”系统角色不能删除！");
                        }
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
    }
    protected void ObjectDataSourceSite_Role_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Node entity = (Node)e.InputParameters[0];
        //载入旧记录
        Node oldEntity = (Node)roleLogic.GetNodeByID(entity.NodeID);
        entity.ParentNodeID = oldEntity.ParentNodeID;
        entity.Creator = oldEntity.Creator;
        entity.CreateTime = oldEntity.CreateTime;
        entity.ModifyTime = DateTime.Now;
        entity.Modifier = this.LoginName;

        RadioButtonList rbls = (RadioButtonList)this.dvRole.FindControl("rbtnlistState");
        entity.State = rbls.SelectedValue.ToInt();
    }
    protected void ObjectDataSourceSite_Role_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Node entity = (Node)e.InputParameters[0];
        entity.CreateTime = DateTime.Now;
        entity.Creator = this.LoginName;
        entity.ModifyTime = DateTime.Now;
        entity.Modifier = entity.Creator; 
        entity.NodeCode = roleLogic.GetNodeTreeForManager(entity, false).NextChildCode;
        RadioButtonList rbls = (RadioButtonList)this.dvRole.FindControl("rbtnlistState");
        entity.State = rbls.SelectedValue.ToInt();
        entity.ParentNodeID = this.PageRequestArgs[0].ParameterValue.ToInt();
    }
    protected void ObjectDataSourceSite_Role_Save(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null && e.Exception.InnerException != null)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(e.Exception.InnerException));
            e.ExceptionHandled = true;
        }
        else
        {
            Response.Redirect(this.ActionHref("alert.aspx"));
        }
    }
    protected void ObjectDataSourceSite_Role_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Node entity = (Node)e.InputParameters[0];
        entity.NodeID = this.PageRequestArgs[0].ParameterValue.ToInt();
    }
}
