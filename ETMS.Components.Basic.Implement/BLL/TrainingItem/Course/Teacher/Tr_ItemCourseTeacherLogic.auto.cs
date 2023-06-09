//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-10 9:15:05.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Teacher;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher
{
    /// <summary>
    /// 培训项目课程讲师表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseTeacherLogic
	{
		private static readonly Tr_ItemCourseTeacherDataAccess DAL = new Tr_ItemCourseTeacherDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_ItemCourseTeacher tr_ItemCourseTeacher)
		{
			DAL.Add(tr_ItemCourseTeacher);
            BizLogHelper.AddOperate(tr_ItemCourseTeacher);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemCourseTeacherID)
		{
            doRemove(itemCourseTeacherID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] itemCourseTeacherIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in itemCourseTeacherIDs  )
				{
					Remove(id);
				}
#if !DEBUG
				ts.Complete();
			}
#endif
		} 
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Tr_ItemCourseTeacher tr_ItemCourseTeacher)
		{
            //修改前信息
            Tr_ItemCourseTeacher originalEntity=GetById(tr_ItemCourseTeacher.ItemCourseTeacherID);
			DAL.Save(tr_ItemCourseTeacher);
            BizLogHelper.UpdateOperate(originalEntity,tr_ItemCourseTeacher);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_ItemCourseTeacher GetById(Guid itemCourseTeacherID)
		{
			Tr_ItemCourseTeacher tr_ItemCourseTeacher = DAL.GetById(itemCourseTeacherID);
			if (tr_ItemCourseTeacher == null)
			{
				throw new ETMS.AppContext.BusinessException("TrainingItem.Course.Teacher.Tr_ItemCourseTeacher.NotFoundException",new object[]{itemCourseTeacherID});
			}
			
			return tr_ItemCourseTeacher;
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
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

