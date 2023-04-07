//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-5-6 11:46:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.IDP.API.Entity;
namespace ETMS.Components.IDP.Implement.BLL
{
    /// <summary>
    /// IDP计划学习内容明细表业务逻辑
    /// </summary>
    public partial class IDP_PlanContentDetailLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(IDP_PlanContentDetail iDP_PlanContentDetail)
		{
            try
            {
			    if(iDP_PlanContentDetail.PlanContentDetailID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    iDP_PlanContentDetail.PlanContentDetailID=iDP_PlanContentDetail.PlanContentDetailID.NewID();;
                    Add(iDP_PlanContentDetail);
                }
                else
                {
                    Update(iDP_PlanContentDetail);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_IDP_PlanContentDetailCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("IDP.IDP_PlanContentDetail.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_IDP_PlanContentDetailName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("IDP.IDP_PlanContentDetail.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid planContentDetailID)
		{
            try
            {
			     DAL.Remove(planContentDetailID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(planContentDetailID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("IDP.IDP_PlanContentDetail.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}  
	}
	
	
}

