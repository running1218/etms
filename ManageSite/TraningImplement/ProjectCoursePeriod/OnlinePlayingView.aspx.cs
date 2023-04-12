using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying.Implement.BLL;

public partial class TraningImplement_ProjectCoursePeriod_OnlinePlayingView : System.Web.UI.Page
{
    /// <summary>
    /// 直播ID 
    /// </summary>
    public string OnlinePlayingID
    {
        get
        {
            return Request.ToparamValue<string>("OnlinePlayingID");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialData();
        }
    }

    private void InitialData()
    {
        var entity = new OnlinePlayingLogic().GetOnlinePlaying(OnlinePlayingID);

        if (null != entity)
        {
            lblSubject.Text = entity.PlayingSubject;
            lblTrainingDate.Text = entity.StartTime.ToDate();
            lblTrainingBeginTime.Text = entity.StartTime.ToString("HH:mm");
            lblTrainingEndTime.Text = entity.EndTime.ToString("HH:mm");
            lblTeacher.Text = entity.TeacherName;
            lblOnlinePlayingDesc.Text = entity.Description;
        }
    }
}