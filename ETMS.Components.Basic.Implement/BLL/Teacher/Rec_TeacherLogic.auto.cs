using System.Collections.Generic;

using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.DAL.Teacher;
using System.Data;

namespace ETMS.Components.Basic.Implement.BLL.Teacher
{
    /// <summary>
    /// 课程推荐表业务逻辑
    /// </summary>
    public partial class Rec_TeacherLogic
    {
        private static readonly Rec_TeacherDataAccess DAL = new Rec_TeacherDataAccess();
        /// <summary>
        /// 添加推荐课程
        /// </summary>
        /// <param name="List<Rec_Teacher>"></param>
        public void AddRecTeacher(List<Rec_Teacher> list)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Rec_Teacher itemTeacher in list)
            {
                try
                {
                    //如果添加不成功，继续添加下一条
                    Add(itemTeacher);
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
                errorMsgALL = "添加完毕：当前要添加的推荐讲师数为“{0}”个，有“{1}”个添加不成功，原因如下：" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, list.Count, noSuccessNum));
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Rec_Teacher rec_Teacher)
        {
            DAL.Add(rec_Teacher);
            BizLogHelper.AddOperate(rec_Teacher);
        }
        /// <summary>
		/// 删除
		/// </summary>
		public void Remove(int teacherID)
        {
            Rec_Teacher rec_Teacher = DAL.GetById(teacherID);
            DAL.Remove(teacherID);
            BizLogHelper.DeleteOperate(rec_Teacher);
        }
        /// <summary>
		/// 修改
		/// </summary>
		public void Update(Rec_Teacher rec_Teacher)
        {
            DAL.Update(rec_Teacher);

            Rec_Teacher new_rec_Teacher = DAL.GetById(rec_Teacher.TeacherID);
            BizLogHelper.UpdateOperate(rec_Teacher, new_rec_Teacher);
        }
        /// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Rec_Teacher GetById(int teacherID)
        {
            return DAL.GetById(teacherID);
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
        /// 获取未推荐的讲师（分页，可排序）
        /// </summary>
        public DataTable GetNoRecommendTeacherPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetNoRecommendTeacherPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }
        /// <summary>
        /// 获取最大的顺序号
        /// </summary>
        public int GetRecommendTeacherMaxSort()
        {
            return DAL.GetRecommendTeacherMaxSort();
        }
        /// <summary>
        /// 根据讲师ID修改推荐讲师排序号
        /// </summary>
        /// <param name="TeacherID">讲师ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByTeacherID(int TeacherID, int orderNum)
        {
            DAL.UpdateOrderNumByTeacherID(TeacherID, orderNum);
        }
        /// <summary>
        /// 获取名师风采列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetFamousTeacherPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetFamousTeacherPagedList(pageIndex,pageSize,sortExpression,criteria,out totalRecords);
        }
        /// <summary>
        /// 根据教师ID获取教师详情
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <returns></returns>
        public DataTable GetFamousTeacherInfoByID(int TeacherID)
        {
            return DAL.GetFamousTeacherInfoByID(TeacherID);
        }
   }
}


