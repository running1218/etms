//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-8 14:21:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Notify;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// 消息类型（1：邮件 2：短信 3：站内信）业务逻辑
    /// </summary>
    public partial class Notify_MessageTypeLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Notify_MessageType notify_MessageType)
		{
            try
            {
			    if(notify_MessageType.MessageTypeID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    notify_MessageType.MessageTypeID=notify_MessageType.MessageTypeID.NewID();;
                    Add(notify_MessageType);
                }
                else
                {
                    Update(notify_MessageType);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Notify_MessageTypeCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageType.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Notify_MessageTypeName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageType.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Int16 messageTypeID)
		{
            try
            {
			     DAL.Remove(messageTypeID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(messageTypeID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageType.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}  
	}
	
	
}

