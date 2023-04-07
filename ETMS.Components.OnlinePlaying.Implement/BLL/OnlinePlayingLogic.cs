using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.DAL;
using ETMS.Utility.Logging;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.OnlinePlaying.Core;

namespace ETMS.Components.OnlinePlaying.Implement.BLL
{
    public partial class OnlinePlayingLogic
    {
        MTCloud live = new MTCloud();

        public OnlinePlayingLogic()
        {
        }

        private static readonly SaService proxyService = new SaService();
        private static readonly OnlinePlayingDataAccess DAL = new OnlinePlayingDataAccess();

        public LoginUser LoginUserCertification
        {
            get
            {
                return new LoginUser()
                {
                    loginName = ProxyServiceConfig.OnlinePlayingUser,
                    password = ProxyServiceConfig.OnlinePlayingPassword,
                    site = ProxyServiceConfig.OnlinePlayingSite
                };
            }
        }

        public void Save(Tr_CourseOnlinePlaying entity, OperationAction action)
        {
            if (action == OperationAction.Add)
            {
                this.CreateOnlinePlaying(entity);
            }
            else if (action == OperationAction.Edit)
            {
                this.UpdateOnlinePlaying(entity);
            }
        }

        /// <summary>
        /// create online playing config
        /// </summary>
        /// <param name="entity"></param>
        private void CreateOnlinePlaying(Tr_CourseOnlinePlaying entity)
        {            
            WebcastCreation webcast = new WebcastCreation() {
                subject = entity.PlayingSubject,
                startTime = entity.StartTime,
                loginRequired = false,  
                description = entity.Description,
                plan = string.Empty,
                speakerInfo = string.Empty,
                videoLevel = entity.WindowSize
            };

            var webcastCreationResult = proxyService.createWebcast(LoginUserCertification, webcast);

            if (webcastCreationResult.code == "0")
            {
                var webcastInfoResult = proxyService.getWebcastSetting(LoginUserCertification, webcastCreationResult.id);

                entity.OnlinePlayingID = webcastInfoResult.id;
                entity.PlayingNo = webcastCreationResult.number;
                entity.OrganizerJoinUrl = webcastInfoResult.organizerJoinUrl;
                entity.PanelistJoinUrl = webcastInfoResult.panelistJoinUrl;
                entity.AttendeeJoinUrl = webcastInfoResult.attendeeJoinUrl;
                entity.AttendeeToken = webcastInfoResult.attendeeToken;
                entity.OrganizerToken = webcastInfoResult.organizerToken;
                entity.PanelistToken = webcastInfoResult.panelistToken;

                DAL.CreateOnlinePlaying(entity);
            }
            else
            {
                BizLogHelper.Operate(string.Format("Method:CreateOnlinePlaying; Subject:{0}", entity.PlayingSubject), webcastCreationResult.code);
            }
        }

        /// <summary>
        /// update online playing config
        /// </summary>
        /// <param name="entity"></param>
        private void UpdateOnlinePlaying(Tr_CourseOnlinePlaying entity)
        {
            var webcastInfoResult = proxyService.getWebcastSetting(LoginUserCertification, entity.OnlinePlayingID);
            WebcastModification webcastModification = new WebcastModification(){
                id = webcastInfoResult.id,
                attendeeToken = webcastInfoResult.attendeeToken,
                description = entity.Description,
                hasVideo = webcastInfoResult.hasVideo,
                hasVideoSpecified = webcastInfoResult.hasVideoSpecified,
                loginRequired = webcastInfoResult.loginRequired,
                loginRequiredSpecified = webcastInfoResult.loginRequiredSpecified,
                opened = webcastInfoResult.opened,
                openedSpecified = webcastInfoResult.openedSpecified,
                organizerToken = webcastInfoResult.organizerToken,
                panelistToken = webcastInfoResult.panelistToken,
                plan = webcastInfoResult.plan,
                skin = webcastInfoResult.skin,
                speakerInfo = webcastInfoResult.speakerInfo,
                startTime = entity.StartTime,
                startTimeSpecified = webcastInfoResult.startTimeSpecified,
                subject = entity.PlayingSubject,
                switchClient = webcastInfoResult.switchClient,
                switchClientSpecified = webcastInfoResult.switchClientSpecified,
                videoFps = webcastInfoResult.videoFps,
                videoFpsSpecified = webcastInfoResult.videoFpsSpecified,
                videoLevel = entity.WindowSize,
                videoLevelSpecified = webcastInfoResult.videoLevelSpecified
            };
            WSResult wsResult = proxyService.updateWebcast(LoginUserCertification, webcastModification);

            if (wsResult.code == "0")
            {
                DAL.UpdateOnlinePlaying(entity);
            }
            else
            {
                BizLogHelper.Operate(string.Format("Method:UpdateOnlinePlaying; ID:{0}", entity.OnlinePlayingID), wsResult.message);
            }
        }

        /// <summary>
        /// end online playing
        /// </summary>
        /// <param name="onlinePlayingID"></param>
        public void FinishOnlinePlaying(string onlinePlayingID)
        {
            WSResult wsResult = proxyService.finishWebcast(LoginUserCertification, onlinePlayingID);

            if (wsResult.code == "0")
            {
                var entity = this.GetOnlinePlaying(onlinePlayingID);
                entity.OnlineStatus = false;
                DAL.UpdateOnlinePlaying(entity);
            }
        }

        /// <summary>
        /// delete online playing config
        /// </summary>
        /// <param name="onlinePlayingID"></param>
        public void DeleteOnlinePlaying(string onlinePlayingID)
        {
            WSResult wsResult = proxyService.deleteWebcast(LoginUserCertification, onlinePlayingID);

            if (wsResult.code == "0")
            {
                DAL.DeleteOnlinePlaying(onlinePlayingID);
            }
            else
            {
                BizLogHelper.Operate(string.Format("Method:DeleteOnlinePlaying; ID:{0}", onlinePlayingID), wsResult.message);
            }
        }

        /// <summary>
        /// get item course online playing list
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Tr_CourseOnlinePlaying> GetOnlinePlayingList(Guid trainingItemCourseID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetOnlinePlayingByItemCourseID(trainingItemCourseID).PageList<Tr_CourseOnlinePlaying>(pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// get online playing config
        /// </summary>
        /// <param name="onlinePlayingID"></param>
        /// <returns></returns>
        public Tr_CourseOnlinePlaying GetOnlinePlaying(string onlinePlayingID)
        {
            var source = DAL.GetOnlinePlaying(onlinePlayingID);

            if (null != source && source.Rows.Count > 0)
            {
                return source.Rows[0].ToEntity<Tr_CourseOnlinePlaying>();
            }

            return null;
        }

        public List<OnlinePlayingInfo> QueryOnlinePlaying(OnlinePlayingInfo entity, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.QueryOnlinePlaying(entity).PageList<OnlinePlayingInfo>(pageIndex, pageSize, out totalRecords);
        }

        #region 直播课程 2017

        public void CreateLiving(Res_Living entity)
        {
            if (entity.LivingType == 3)
            {
                var result = live.courseAdd(
                                    entity.LivingName,
                                    entity.Account,
                                    entity.StartTime.ToString(),
                                    entity.EndTime.ToString(),
                                    entity.NikeName,
                                    string.Empty,
                                    null
                                );

                LivingResult liveResult = JsonHelper.DeserializeObject<LivingResult>(result);

                if (liveResult != null && liveResult.code == "0")
                {
                    entity.BID = liveResult.data.bid;
                    entity.LivingID = liveResult.data.course_id;
                    entity.PartnerID = liveResult.data.partner_id;
                    entity.AnchorKey = liveResult.data.zhubo_key;
                    entity.AssistantKey = liveResult.data.admin_key;
                    entity.StudentKey = liveResult.data.user_key;
                }
                else
                {
                    throw new BusinessException(liveResult.msg);
                }
            }
            else
            {
                try
                {
                    entity.LivingID = new LivingProvider().CreateTKLiving(entity);
                }
                catch (BusinessException bex)
                {
                    throw bex;
                }
            }

            DAL.CreateLiving(entity);
            BizLogHelper.AddOperate(entity);
        }

        public void UpdateLiving(Res_Living entity)
        {
            if (entity.LivingType == 3)
            {
                var result = live.courseUpdate(entity.LivingID, entity.Account, entity.LivingName, entity.StartTime.ToString(), entity.EndTime.ToString(), entity.NikeName);
                LivingResult2 liveResult = JsonHelper.DeserializeObject<LivingResult2>(result);

                if (liveResult == null || liveResult.code != "0")
                {
                    throw new BusinessException(liveResult.msg);
                }
            }
            else {
                try
                {
                    new LivingProvider().UpdateTKLiving(entity);
                }
                catch (BusinessException bex)
                {
                    throw bex;
                }
            }

            DAL.UpdateLiving(entity);
        }

        public void DeleteLiving(string livingID)
        {
            var living = new OnlinePlayingLogic().GetLiving(livingID);

            if (living.LivingType == 3)
            {
                var result = live.courseDelete(livingID);
                var liveResult = JsonHelper.DeserializeObject<LivingResult2>(result);

                if (liveResult == null || liveResult.code != "0")
                {
                    throw new BusinessException(liveResult.msg);                    
                }
            }
            else
            {
                try
                {
                    new LivingProvider().DeleteTKLiving(livingID);
                }
                catch (BusinessException bex)
                {
                    throw bex;
                }
            }

            DAL.DeleteLiving(livingID);
        }

        public int DeleteLivingByCourse(Guid courseID)
        {
            var livings = DAL.GetLivingPageList(courseID).ToList<Res_Living>();
            foreach (var item in livings)
            {
                live.courseDelete(item.LivingID);
            }

            return DAL.DeleteLivingByCourse(courseID);
        }

        public Res_Living GetLiving(string livingID)
        {
            return DAL.GetLiving(livingID).ToList<Res_Living>()[0];
        }

        public List<Res_Living> GetLivingPageList(Guid courseID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetLivingPageList(courseID).PageList<Res_Living>(pageIndex, pageSize, out totalRecords);
        }

        public List<Res_Living> GetLivingByCourseID(Guid courseID)
        {
            return DAL.GetLivingPageList(courseID).ToList<Res_Living>();
        }

        public List<Res_Living> GetLivingsByTeacher(int teacherID, DateTime startTime, DateTime endTime, string livingName, string courseName, int livingType, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetLivingsByTeacher(teacherID, startTime, endTime, livingName, courseName, livingType).PageList<Res_Living>(pageIndex, pageSize, out totalRecords);
        }
        #endregion
    }
}
