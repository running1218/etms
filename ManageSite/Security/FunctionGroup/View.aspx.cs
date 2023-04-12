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
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Role_View : ETMS.Controls.BasePage
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();

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
                 RequestParameter.CreateRangeRequestParameter("id", Request.QueryString["id"],RequestParameter.NaturalInt32RangeVerify)
                ,RequestParameter.CreateRangeRequestParameter("op", Request.QueryString["op"],RequestParameter.EnumTypeRangeVerify(new string[] { "add", "edit", "delete", "view" }))
                ,RequestParameter.CreateRangeRequestParameter("parentid",RequestParameter.NaturalInt32RangeVerify)
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
                        Button ibUpdateOk4 = (Button)this.dvRole.FindControl("ImageButtonReturn");
                        if (ibUpdateOk4 != null)
                        {
                            ibUpdateOk4.Text = "关闭";
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
            if (parentNode == null)
            {
                int nodeID = this.PageRequestArgs[0].ParameterValue.ToInt();
                if (nodeID == 0)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox("仅允许一个根功能组！");
                    return;
                }
                parentNode = functionGroupLogic.GetNodeByID(nodeID);
                if (parentNode.ParentNodeID == 0)//如果父节点为root节点
                {
                    parentNode.ParentNode = new FunctionGroup();
                }
                else
                {
                    parentNode.ParentNode = parentNode;
                }
                parentNode = functionGroupLogic.GetNodeTreeForManager(parentNode, false);
            }

            Label lblRoleCode = (Label)this.dvRole.FindControl("LabelRoleCode");
            lblRoleCode.Text = parentNode.NextChildCode;

            DropDownList ddl = (DropDownList)this.dvRole.FindControl("ddlOrderNo");
            if (parentNode.ChildNodes.Count == 0)
            {
                ddl.SelectedIndex = 0;
            }
            else
            {
                System.Collections.Generic.List<Node> list = (System.Collections.Generic.List<Node>)parentNode.ChildNodes;
                list.Sort(new Comparison<Node>(delegate(Node A, Node B) { return A.OrderNo.CompareTo(B.OrderNo); }));
                ddl.SelectedValue = (parentNode.ChildNodes[parentNode.ChildNodes.Count - 1].OrderNo + 1).ToString();
            }
        }

    }
    protected void ObjectDataSourceSite_Role_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Node entity = (Node)e.InputParameters[0];

        //载入旧记录
        Node oldEntity = (Node)functionGroupLogic.GetNodeByID(entity.NodeID);
        entity.ParentNodeID = oldEntity.ParentNodeID;
        entity.Creator = oldEntity.Creator;
        entity.CreateTime = oldEntity.CreateTime;
        entity.ModifyTime = DateTime.Now;
        entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;

        RadioButtonList rbls = (RadioButtonList)this.dvRole.FindControl("rbtnlistState");
        entity.State = rbls.SelectedValue.ToInt();

        DropDownList ddl = (DropDownList)this.dvRole.FindControl("ddlOrderNo");
        entity.OrderNo = ddl.SelectedValue.ToInt();
    }
    protected void ObjectDataSourceSite_Role_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Node entity = (Node)e.InputParameters[0];
        entity.CreateTime = DateTime.Now;
        entity.Creator = ETMS.AppContext.UserContext.Current.RealName;
        entity.ModifyTime = DateTime.Now;
        entity.Modifier = entity.Creator;

        RadioButtonList rbls = (RadioButtonList)this.dvRole.FindControl("rbtnlistState");
        entity.State = rbls.SelectedValue.ToInt();
        entity.ParentNodeID = this.PageRequestArgs[0].ParameterValue.ToInt();

        DropDownList ddl = (DropDownList)this.dvRole.FindControl("ddlOrderNo");
        entity.OrderNo = ddl.SelectedValue.ToInt();
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
        entity.NodeID =this.PageRequestArgs[0].ParameterValue.ToInt();
    }
}
