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
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_Department_View : BasePage
{
    private static DepartmentLogic Logic = new DepartmentLogic();
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
                ,RequestParameter.CreateRangeRequestParameter("parentid",RequestParameter.NaturalInt32RangeVerify)
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
                    this.UserControl1.BindFromData(new Department(), ViewMode.Edit);
                    break;
                case "edit":
                    this.UserControl1.BindFromData(Logic.GetNodeByID(int.Parse(Request.QueryString["id"])), ViewMode.Edit);
                    break;
                case "view":
                    this.UserControl1.BindFromData(Logic.GetNodeByID(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
                    break;
                case "delete":
                    this.UserControl1.BindFromData(Logic.GetNodeByID(int.Parse(Request.QueryString["id"])), ViewMode.Browse);
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
        try
        {
            string sOperType = ((Button)sender).CommandName;
            Department entity = null;
            switch (sOperType)
            {
                case "add":
                    entity = (Department)this.UserControl1.DomainModel;
                    entity.CreateTime = DateTime.Now;
                    entity.Creator = ETMS.AppContext.UserContext.Current.RealName;
                    entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                    entity.ModifyTime = DateTime.Now;
                    int nodeID = this.PageRequestArgs[0].ParameterValue.ToInt();
                    entity.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                    //������ʾ����·��
                    Node parentNode = Logic.GetNodeByID(nodeID);
                    (entity as Department).DisplayPath = (parentNode as Department).DisplayPath + string.Format("/{0}", entity.NodeName);
                    parentNode = (Department)Logic.GetNodeTreeForManager(parentNode, false);
                    //entity.Path = parentNode.NextChildCode;//����path
                    entity.Path = Logic.GetNewPath(entity.OrganizationID,nodeID);//����path
                    entity.DepartmentCode= Logic.GetNewCode(entity.OrganizationID,nodeID);//����path
                    entity.ParentNodeID = nodeID;//���ø��ڵ�
                    entity.State = 1;//Ĭ������
                    Logic.Save(entity);
                    break;
                case "edit":
                    entity = (Department)this.UserControl1.DomainModel;
                    entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                    entity.ModifyTime = DateTime.Now;
                    Logic.Save(entity);
                    break;
                case "delete":
                    //���¼�����ʱ������ɾ��
                    Logic.Remove(new Department() { NodeID = int.Parse(Request.QueryString["id"]) });
                    break;
            }

            ETMS.Utility.JsUtility.CloseWindow("function(){window.parent.location.href=window.parent.location.href;}");//ˢ��ҳ��
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}

