//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-23 11:37:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.DAL;
namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 问卷调查题目选择题选项表业务逻辑
    /// </summary>
    public partial class QS_QueryTitleOptionLogic
    {



        /// <summary>
        /// 问卷调查题目选项保存
        /// </summary>
        /// <param name="entity">问卷调查题目选项实体</param>
        /// <param name="action">操作方法：添加或者修改</param>
        public void Save(QS_QueryTitleOption entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                {
                    //取当前问卷调查题目的最大选项序号
                    int maxTitleOptionNo = GetMaxTitleOptionNo(entity.TitleID);
                    //自动设置序号为下一个
                    entity.OptionNo = maxTitleOptionNo + 1;
                    Add(entity);
                }
                else if (action == OperationAction.Edit)
                {
                    Update(entity);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid optionID)
        {
            try
            {
                DAL.Remove(optionID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(optionID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message.ToUpper();
                if (errorMsg.IndexOf("FK_QS_QUERY_REF_206_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该调查问卷题目选项已有“作答结果”，不能删除！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }


        /// <summary>
        /// 获取某个调查问卷题目下的选项的当前最大序号
        /// </summary>
        /// <param name="titleID">调查问卷题目ID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleOptionNo(Guid titleID)
        {
            return DAL.GetMaxTitleOptionNo(titleID);
        }



        /// <summary>
        /// 获取问卷标题的所有选项详细信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GetQueryTitleOptionAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetQueryTitleOptionAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }




        /// <summary>
        /// 获取某个问卷调查标题的所有选项详细信息,按选项的顺序排序(不分页)
        /// </summary>
        /// <param name="titleID">调查问卷ID</param>
        public DataTable GetQueryTitleOptionAllInfoByTitle(Guid titleID)
        {
            int totalRecords = 0;
            string criteria = string.Format(" AND qto.TitleID='{0}'", titleID);
            string sortExpression = "qto.OptionNo";
            return GetQueryTitleOptionAllInfo(1, 10000, sortExpression, criteria, out  totalRecords);
        }


        public void RemoveByTitleID(Guid titleID)
        {
            new QS_QueryTitleOptionDataAccess().RemoveByTitleID(titleID);
        }

    }


}

