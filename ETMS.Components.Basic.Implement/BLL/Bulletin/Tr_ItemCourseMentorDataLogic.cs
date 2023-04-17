
using System;
using ETMS.Components.Basic.Implement.DAL.Bulletin;
namespace ETMS.Components.Basic.Implement.BLL.Bulletin
{
    /// <summary>
    /// ��Ŀ�γ̵�ѧ���ϱ�ҵ���߼�
    /// </summary>
    public partial class Tr_ItemCourseMentorDataLogic
	{
		/// <summary>
        /// ���ݵ�ѧ���ϱ�ź���Ŀ�γ̱�ţ�ɾ����Ŀ�γ̵�ѧ���Ϲ�ϵ
        /// </summary>
        /// <param name="articleID">��ѧ���ϱ��</param>
        /// <param name="trainingItemCourseID">��ѵ��Ŀ�γ̱��</param>
        public void RemoveItemCourseMentorData(Int32 articleID, Guid trainingItemCourseID)
        {
            DAL.RemoveItemCourseMentorData(articleID, trainingItemCourseID);
        }



        /// <summary>
        /// ��ȡĳ����ѵ��Ŀ�γ̵Ŀ��õ�ѧ����������״̬Ϊ�����á���
        /// </summary>
        /// <param name="trainingItemCourseID">��ѵ��Ŀ�γ�ID</param>
        /// <returns></returns>
        public Int32 GetMentorDataTotalByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetMentorDataTotalByTrainingItemCourseID(trainingItemCourseID);
        }

        public Int32 GetGuidanceDataTotalByTrainingItemCourseID(Guid tradiningItemCourseID)
        {
            return new Inf_BulletinDataAccess().GetNoticeDatabyItemCourse(tradiningItemCourseID).Rows.Count;
        }



	}
	
	
}

