
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.DAL.Bulletin;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.Bulletin
{
    #region 导学资料
    /// <summary>
    /// 公告表业务逻辑
    /// </summary>
    public partial class Inf_BulletinLogic
    {

        Tr_ItemCourseMentorDataLogic tr_ItemCourseMentorDataLogic = new Tr_ItemCourseMentorDataLogic();
        

        /// <summary>
        /// 获取课程公告列表
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetCourseNoticeList(Guid TrainingItemCourseID)
        {
            var dal = new Inf_BulletinDataAccess();
            var dt = dal.GetCourseNoticeList(TrainingItemCourseID);
            return dt;
        }

        public DataTable GetCourseNoticeList(Guid TrainingItemCourseID, int articleType)
        {
            var dal = new Inf_BulletinDataAccess();
            var dt = dal.GetCourseNoticeList(TrainingItemCourseID, articleType);
            return dt;
        }

        /// <summary>
        /// 增加导学资料
        /// </summary>
        /// <param name="inf_Bulletin">导学资料实体</param>
        public void AddItemCourseMentorData(Inf_Bulletin inf_Bulletin)
        {
            //添加导学信息
            Add(inf_Bulletin);

            //构造导学资料与培训项目课程实体,缺省为启用
            Tr_ItemCourseMentorData tr_ItemCourseMentorData = new Tr_ItemCourseMentorData();
            tr_ItemCourseMentorData.ItemCourseMentorDataID = System.Guid.NewGuid();
            tr_ItemCourseMentorData.ArticleID = inf_Bulletin.ArticleID;
            tr_ItemCourseMentorData.TrainingItemCourseID = inf_Bulletin.TrainingItemCourseID;
            tr_ItemCourseMentorData.BeginTime = inf_Bulletin.BeginDate;
            tr_ItemCourseMentorData.EndTime = inf_Bulletin.EndDate;
            tr_ItemCourseMentorData.IsUse = 1;

            //维护导学资料与培训项目课程关系
            tr_ItemCourseMentorDataLogic.Add(tr_ItemCourseMentorData);
        }

        /// <summary>
        /// 删除导学资料
        /// </summary>
        /// <param name="articleID">导学资料编号</param>
        /// <param name="itemCourseMentorDataID">培训项目课程编号</param>
        public void RemoveItemCourseMentorData(Int32 articleID, Guid trainingItemCourseID)
        {
            //删除导学资料与培训项目课程关系
            tr_ItemCourseMentorDataLogic.RemoveItemCourseMentorData(articleID, trainingItemCourseID); ;

            //删除导学信息
            Remove(articleID);
        }

        /// <summary>
        /// 根据培训项目课程获取导学资料
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns>返回培训项目课程编号下导学资料列表</returns>
        public DataTable GetMentorDatabyItemCourse(Guid trainingItemCourseID)
        {
            return DAL.GetMentorDatabyItemCourse(trainingItemCourseID);
        }

        /// <summary>
        /// 根据项目课程获取公告
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetNoticeDatabyItemCourse(Guid trainingItemCourseID)
        {
            return DAL.GetNoticeDatabyItemCourse(trainingItemCourseID);
        }

        /// <summary>
        /// 根据培训项目课程获取导学资料数量
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public int GetMontorDataNumbyItemCourse(Guid trainingItemCourseID)
        {
            return GetMentorDatabyItemCourse(trainingItemCourseID).Rows.Count;
        }


        /// <summary>
        /// 设置导学资料启用状态
        /// </summary>
        /// <param name="articleID">导学资料编号</param>
        /// <param name="isUse">启用状态</param>
        /// <returns></returns>
        public void SetMontorDataIsUse(Int32 articleID, int isUse)
        {
            DAL.SetMontorDataIsUse(articleID, isUse);
        }

        /// <summary>
        /// 根据培训项目课程获取导学资料,用于前台
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public DataTable GetMontorDataByTrainintItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetMontorDataByTrainintItemCourseID(trainingItemCourseID);
        }
        #endregion

        #region 公司公告
        Inf_BulletinObjectLogic inf_BulletinObjectLogic = new Inf_BulletinObjectLogic();

        /// <summary>
        /// 根据机构获取公告列表
        /// </summary>
        /// <param name="OrgID">机构编号</param>
        /// <returns>返回所属机构的公告列表</returns>
        public DataTable GetBulletinByOrgID(int OrgID)
        {
            return DAL.GetBulletinByOrgID(OrgID);
        }

        /// <summary>
        /// 增加公告
        /// </summary>
        /// <param name="inf_Bulletin">公告实体</param>
        public void AddBulletin(Inf_Bulletin inf_Bulletin)
        {
            //增加公告
            Add(inf_Bulletin);

            //构造公告发布对象实体
            Inf_BulletinObject inf_BulletinObject = new Inf_BulletinObject();
            inf_BulletinObject.ArticleID = inf_Bulletin.ArticleID;
            inf_BulletinObject.BulletinObjectTypeID = inf_Bulletin.BulletinObjectTypeID;
            inf_BulletinObject.BulletinObjectID = System.Guid.NewGuid();
            inf_BulletinObject.BeginTime = inf_Bulletin.BeginDate;
            inf_BulletinObject.EndTime = inf_Bulletin.EndDate;
            inf_BulletinObject.IsUse = 1;
            inf_BulletinObject.CreateTime = System.DateTime.Now;

            //增加公告发布对象
            inf_BulletinObjectLogic.Add(inf_BulletinObject);
        }

        /// <summary>
        /// 编辑公告
        /// </summary>
        /// <param name="inf_Bulletin">公告实体</param>
        public void SaveBulletin(Inf_Bulletin inf_Bulletin)
        {
            //编辑公告实体
            Save(inf_Bulletin);

            //编辑公告发布对象
            inf_BulletinObjectLogic.SetBulletinObject(inf_Bulletin.ArticleID, inf_Bulletin.BulletinObjectTypeID);
        }


        /// <summary>
        /// 设置公告启用状态
        /// </summary>
        /// <param name="articleID">公告编号</param>
        /// <param name="isUse">启用状态</param>
        /// <returns></returns>
        public void SetBulletinIsUse(Int32 articleID, int isUse)
        {
            DAL.SetMontorDataIsUse(articleID, isUse);
        }

        /// <summary>
        /// 学习首页公告：通过机构编号获取公告列表
        /// </summary>
        /// <param name="orgID">机构编号</param>
        /// <returns></returns>
        public DataTable GetBulletinListToStudentByOrgID(int orgID)
        {
            return GetBulletinListToStudentByOrgID(orgID, 6);
        }

        /// <summary>
        /// 学习首页公告更多：通过机构编号获取公告列表
        /// </summary>
        /// <param name="orgID">机构编号</param>
        /// <returns></returns>
        public DataTable GetMoreBulletinListToStudentByOrgID(int orgID)
        {
            return GetBulletinListToStudentByOrgID(orgID, 0);
        }

        /// <summary>
        /// 首页学习公告：通过机构编号获取公告列表  
        /// </summary>
        /// <param name="orgID">机构编号</param>
        /// <param name="topNum">获取记录数:0代表全部</param>
        /// <returns></returns>
        public DataTable GetBulletinListToStudentByOrgID(int orgID, int topNum)
        {
            return DAL.GetBulletinListToStudentByOrgID(orgID, topNum);
        }

        public List<Inf_Bulletin> GetBulletinByOrgID(string orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Inf_Bulletin> source = new List<Inf_Bulletin>();
            string[] orgs = orgID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string org in orgs)
            {
                var orgBulletin = DAL.GetNoticeByOrgID(org.ToInt()).ToList<Inf_Bulletin>();
                foreach (var bulletin in orgBulletin)
                {
                    source.Add(bulletin);
                }
            }

            return source.OrderByDescending(f => f.IsTop).OrderByDescending(f => f.BeginDate).ToList().PageList<Inf_Bulletin>(pageIndex, pageSize, out totalRecords);
        }

        #endregion
    }
}

