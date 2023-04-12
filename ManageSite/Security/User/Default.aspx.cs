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
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_User_Default : BasePage
{
    UserLogic Logic = new UserLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new ETMS.Controls.IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    protected void UserOpeator_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int userID = e.CommandArgument.ToInt();
            switch (e.CommandName)
            {
                case "SwitchStatus":
                    Logic.SwitchUserStatus(userID, ETMS.AppContext.UserContext.Current.RealName);
                    ETMS.Utility.JsUtility.SuccessMessageBox("�����û�״̬�����ɹ���");
                    break;
                case "Reset":
                    AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
                    AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
                    Logic.ResetPassword(userID, ETMS.AppContext.UserContext.Current.RealName, strategy.Security_PassWord_Default);//�������ݿ�
                    ETMS.Utility.JsUtility.SuccessMessageBox("�û��������óɹ���");
                    break;
                case "Remove":
                    Logic.Remove(new User() { UserID = userID });
                    ETMS.Utility.JsUtility.SuccessMessageBox("�û�ɾ���ɹ���");
                    break;
            }
            //ˢ��
            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    public bool IsDisplay
    {
        get
        {
            return !(ETMS.AppContext.UserContext.Current.UserName.Equals("SysAdmin", StringComparison.InvariantCultureIgnoreCase));
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        int startIndex = (pageIndex - 1) * pageSize;
        //�����ǰ�û��ǳ�������Ա����Ĭ�����л�������Ա�˺�
        IList dataList = null;
        if (ETMS.AppContext.UserContext.Current.UserName.Equals("SysAdmin", StringComparison.InvariantCultureIgnoreCase))
        {
            //���δ����û���ť

            //��Ĭ�����л�������Ա�˺�
            dataList = Logic.GetPagedList(startIndex, pageSize, string.Format(" AND IsSysAccount=1 AND LoginName like '%{0}%' AND RealName like '%{1}%'", this.txtLoginName.Text.Trim().ToSafeSQLValue(), this.txtRealName.Text.Trim().ToSafeSQLValue()), " LoginName ", out totalRecords);
        }
        else//���б������µ��˺�
        {
            dataList = Logic.GetPagedList(startIndex, pageSize, string.Format(" AND [OrganizationID]={0} AND LoginName like '%{1}%' AND RealName like '%{2}%'", ETMS.AppContext.UserContext.Current.OrganizationID, this.txtLoginName.Text.Trim().ToSafeSQLValue(), this.txtRealName.Text.Trim().ToSafeSQLValue()), " LoginName ", out totalRecords);
        }

        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        //{
        //    Site_User entity = (Site_User)e.Row.DataItem;
        //    HyperLink lk = (HyperLink)e.Row.FindControl("lkpxkc");
        //    Tb_e_TrainCourse course = CourseLogic.GetById(entity.Pxjhkcbh);
        //    lk.Text = course.Kcmc;
        //    lk.NavigateUrl = string.Format("../Tb_e_TrainCourse/View.aspx?id={0}&op=view", course.Pxkcbh);
        //}
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        object[] selectedValues = CustomGridView.GetSelectedValues<object>(this.GridViewList);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("���ٹ�ѡһ���û���");
            return;
        }
        //���������û�����
        AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
        AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
        foreach (object obj in selectedValues)
        {
            Logic.ResetPassword(obj.ToInt(), ETMS.AppContext.UserContext.Current.RealName, strategy.Security_PassWord_Default);//�������ݿ�            
        }
        //ˢ������
        this.PageSet1.DataBind();
        ETMS.Utility.JsUtility.SuccessMessageBox("����������������ɹ���");
    }
}

