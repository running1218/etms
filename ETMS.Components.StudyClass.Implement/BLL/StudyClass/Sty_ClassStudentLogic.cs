//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-04-18 22:30:53.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.API;
namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    /// <summary>
    /// 班级学员表业务逻辑
    /// </summary>
    public partial class Sty_ClassStudentLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Sty_ClassStudent sty_ClassStudent)
		{
            try
            {
			    if(sty_ClassStudent.ClassStudentID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    sty_ClassStudent.ClassStudentID=sty_ClassStudent.ClassStudentID.NewID();
                    Add(sty_ClassStudent);
                }
                else
                {
                    Update(sty_ClassStudent);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Sty_ClassStudentCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassStudent.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_ClassStudentName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassStudent.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid classStudentID)
		{
            try
            {
			     DAL.Remove(classStudentID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(classStudentID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(BizErrorDefine.StudyClass_Sty_Class_DataUsed);
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}

        /// <summary>
        /// 删除班级学员
        /// </summary>
        /// <param name="classStudentIDs"></param>
        public void Delete(Guid[] classStudentIDs)
        {
            foreach (Guid classStudentID in classStudentIDs)
            {
                // 先删除班级学员职务
                new Sty_ClassMonitorLogic().DeleteByClassStudentID(classStudentID);
                // 删除班级学员
                doRemove(classStudentID);
            }
        }
	}	
}

