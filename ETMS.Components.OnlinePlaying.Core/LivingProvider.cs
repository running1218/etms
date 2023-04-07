using ETMS.AppContext;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Utility;
using System.Collections.Generic;

namespace ETMS.Components.OnlinePlaying.Core
{
    public class LivingProvider
    {
        MTCloud live = new MTCloud();
        TKRoom tkLive = new TKRoom();

        #region common method
        public string CreateLiving(Res_Living entity)
        {
            try
            {
                if (entity.LivingType == 3)
                {
                    return CreateMTCloudRoom(entity);
                }
                else {
                    return CreateTKLiving(entity);
                }
            }
            catch (BusinessException bex)
            {
                throw bex;
            }
        }
        public void UpdateLiving(Res_Living entity)
        {
            try
            {
                if (entity.LivingType == 3)
                {
                    UpdateMTCloudRoom(entity);
                }
                else
                {
                    UpdateTKLiving(entity);
                }
            }
            catch (BusinessException bex)
            {
                throw bex;
            }
        }

        public void DeleteLiving(string livingID, int livingTypeID)
        {
            try
            {
                if (livingTypeID == 3)
                {
                    DeleteMTCloudLiving(livingID);
                }
                else
                {
                    DeleteTKLiving(livingID);
                }
            }
            catch (BusinessException bex)
            {
                throw bex;
            }
        }
        /// <summary>
        /// 进入直播间
        /// </summary>
        /// <param name="livingID"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="nikeName"></param>
        /// <param name="role"></param>
        /// <param name="livingType"></param>
        /// <returns></returns>
        public string EnterRoom(string livingID, string userID, string password, string nikeName, string role, int livingType)
        {
            if (livingType == 3)
            {
                return EnterMTCloudRoom(livingID, userID, nikeName, role);
            }
            else
            {
                return EnterTKRoom(livingID, nikeName, password, role.ToInt());
            }
        }       
        #endregion

        #region MTCloud 大班直播
        /// <summary>
        /// 创建直播间
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateMTCloudRoom(Res_Living entity)
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
                return liveResult.data.course_id;
            }
            else
            {
                throw new BusinessException(liveResult.msg);
            }
        }
        /// <summary>
        /// 更新直播间
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMTCloudRoom(Res_Living entity)
        {
            var result = live.courseUpdate(entity.LivingID, entity.Account, entity.LivingName, entity.StartTime.ToString(), entity.EndTime.ToString(), entity.NikeName);
            LivingResult2 liveResult = JsonHelper.DeserializeObject<LivingResult2>(result);
            if (liveResult == null || liveResult.code != "0")
            {
                throw new BusinessException(liveResult.msg); 
            }
        }
        /// <summary>
        /// 删除直播间
        /// </summary>
        /// <param name="livingID"></param>
        public void DeleteMTCloudLiving(string livingID)
        {
            var result = live.courseDelete(livingID);
            var liveResult = JsonHelper.DeserializeObject<LivingResult2>(result);

            if (liveResult == null || liveResult.code != "0")
            {
                throw new BusinessException("操作失败，请与管理员联系！");
            }
        }
        /// <summary>
        /// 进入直播间
        /// </summary>
        /// <param name="livingID"></param>
        /// <param name="userID"></param>
        /// <param name="nikeName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string EnterMTCloudRoom(string livingID, string userID, string nikeName, string role)
        {
            return new MTCloud().courseAccess(livingID, userID, nikeName, role, 7200, null);
        }
        /// <summary>
        /// 直播间回放
        /// </summary>
        /// <param name="livingID"></param>
        /// <param name="userID"></param>
        /// <param name="nikeName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string MTCloudPlaybackRoom(string livingID, string userID, string nikeName, string role)
        {
            return new MTCloud().courseAccessPlayback(livingID, userID, nikeName, role, 7200, null);
        }
        /// <summary>
        /// 启动直播间课堂
        /// </summary>
        /// <param name="livingID"></param>
        /// <returns></returns>
        public string MTCloudLaunch(string livingID)
        {
            string data = live.courseLaunch(livingID);
            CourseLaunchResult result = JsonHelper.DeserializeObject<CourseLaunchResult>(data);

            if (result.code == "0")
            {
                return result.data.url;
            }

            return string.Empty;
        }
        #endregion

        #region TK 小班直播
        public string CreateTKLiving(Res_Living entity)
        {
            var result = tkLive.CreateRoom(
                                entity.LivingName,
                                3,
                                entity.StartTime,
                                entity.EndTime
                            );

            TKResult liveResult = JsonHelper.DeserializeObject<TKResult>(result);

            if (liveResult != null && liveResult.result == 0)
            {
                return liveResult.serial;
            }
            else
            {
                throw new BusinessException(liveResult.result.ToString());
            }
        }
        public void UpdateTKLiving(Res_Living entity)
        {
            var result = tkLive.ModifyRoom(
                    entity.LivingID,
                    entity.LivingName,
                    3,
                    entity.StartTime,
                    entity.EndTime
                );

            TKResult liveResult = JsonHelper.DeserializeObject<TKResult>(result);

            if (liveResult == null || liveResult.result != 0)
            {
                throw new BusinessException(liveResult.result.ToString());
            }
        }
        public void DeleteTKLiving(string livingID)
        {
            var result = tkLive.DeleteRoom(livingID);
            var liveResult = JsonHelper.DeserializeObject<TKResult>(result);

            if (liveResult == null || liveResult.result != 0)
            {
                throw new BusinessException(liveResult.result.ToString());
            }
        }
        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="username"></param>
        /// <param name="usertype"> 0：主讲(老师 ) 1：助教 2: 学员 3：直播用户 4:巡检员 默认值为 2</param>
        /// <returns></returns>
        public string EnterTKRoom(string serial, string username, string password, int usertype)
        {
            return tkLive.EntryRoom(serial, username, password, usertype);
        }
        public List<TKRecordList> GetRecordList(string serial)
        {
            var history = JsonHelper.DeserializeObject<TKRecordResult>(tkLive.GetRecordList(serial));

            if (history != null && history.result == 0 && history.recordlist.Count > 0)
            {
                return history.recordlist;
            }
            else
                return null;
        }
        #endregion
    }
}
