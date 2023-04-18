//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-04-18 22:30:53.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.DAL.StudyClass;
namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    /// <summary>
    /// 班级学员表业务逻辑
    /// </summary>
    public partial class Sty_ClassStudentLogic
	{
		private static readonly Sty_ClassStudentDataAccess DAL = new Sty_ClassStudentDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Sty_ClassStudent sty_ClassStudent)
		{
			DAL.Add(sty_ClassStudent);
            BizLogHelper.AddOperate(sty_ClassStudent);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Sty_ClassStudent sty_ClassStudent)
		{
            //修改前信息
            Sty_ClassStudent originalEntity=GetById(sty_ClassStudent.ClassStudentID);
			DAL.Save(sty_ClassStudent);
            BizLogHelper.UpdateOperate(originalEntity,sty_ClassStudent);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid classStudentID)
		{
            doRemove(classStudentID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] classStudentIDs)
		{
				foreach (Guid id in classStudentIDs  )
				{
					Remove(id);
				}
		} 
    
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Sty_ClassStudent GetById(Guid classStudentID)
		{
			Sty_ClassStudent sty_ClassStudent = DAL.GetById(classStudentID);
			if (sty_ClassStudent == null)
			{
				throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassStudent.NotFoundException",new object[]{classStudentID});
			}
			
			return sty_ClassStudent;
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
		public IList<Sty_ClassStudent> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

