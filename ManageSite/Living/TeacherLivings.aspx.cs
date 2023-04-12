using ETMS.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying;
using ETMS.Components.OnlinePlaying.Core;

public partial class Living_TeacherLivings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DateTime startTime = default(DateTime);
        if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            startTime = txtBeginTime.Text.Trim().ToStartDateTime();
        else
            startTime = DateTime.Now.AddYears(-100);
        DateTime endTime = default(DateTime);
        if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            endTime = txtEndTime.Text.Trim().ToEndDateTime();
        else
            endTime = DateTime.Now.AddYears(100);

        var data = new ETMS.Components.OnlinePlaying.Implement.BLL.OnlinePlayingLogic().GetLivingsByTeacher(UserContext.Current.UserID, startTime, endTime, txtLivingName.Text.Trim(), txtCourseName.Text.Trim(), ddlLivingType.SelectedValue.ToInt(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(data, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ctl = (LinkButton)e.Row.FindControl("lbtLiving");
            Res_Living entity = (Res_Living)e.Row.DataItem;
            if (null != entity && null != entity.LivingID)
            {
                var hisLink = GetUrl(entity);
                if (!string.IsNullOrEmpty(hisLink))
                {
                    ctl.Text = string.Format("<a href='{0}' target='_blank'>直播间</a>", hisLink);
                }
                else {
                    ctl.Text = string.Format("<a onclick=\"nostart('{0}')\">直播间</a>", ctl.ClientID);
                }
            }
        }
    }

    private string GetUrl(Res_Living entity)
    {
        //var entity = new ETMS.Components.OnlinePlaying.Implement.BLL.OnlinePlayingLogic().GetLiving(livingID);
        MTCloud live = new MTCloud();

        if (entity.Flag == 1)
        {
            if (entity.LivingType == 3)
            {
                string data = live.courseLaunch(entity.LivingID);
                CourseLaunchResult result = JsonHelper.DeserializeObject<CourseLaunchResult>(data);

                if (result.code == "0")
                {
                    return result.data.url;
                }
            }
            else {
                return new LivingProvider().EnterTKRoom(entity.LivingID, entity.TeacherName, string.Empty, 0);
            }
        }
        else
        {
            if (entity.LivingType == 3)
            {
                string data = live.courseAccessPlayback(entity.LivingID, entity.UserName, entity.TeacherName, MTCloud.ROLE_ADMIN, 7200, null);
                CourseAccessResult result = JsonHelper.DeserializeObject<CourseAccessResult>(data);

                if (result.code == "0")
                {
                    return result.data.playbackUrl;
                }
            }
            else {
                var historyRecords = new LivingProvider().GetRecordList(entity.LivingID);
                if (historyRecords!=null)
                {
                    // 如果有多个直播，取时间最长的那个
                    historyRecords = historyRecords.OrderByDescending(f => f.duration).ToList();
                    return historyRecords[0].playpath;
                }

            }
        }

        return string.Empty;
    }
}