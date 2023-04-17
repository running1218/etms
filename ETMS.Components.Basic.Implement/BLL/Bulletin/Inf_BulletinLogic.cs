
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.DAL.Bulletin;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.Bulletin
{
    #region ��ѧ����
    /// <summary>
    /// �����ҵ���߼�
    /// </summary>
    public partial class Inf_BulletinLogic
    {

        Tr_ItemCourseMentorDataLogic tr_ItemCourseMentorDataLogic = new Tr_ItemCourseMentorDataLogic();
        

        /// <summary>
        /// ��ȡ�γ̹����б�
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
        /// ���ӵ�ѧ����
        /// </summary>
        /// <param name="inf_Bulletin">��ѧ����ʵ��</param>
        public void AddItemCourseMentorData(Inf_Bulletin inf_Bulletin)
        {
            //��ӵ�ѧ��Ϣ
            Add(inf_Bulletin);

            //���쵼ѧ��������ѵ��Ŀ�γ�ʵ��,ȱʡΪ����
            Tr_ItemCourseMentorData tr_ItemCourseMentorData = new Tr_ItemCourseMentorData();
            tr_ItemCourseMentorData.ItemCourseMentorDataID = System.Guid.NewGuid();
            tr_ItemCourseMentorData.ArticleID = inf_Bulletin.ArticleID;
            tr_ItemCourseMentorData.TrainingItemCourseID = inf_Bulletin.TrainingItemCourseID;
            tr_ItemCourseMentorData.BeginTime = inf_Bulletin.BeginDate;
            tr_ItemCourseMentorData.EndTime = inf_Bulletin.EndDate;
            tr_ItemCourseMentorData.IsUse = 1;

            //ά����ѧ��������ѵ��Ŀ�γ̹�ϵ
            tr_ItemCourseMentorDataLogic.Add(tr_ItemCourseMentorData);
        }

        /// <summary>
        /// ɾ����ѧ����
        /// </summary>
        /// <param name="articleID">��ѧ���ϱ��</param>
        /// <param name="itemCourseMentorDataID">��ѵ��Ŀ�γ̱��</param>
        public void RemoveItemCourseMentorData(Int32 articleID, Guid trainingItemCourseID)
        {
            //ɾ����ѧ��������ѵ��Ŀ�γ̹�ϵ
            tr_ItemCourseMentorDataLogic.RemoveItemCourseMentorData(articleID, trainingItemCourseID); ;

            //ɾ����ѧ��Ϣ
            Remove(articleID);
        }

        /// <summary>
        /// ������ѵ��Ŀ�γ̻�ȡ��ѧ����
        /// </summary>
        /// <param name="trainingItemCourseID">��ѵ��Ŀ�γ̱��</param>
        /// <returns>������ѵ��Ŀ�γ̱���µ�ѧ�����б�</returns>
        public DataTable GetMentorDatabyItemCourse(Guid trainingItemCourseID)
        {
            return DAL.GetMentorDatabyItemCourse(trainingItemCourseID);
        }

        /// <summary>
        /// ������Ŀ�γ̻�ȡ����
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetNoticeDatabyItemCourse(Guid trainingItemCourseID)
        {
            return DAL.GetNoticeDatabyItemCourse(trainingItemCourseID);
        }

        /// <summary>
        /// ������ѵ��Ŀ�γ̻�ȡ��ѧ��������
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public int GetMontorDataNumbyItemCourse(Guid trainingItemCourseID)
        {
            return GetMentorDatabyItemCourse(trainingItemCourseID).Rows.Count;
        }


        /// <summary>
        /// ���õ�ѧ��������״̬
        /// </summary>
        /// <param name="articleID">��ѧ���ϱ��</param>
        /// <param name="isUse">����״̬</param>
        /// <returns></returns>
        public void SetMontorDataIsUse(Int32 articleID, int isUse)
        {
            DAL.SetMontorDataIsUse(articleID, isUse);
        }

        /// <summary>
        /// ������ѵ��Ŀ�γ̻�ȡ��ѧ����,����ǰ̨
        /// </summary>
        /// <param name="trainingItemCourseID">��ѵ��Ŀ�γ̱��</param>
        /// <returns></returns>
        public DataTable GetMontorDataByTrainintItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetMontorDataByTrainintItemCourseID(trainingItemCourseID);
        }
        #endregion

        #region ��˾����
        Inf_BulletinObjectLogic inf_BulletinObjectLogic = new Inf_BulletinObjectLogic();

        /// <summary>
        /// ���ݻ�����ȡ�����б�
        /// </summary>
        /// <param name="OrgID">�������</param>
        /// <returns>�������������Ĺ����б�</returns>
        public DataTable GetBulletinByOrgID(int OrgID)
        {
            return DAL.GetBulletinByOrgID(OrgID);
        }

        /// <summary>
        /// ���ӹ���
        /// </summary>
        /// <param name="inf_Bulletin">����ʵ��</param>
        public void AddBulletin(Inf_Bulletin inf_Bulletin)
        {
            //���ӹ���
            Add(inf_Bulletin);

            //���칫�淢������ʵ��
            Inf_BulletinObject inf_BulletinObject = new Inf_BulletinObject();
            inf_BulletinObject.ArticleID = inf_Bulletin.ArticleID;
            inf_BulletinObject.BulletinObjectTypeID = inf_Bulletin.BulletinObjectTypeID;
            inf_BulletinObject.BulletinObjectID = System.Guid.NewGuid();
            inf_BulletinObject.BeginTime = inf_Bulletin.BeginDate;
            inf_BulletinObject.EndTime = inf_Bulletin.EndDate;
            inf_BulletinObject.IsUse = 1;
            inf_BulletinObject.CreateTime = System.DateTime.Now;

            //���ӹ��淢������
            inf_BulletinObjectLogic.Add(inf_BulletinObject);
        }

        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="inf_Bulletin">����ʵ��</param>
        public void SaveBulletin(Inf_Bulletin inf_Bulletin)
        {
            //�༭����ʵ��
            Save(inf_Bulletin);

            //�༭���淢������
            inf_BulletinObjectLogic.SetBulletinObject(inf_Bulletin.ArticleID, inf_Bulletin.BulletinObjectTypeID);
        }


        /// <summary>
        /// ���ù�������״̬
        /// </summary>
        /// <param name="articleID">������</param>
        /// <param name="isUse">����״̬</param>
        /// <returns></returns>
        public void SetBulletinIsUse(Int32 articleID, int isUse)
        {
            DAL.SetMontorDataIsUse(articleID, isUse);
        }

        /// <summary>
        /// ѧϰ��ҳ���棺ͨ��������Ż�ȡ�����б�
        /// </summary>
        /// <param name="orgID">�������</param>
        /// <returns></returns>
        public DataTable GetBulletinListToStudentByOrgID(int orgID)
        {
            return GetBulletinListToStudentByOrgID(orgID, 6);
        }

        /// <summary>
        /// ѧϰ��ҳ������ࣺͨ��������Ż�ȡ�����б�
        /// </summary>
        /// <param name="orgID">�������</param>
        /// <returns></returns>
        public DataTable GetMoreBulletinListToStudentByOrgID(int orgID)
        {
            return GetBulletinListToStudentByOrgID(orgID, 0);
        }

        /// <summary>
        /// ��ҳѧϰ���棺ͨ��������Ż�ȡ�����б�  
        /// </summary>
        /// <param name="orgID">�������</param>
        /// <param name="topNum">��ȡ��¼��:0����ȫ��</param>
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

