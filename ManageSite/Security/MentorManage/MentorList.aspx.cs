using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using System.Data;
using ETMS.Components.Mentor.Implement.BLL.Mentor;
using ETMS.Components.Mentor.API.Entity.Mentor;
using ETMS.Controls;
using ETMS.WebApp.Manage;


public partial class Security_TutorManage_TutorList : ETMS.Controls.BasePage
{
    private static Site_MentorLogic siteMentorLogic = new Site_MentorLogic();
    private Site_Mentor siteMentor = new Site_Mentor();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        //this.btnAdd.PostBackUrl = this.ActionHref(string.Format("{0}/Security/ProfessorManage/ProfessorAddInner.aspx", WebUtility.AppPath));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string teacherName = txtTeacherName.Text.Trim();
        string depart = txtDepartment.Text.Trim();
        string rank = txtPost.Text.Trim();
        int isUse = Dic_Status.SelectedValue.ToInt();

        List<Site_Mentor> dt = siteMentorLogic.GetMentorList(teacherName,depart,rank,isUse,pageIndex,pageSize,out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            Site_Mentor mentor = e.Row.DataItem as Site_Mentor;
            LinkButton btnStart = e.Row.FindControl("lbnStart") as LinkButton;
            LinkButton btnStop = e.Row.FindControl("lbnStop") as LinkButton;
            btnStart.Visible=false;
            btnStop.Visible=false;
            if (mentor.IsUse == 1)
            {
                btnStop.Visible = true;
            }
            else
            {
                btnStart.Visible = true;
            }
        }
    }

    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName.ToLower())
            {
                case "start":
                    siteMentor = siteMentorLogic.GetById(e.CommandArgument.ToInt());
                    siteMentor.IsUse = 1;
                    siteMentorLogic.UpdateStatus(siteMentor);
                    Extention.SuccessMessageBox("设置成功！");
                    this.PageSet1.DataBind();
                    break;
                case "stop":
                    siteMentor = siteMentorLogic.GetById(e.CommandArgument.ToInt());
                    siteMentor.IsUse = 0;
                    siteMentorLogic.UpdateStatus(siteMentor);
                    Extention.SuccessMessageBox("设置成功！");
                    this.PageSet1.DataBind();
                    break;
                case "del":
                    siteMentorLogic.Remove(e.CommandArgument.ToInt());
                    this.PageSet1.DataBind();
                    break;
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
    
}