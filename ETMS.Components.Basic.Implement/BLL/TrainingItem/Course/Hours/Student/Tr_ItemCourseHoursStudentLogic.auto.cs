//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-4-23 21:23:01.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Hours.Student;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student
{
    /// <summary>
    /// 培训项目课程课时学员表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseHoursStudentLogic
	{
		private static readonly Tr_ItemCourseHoursStudentDataAccess DAL = new Tr_ItemCourseHoursStudentDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent)
		{
			DAL.Add(tr_ItemCourseHoursStudent);
            BizLogHelper.AddOperate(tr_ItemCourseHoursStudent);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent)
		{
            //修改前信息
            Tr_ItemCourseHoursStudent originalEntity=GetById(tr_ItemCourseHoursStudent.ItemCourseHoursStudentID);
			DAL.Save(tr_ItemCourseHoursStudent);
            BizLogHelper.UpdateOperate(originalEntity,tr_ItemCourseHoursStudent);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemCourseHoursStudentID)
		{
            doRemove(itemCourseHoursStudentID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] itemCourseHoursStudentIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in itemCourseHoursStudentIDs  )
				{
					Remove(id);
				}
#if !DEBUG
				ts.Complete();
			}
#endif
		} 
    
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_ItemCourseHoursStudent GetById(Guid itemCourseHoursStudentID)
		{
			Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent = DAL.GetById(itemCourseHoursStudentID);
			if (tr_ItemCourseHoursStudent == null)
			{
				throw new ETMS.AppContext.BusinessException("TrainingItem.Course.Hours.Student.Tr_ItemCourseHoursStudent.NotFoundException",new object[]{itemCourseHoursStudentID});
			}
			
			return tr_ItemCourseHoursStudent;
		}		
		 
		/// <summary>
        /// 查询数据列表分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
				
        /// <summary>
        /// 查询实体分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
		public IList<Tr_ItemCourseHoursStudent> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

