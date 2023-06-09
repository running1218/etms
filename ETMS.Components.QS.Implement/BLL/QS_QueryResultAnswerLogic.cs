//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-23 11:37:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.QS.API.Entity;
namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 问答题作答结果表业务逻辑
    /// </summary>
    public partial class QS_QueryResultAnswerLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(QS_QueryResultAnswer qS_QueryResultAnswer)
		{
            try
            {
			    if(qS_QueryResultAnswer.AnswerResultID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    qS_QueryResultAnswer.AnswerResultID=qS_QueryResultAnswer.AnswerResultID.NewID();;
                    Add(qS_QueryResultAnswer);
                }
                else
                {
                    Update(qS_QueryResultAnswer);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_QS_QueryResultAnswerCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".QS_QueryResultAnswer.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_QS_QueryResultAnswerName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".QS_QueryResultAnswer.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid answerResultID)
		{
            try
            {
			     DAL.Remove(answerResultID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(answerResultID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(".QS_QueryResultAnswer.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}  
	}
	
	
}

