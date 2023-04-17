
using System;
using ETMS.Components.Basic.Implement.DAL.Bulletin;
namespace ETMS.Components.Basic.Implement.BLL.Bulletin
{
    /// <summary>
    /// 项目课程导学资料表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseMentorDataLogic
	{
		/// <summary>
        /// 根据导学资料编号和项目课程编号，删除项目课程导学资料关系
        /// </summary>
        /// <param name="articleID">导学资料编号</param>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        public void RemoveItemCourseMentorData(Int32 articleID, Guid trainingItemCourseID)
        {
            DAL.RemoveItemCourseMentorData(articleID, trainingItemCourseID);
        }



        /// <summary>
        /// 获取某个培训项目课程的可用导学资料总数（状态为“启用”）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
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

