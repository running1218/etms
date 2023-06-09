//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-16 19:58:03.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.ExOfflineHomework.API.Entity;
namespace ETMS.Components.ExOfflineHomework.Implement.BLL
{
    /// <summary>
    /// 学生离线作业表业务逻辑
    /// </summary>
    public partial class Sty_StudentOffLineJobLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Sty_StudentOffLineJob sty_StudentOffLineJob)
		{
            try
            {
			    if(sty_StudentOffLineJob.StudentJobID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    sty_StudentOffLineJob.StudentJobID=sty_StudentOffLineJob.StudentJobID.NewID();;
                    Add(sty_StudentOffLineJob);
                }
                else
                {
                    Update(sty_StudentOffLineJob);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Sty_StudentOffLineJobCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".Sty_StudentOffLineJob.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_StudentOffLineJobName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".Sty_StudentOffLineJob.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid studentJobID)
		{
            try
            {
			     DAL.Remove(studentJobID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(studentJobID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".Sty_StudentOffLineJob.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}  
	}
	
	
}

