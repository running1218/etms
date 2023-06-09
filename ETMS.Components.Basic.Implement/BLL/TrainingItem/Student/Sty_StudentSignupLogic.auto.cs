//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-5 19:34:56.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Student;
using ETMS.Components.Basic.Implement.DAL.TrainingItem.Student;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Student
{
    /// <summary>
    /// 学员报名表业务逻辑
    /// </summary>
    public partial class Sty_StudentSignupLogic
	{
		private static readonly Sty_StudentSignupDataAccess DAL = new Sty_StudentSignupDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Sty_StudentSignup sty_StudentSignup)
		{
			DAL.Add(sty_StudentSignup);
            BizLogHelper.AddOperate(sty_StudentSignup);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid studentSignupID)
		{
            doRemove(studentSignupID);
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] studentSignupIDs)
		{
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in studentSignupIDs  )
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
		public void Update(Sty_StudentSignup sty_StudentSignup)
		{
            //修改前信息
            Sty_StudentSignup originalEntity=GetById(sty_StudentSignup.StudentSignupID);
			DAL.Save(sty_StudentSignup);
            BizLogHelper.UpdateOperate(originalEntity,sty_StudentSignup);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Sty_StudentSignup GetById(Guid studentSignupID)
		{
			Sty_StudentSignup sty_StudentSignup = DAL.GetById(studentSignupID);
			if (sty_StudentSignup == null)
			{
				throw new ETMS.AppContext.BusinessException("TrainingItem.Student.Sty_StudentSignup.NotFoundException",new object[]{studentSignupID});
			}
			
			return sty_StudentSignup;
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

