using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Mentor.Implement.BLL.Mentor;
using ETMS.Components.Mentor.API.Entity.Mentor;
using ETMS.Controls;
using ETMS.Utility;

public partial class Security_MentorManage_MentorListAdd : ETMS.Controls.BasePage
{
    private static Site_MentorLogic siteMentorLogic = new Site_MentorLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        aBack.HRef = "MentorList.aspx";
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string realName = this.txtTeacherName.Text.Trim();
        string departmentName = txtDepartment.Text.Trim();
        string post = txtPost.Text.Trim();
        List<Site_Mentor> dt = siteMentorLogic.ChoseMentorList(realName, departmentName, post, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int[] selectNum = CustomGridView.GetSelectedValues<int>(this.GridViewList);
        if (selectNum.Length==0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的导师！");
            return;
        }
        else
        {
            try
            {
                siteMentorLogic.SetMentor(selectNum);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "导师添加成功！", "function (){window.location='" + this.ActionHref("MentorList.aspx") + "'}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
}