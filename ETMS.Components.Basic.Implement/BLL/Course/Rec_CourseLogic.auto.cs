using System;
using System.Collections.Generic;

using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.DAL.Course;
using System.Data;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    /// <summary>
    /// 课程推荐表业务逻辑
    /// </summary>
    public partial class Rec_CourseLogic
    {
        private static readonly Rec_CourseDataAccess DAL = new Rec_CourseDataAccess();
        /// <summary>
        /// 添加推荐课程
        /// </summary>
        /// <param name="List<Rec_Course>"></param>
        public void AddRecCourse(List<Rec_Course> list)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Rec_Course itemCourse in list)
            {
                try
                {
                    //如果添加不成功，继续添加下一条
                    Add(itemCourse);
                    SetResourceDefualtOpen(itemCourse.CourseID);
                }
                catch (System.Exception ex)
                {
                    noSuccessNum++;
                    if (errorMsgALL.IndexOf(ex.Message) < 0)
                        errorMsgALL += ex.Message;
                }
            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "添加完毕：当前要添加的推荐课程数为“{0}”个，有“{1}”个添加不成功，原因如下：" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, list.Count, noSuccessNum));
            }
        }

        private void SetResourceDefualtOpen(Guid courseID)
        {
            Res_ContentLogic logic = new Res_ContentLogic();
            int totalRecordCount = 0;
            int num = System.Configuration.ConfigurationManager.AppSettings["OpenResourceNum"].ToInt();
            DataTable dt = logic.GetPagedList(1, num == 0 ? 1 : num, " RCCR.[Sort] ASC ", "", courseID, out totalRecordCount);

            foreach (DataRow row in dt.Rows)
            {
                var contentID = row["ContentID"].ToGuid();
                var content = logic.GetByID(contentID);
                content.IsOpen = true;
                logic.Update(content);
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Rec_Course rec_Course)
        {
            DAL.Add(rec_Course);
            BizLogHelper.AddOperate(rec_Course);
        }
        /// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid courseID)
        {
            Rec_Course rec_Course = DAL.GetById(courseID);
            DAL.Remove(courseID);
            //RecoverRecommendCourseResource(courseID);
            BizLogHelper.DeleteOperate(rec_Course);
        }

        private void RecoverRecommendCourseResource(Guid courseID)
        {
            Res_ContentLogic logic = new Res_ContentLogic();
            int totalRecordCount = 0;
            DataTable dt = logic.GetPagedList(1, int.MaxValue -1, " RCCR.[Sort] ASC ", "", courseID, out totalRecordCount);

            foreach (DataRow row in dt.Rows)
            {
                var contentID = row["ContentID"].ToGuid();
                var content = logic.GetByID(contentID);
                content.IsOpen = false;
                logic.Update(content);
            }
        }
        /// <summary>
		/// 修改
		/// </summary>
		public void Update(Rec_Course rec_Course)
        {
            DAL.Update(rec_Course);
            //Rec_Course new_rec_Course = DAL.GetById(rec_Course.CourseID);
            //BizLogHelper.UpdateOperate(rec_Course, new_rec_Course);
        }
        /// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Rec_Course GetById(Guid courseID)
        {
            return DAL.GetById(courseID);
        }
       
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }
        /// <summary>
        /// 获取最大的顺序号
        /// </summary>
        public int GetRecommendCourseMaxSort()
        {
            return DAL.GetRecommendCourseMaxSort();
        }
        /// <summary>
        /// 根据课程ID修改推荐课程排序号
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByCourseID(Guid CourseID, int orderNum)
        {
            DAL.UpdateOrderNumByCourseID(CourseID, orderNum);
        }
        /// <summary>
        /// 获取点播课程列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDemandCoursePagedList(int pageIndex, int pageSize, string sortExpression, string criteria, int orgID, out int totalRecords)
        {
            return DAL.GetDemandCoursePagedList(pageIndex,pageSize,sortExpression,criteria, orgID, out totalRecords);
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCoursePagedList(int pageIndex, int pageSize, string sortExpression, int typeid, int classid, string searchContent,int orgID, out int totalRecords)
        {
            return DAL.GetCoursePagedList(pageIndex, pageSize, sortExpression, typeid, classid, searchContent, orgID, out totalRecords);
        }

        public void SetOpen(Guid courseID, List<string> ids)
        {
            RecoverRecommendCourseResource(courseID);
            foreach (string id in ids)
            {
                Res_ContentLogic logic = new Res_ContentLogic();
                var content = logic.GetByID(id.ToGuid());
                content.IsOpen = true;
                logic.Update(content);
            }
        }
    }
}


