using System;
using System.Linq;
using System.Web.UI.WebControls;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Components.OnlinePlaying.Core;
using ETMS.Components.OnlinePlaying.API.Entity;
using University.Mooc.AppContext;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying;

namespace ETMS.Studying.Public
{
    public partial class LivingGuide : System.Web.UI.Page
    {
        private static readonly OnlinePlayingLogic livingLogic = new OnlinePlayingLogic();
        private static readonly LivingProvider livingProvider = new LivingProvider();

        private string LivingID
        {
            get {
                return Request.QueryString["LivingID"];
            }
        }

        private string UserName
        {
            get {
                if (null != UserContext.Current.LoginName && !string.IsNullOrEmpty(UserContext.Current.LoginName))
                {
                    return UserContext.Current.LoginName;
                }
                else {
                    return CrypProvider.GeneralRandom(8);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Res_Living entity = livingLogic.GetLiving(LivingID);

                //small class
                if (entity.LivingType == 1 || entity.LivingType == 2)
                {
                    // watch living record
                    if (DateTime.Now >= entity.EndTime)
                    {
                        var records = livingProvider.GetRecordList(LivingID);
                        if (null != records && records.Count > 0)
                        {
                            StudyRecord(entity);
                            records = records.OrderByDescending(f => f.duration).ToList();
                            this.Response.Redirect(records[0].playpath, true);
                        }
                    }// enter living room
                    else
                    {
                        StudyRecord(entity);
                        this.Response.Redirect(livingProvider.EnterTKRoom(LivingID, UserName, string.Empty, 2), true);
                    }

                }// big calss
                else if (entity.LivingType == 3)
                {
                    // watch living record
                    if (DateTime.Now >= entity.EndTime)
                    {                        
                        var records = livingProvider.MTCloudPlaybackRoom(LivingID, UserName, UserName, MTCloud.ROLE_USER);
                        CourseAccessResult model = JsonHelper.DeserializeObject<CourseAccessResult>(records);
                        if (model != null && model.data != null)
                        {
                            if (!string.IsNullOrEmpty(model.data.playbackUrl))
                                StudyRecord(entity);
                            this.Response.Redirect(model.data.playbackUrl, true);
                        }
                    }// enter living room
                    else
                    {                        
                        var result = livingProvider.EnterMTCloudRoom(LivingID, UserName, UserName, MTCloud.ROLE_USER);
                        CourseAccessResult record = JsonHelper.DeserializeObject<CourseAccessResult>(result);
                        if (record != null && record.data != null)
                        {
                            if (!string.IsNullOrEmpty(record.data.liveUrl))
                                StudyRecord(entity);
                            this.Response.Redirect(record.data.liveUrl, true);
                        }                    
                    }
                }//unknow
                else
                {
                    //do nothing
                }
            }
        }

        private void StudyRecord(Res_Living entity)
        {
            if (UserContext.Current != null && UserContext.Current.UserID != 0)
            {
                if (DateTime.Now.AddMinutes(30) > entity.StartTime)
                {
                    new StudentOnlinePlaying().CreateUserLiving(UserContext.Current.UserID.ToString(), LivingID);
                }
            }
        }
    }
}