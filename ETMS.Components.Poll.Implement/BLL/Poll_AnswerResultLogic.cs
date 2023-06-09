using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// 调查答案表业务逻辑
    /// </summary>
    public partial class Poll_AnswerResultLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Poll_AnswerResult poll_AnswerResult)
		{
            try
            {
			    if(poll_AnswerResult.AnswerResultID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    poll_AnswerResult.AnswerResultID=poll_AnswerResult.AnswerResultID.NewID();;
                    Add(poll_AnswerResult);
                }
                else
                {
                    Update(poll_AnswerResult);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Poll_AnswerResultCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Poll.Poll_AnswerResult.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Poll_AnswerResultName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Poll.Poll_AnswerResult.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Int32 answerResultID)
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
                    throw new ETMS.AppContext.BusinessException("Poll.Poll_AnswerResult.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}  
	}
	
	
}

