//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-03-31 21:47:27.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Transactions;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course.Teacher;
using ETMS.Components.Basic.Implement.DAL.Course.Teacher;
namespace ETMS.Components.Basic.Implement.BLL.Course.Teacher
{
    /// <summary>
    /// 讲师授课课程关系表业务逻辑
    /// </summary>
    public partial class Res_TeacherCourseLogic
	{
		private static readonly Res_TeacherCourseDataAccess DAL = new Res_TeacherCourseDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Res_TeacherCourse res_TeacherCourse)
		{
			DAL.Add(res_TeacherCourse);
            BizLogHelper.AddOperate(res_TeacherCourse);
		}

		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid teacherCourseID)
		{
			DAL.Remove(teacherCourseID);
            //记录删除日志（根据ID删除）
            BizLogHelper.Operate(teacherCourseID,"删除");
		} 

		/// <summary>
		/// 批量删除(主键ID数组）
		/// </summary>
		public void Remove(Guid[] teacherCourseIDs )
		{
#if DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
				foreach (Guid id in teacherCourseIDs  )
				{
					Remove(id);
				}
#if DEBUG
					
				ts.Complete();
			}
#endif
		} 
    
		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Res_TeacherCourse res_TeacherCourse)
		{
            //修改前信息
            Res_TeacherCourse originalEntity=GetById(res_TeacherCourse.TeacherCourseID);
			DAL.Save(res_TeacherCourse);
            BizLogHelper.UpdateOperate(originalEntity,res_TeacherCourse);
		}
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Res_TeacherCourse GetById(Guid teacherCourseID)
		{
			Res_TeacherCourse res_TeacherCourse = DAL.GetById(teacherCourseID);
			if (res_TeacherCourse == null)
			{
				throw new ETMS.AppContext.BusinessException("TeacherCourse.Res_TeacherCourse.NotFoundException",new object[]{teacherCourseID});
			}
			
			return res_TeacherCourse;
		}				 				
	}		
}

