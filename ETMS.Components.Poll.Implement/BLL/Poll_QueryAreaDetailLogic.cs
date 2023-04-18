//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-23 15:08:15.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Poll.API.Entity;
using System.Transactions;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Poll_QueryAreaDetailLogic
    {
        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Poll_QueryAreaDetail poll_QueryAreaDetail)
        {
            try
            {
                if (poll_QueryAreaDetail.QueryAreaDetailID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    poll_QueryAreaDetail.QueryAreaDetailID = poll_QueryAreaDetail.QueryAreaDetailID.NewID(); ;
                    Add(poll_QueryAreaDetail);
                }
                else
                {
                    Update(poll_QueryAreaDetail);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Poll_QueryAreaDetailCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryAreaDetail.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Poll_QueryAreaDetailName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryAreaDetail.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected int doRemove(Int32 queryAreaDetailID)
        {
            try
            {
                int i = DAL.Remove(queryAreaDetailID);
                //记录删除日志（根据ID删除）DeleteOperate
                BizLogHelper.Operate(queryAreaDetailID, "删除");
                return i;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryAreaDetail.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        public void AddAllStudent(string sortExpression, string criteria, int queryAreaID, string creator)
        {
            DAL.AddAllStudent(sortExpression, criteria, queryAreaID, creator);
        }

        public int DeleteAllStudent(string criteria, int queryAreaID, out int totalCount)
        {
            return DAL.DeleteAllStudent(criteria, queryAreaID, out totalCount);
        }

        /// <summary>
        /// 根据问卷ID获取调查对象明细
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetQueryAreaOrQueryAreaDetailPagedList(int queryID, string sType, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            return DAL.GetQueryAreaOrQueryAreaDetailPagedList(queryID, sType, pageIndex, pageSize, sortExpression, out totalRecords);
        }


        /// <summary>
        /// 删除
        /// </summary>
        public int doRemoveItem(Int32 queryAreaDetailID)
        {
            try
            {
                int i = DAL.RemoveItem(queryAreaDetailID);
                //记录删除日志（根据ID删除）DeleteOperate
                BizLogHelper.Operate(queryAreaDetailID, "删除");
                return i;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryAreaDetail.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Remove(Int32 queryAreaDetailID, int queryID)
        {
            try
            {
                int i = DAL.Remove(queryAreaDetailID, queryID);
                //记录删除日志（根据ID删除）DeleteOperate
                BizLogHelper.Operate(queryAreaDetailID, "删除");
                return i;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryAreaDetail.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }


        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] queryAreaDetailIDs, int queryID, out int extCount)
        {
            extCount = 0;
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif

            foreach (Int32 id in queryAreaDetailIDs)
            {
                int i = Remove(id, queryID);
                if (i > 0)
                {
                    extCount++;
                }
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }
    }


}

