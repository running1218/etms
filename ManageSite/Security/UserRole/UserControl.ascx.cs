using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_UserRole_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static UserRoleRelationLogic Logic = new UserRoleRelationLogic();
    private static RoleLogic RoleLogic = new RoleLogic();
    private static UserLogic userLogic = new UserLogic();

    #region IMuliViewControl 成员
    private object m_DomainModel;
    public Object DomainModel
    {
        get
        {
            return this.Views.DomainModel;
        }
        set
        {
            m_DomainModel = value;
        }
    }

    public ViewMode ControlMode
    {
        get
        {
            return this.Views.ControlMode;
        }
    }

    public int UserID
    {
        get
        {
            return int.Parse(Request.QueryString["id"]);
        }
    }
    #region Manage View
    public void BindFromData_Manage(object domainModel)
    {

    }
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {

        this.lblLoginName.Text = userLogic.GetUserByID(this.UserID).LoginName;

        //1、用户已有的角色列表
        List<Role> userRoles = (List<Role>)Logic.Query(this.UserID);
        foreach (Role role in userRoles)
        {
            if (role.OrganizationID == 0)
            {
                continue;
            }
            this.lboxSelectedRoles.Items.Add(new ListItem((role.State == 1 ? role.RoleName : role.RoleName + "(已停用)"), role.RoleID.ToString()));
            //this.lboxSelectedRoles.Items.Add(new ListItem((role.OrganizationID == 0 ? role.RoleName + "(系统)" : role.RoleName + "(用户)"), role.RoleID.ToString()));
        }
        //2、载入角色列表
        //2.1 系统系统角色
        //Role[] sysRoles = RoleLogic.GetOrganizationRoles(0);
        //foreach (Role role in sysRoles)
        //{
        //    if (!userRoles.Exists(new Predicate<Role>(delegate(Role item) { return role.RoleID.Equals(item.RoleID); })))
        //    {
        //        this.lboxRoles.Items.Add(new ListItem(role.RoleName + "(系统)", role.RoleID.ToString()));
        //    }

        //}
        //2.2 机构用户角色
        Role[] orgRoles = RoleLogic.GetOrganizationRoles(ETMS.AppContext.UserContext.Current.OrganizationID);
        foreach (Role role in orgRoles)
        {
            //停用机构不显示 #Bug(181)
            if (role.State == 0)
            {
                continue;
            }
            if (!userRoles.Exists(new Predicate<Role>(delegate(Role item) { return role.RoleID.Equals(item.RoleID); })))
            {
                //this.lboxRoles.Items.Add(new ListItem(role.RoleName + "(用户)", role.RoleID.ToString()));
                this.lboxRoles.Items.Add(new ListItem(role.RoleName, role.RoleID.ToString()));
            }
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {
        this.Label1.Text = userLogic.GetUserByID(this.UserID).LoginName;

        //1、用户已有的角色列表
        List<Role> userRoles = (List<Role>)Logic.Query(this.UserID);
        foreach (Role role in userRoles)
        {
            //this.ListBox1.Items.Add(new ListItem((role.OrganizationID == 0 ? role.RoleName + "(系统)" : role.RoleName + "(用户)"), role.RoleID.ToString()));
            if (role.OrganizationID == 0)
            {
                continue;
            }
            this.ListBox1.Items.Add(new ListItem((role.State == 1 ? role.RoleName : role.RoleName + "(已停用)"), role.RoleID.ToString()));
        }
    }
    #endregion

    #region List_View
    public void BindFromData_List(object domainModel)
    {

    }
    #endregion

    #region Handler

    public object UnBindFromData(object domainModel)
    {
        if (this.ControlMode == ViewMode.Edit)
        {
            StringBuilder writer = new StringBuilder();
            foreach (ListItem item in this.lboxSelectedRoles.Items)
            {
                writer.AppendFormat("{0},", item.Value);
            }
            return writer.ToString();
        }
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }

    #endregion
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        //添加
        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in this.lboxRoles.Items)
        {
            if (item.Selected)
            {
                selected.Add(item);
            }
        }
        foreach (ListItem item in selected)
        {
            this.lboxRoles.Items.Remove(item);
            this.lboxSelectedRoles.Items.Add(item);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //移除
        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in this.lboxSelectedRoles.Items)
        {
            if (item.Selected)
            {
                selected.Add(item);
            }
        }
        foreach (ListItem item in selected)
        {
            this.lboxSelectedRoles.Items.Remove(item);
            this.lboxRoles.Items.Add(item);
        }
    }
}

