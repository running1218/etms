using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.AppContext;

public partial class TraningImplement_ProjectCoursePeriod_OnlinePlayingList : System.Web.UI.Page
{
    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();
        OnlinePlayingInfo entity = new OnlinePlayingInfo()
        {
            ItemName = txtItemName.Text,
            CourseName = txtCourseName.Text,
            PlayingSubject = txtPlayingSubject.Text,
            StartTime = txtStartTime.Text.Trim() == string.Empty ? string.Format("1900-01-01 00:00:00.000").ToDateTime() : string.Format("{0} 00:00:00.000", txtStartTime.Text.Trim()).ToDateTime(),
            EndTime = txtEndTime.Text.Trim() == string.Empty ? DateTime.MaxValue : string.Format("{0} 23:59:59.900", txtEndTime.Text.Trim()).ToDateTime(),
            OnlineStatus = rblOnlineStatus.SelectedValue == "0" ? false: true,
            UserID = UserContext.Current.UserID
        };
        var source = onlinePlayingLogic.QueryOnlinePlaying(entity, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;                
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltlPlaying = (Literal)e.Row.FindControl("lbnEntery");
            CustomLinkButton lbnEnd = (CustomLinkButton)e.Row.FindControl("lbnEnd");

            if (null != ltlPlaying && null != lbnEnd)
            {
                OnlinePlayingInfo entity = (OnlinePlayingInfo)e.Row.DataItem;
                if (entity.OnlineStatus)
                {
                    var url = string.Format("{0}?nickName={1}&token={2}", entity.OrganizerJoinUrl, Server.UrlEncode(UserContext.Current.RealName), entity.OrganizerToken);
                    ltlPlaying.Text = string.Format("<a target=\"_blank\" href=\"{0}\">开始直播</a>", url);
                }
                else
                {
                    ltlPlaying.Text = string.Format("<a class=\"link_colorGray\">开始直播</a>");
                    lbnEnd.Enabled = false;
                    lbnEnd.CssClass = "link_colorGray";
                }
            }
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "end")
        {
            try
            {
                OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();
                onlinePlayingLogic.FinishOnlinePlaying(e.CommandArgument.ToString());
                ETMS.Utility.JsUtility.SuccessMessageBox("结束直播信息成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}