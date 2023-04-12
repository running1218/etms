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
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Site_Department_UserControl : System.Web.UI.UserControl, IMuliViewControl
{
    private static DepartmentLogic Logic = new DepartmentLogic();
    private static bool IsIncludeDataTimeField = false;
    private void DateTimeControlSetting()
    {
        TextBox[] txtBeginTimes = new TextBox[0]; //{};
        foreach (TextBox txtBeginTime in txtBeginTimes)
        {
            txtBeginTime.Attributes.Add("readonly", "");
            txtBeginTime.Attributes.Add("onfocus", "new WdatePicker(this,'%Y-%M-%D',true)");
            txtBeginTime.Attributes.Add("class", "Wdate");
        }
        string ScriptFormat = "  <script language=\"javascript\" type=\"text/javascript\" src=\"{0}/DateControl/WdatePicker.js\"></script>";
        Page.RegisterClientScriptBlock("datetimejs", string.Format(ScriptFormat, ETMS.Utility.WebUtility.AppPath));
    }
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

    #region Manage View
    public void BindFromData_Manage(object domainModel)
    {

    }
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {
        if (IsIncludeDataTimeField)
            DateTimeControlSetting();

        if (!this.Page.IsPostBack)
        {
            //设置顺序下拉列表
            for (int i = 1; i <= 100; i++)
            {
                ddlOrder.Items.Add(i.ToString());
            }
            Department entity = (Department)domainModel;
            //载入父部门名称与编号
            Department parentOrg = (Department)Logic.GetNodeByID(int.Parse(Request.QueryString["parentid"]));
            parentOrg = (Department)Logic.GetNodeTreeForManager(parentOrg, false);//载入子节点
            if (parentOrg.NodeID == 0)
            {
                OrganizationLogic orgLogic = new OrganizationLogic();
                parentOrg.DisplayPath = orgLogic.GetNodeByID(ETMS.AppContext.UserContext.Current.OrganizationID).NodeName;
            }
            this.lblParentCode.Text = parentOrg.DepartmentCode;
            this.lblParentName.Text = parentOrg.DisplayPath;

            if (entity.NodeID == 0)//注意:仅添加模式不需要
            {
                //自动计算序号
                if (parentOrg.ChildNodes.Count == 0)
                {
                    ddlOrder.SelectedIndex = 0;
                }
                else
                {
                    System.Collections.Generic.List<Node> list = (System.Collections.Generic.List<Node>)parentOrg.ChildNodes;
                    list.Sort(new Comparison<Node>(delegate(Node A, Node B) { return A.OrderNo.CompareTo(B.OrderNo); }));
                    ddlOrder.SelectedValue = (parentOrg.ChildNodes[parentOrg.ChildNodes.Count - 1].OrderNo + 1).ToString();
                }

                //自动部门编码
                string autoCode = parentOrg.NextChildCode;
                this.txtDepartmentCode.Text = autoCode.Substring((autoCode.Length / 3 - 1) * 3);//三位部门编码，自动生成部门编码取最后三位
                return;
            }
            //载入数据
            this.txtDepartmentCode.Text = entity.DepartmentCode;
            this.txtDepartmentName.Text = entity.DepartmentName;
            this.txtDescription.Text = entity.Description;
            this.txtManager.Text = entity.Manager;
            this.ddlOrder.SelectedValue = entity.OrderNo.ToString();
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {

        Department entity = (Department)domainModel;

        this.lblDepartmentCode.Text = entity.DepartmentCode;
        this.lblDepartmentName.Text = entity.DepartmentName;
        this.lblDescription.Text = entity.Description;
        this.lblManager.Text = entity.Manager;
        this.lblStatus.Text = entity.State == 1 ? "启用" : "停用";

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
            Department entity = (Department)domainModel;

            //载入数据

            entity.Manager = this.txtManager.Text;
            entity.DepartmentCode = this.txtDepartmentCode.Text;
            entity.DepartmentName = this.txtDepartmentName.Text;
            entity.Description = this.txtDescription.Text;
            entity.OrderNo = int.Parse(this.ddlOrder.SelectedValue);
        }
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }

    #endregion
    #endregion
}

