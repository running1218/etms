﻿//==================================================================================================
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

using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class QS_QueryAreaLogic
    {

        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(QS_QueryArea queryAreaEntity)
        {
            try
            {
                if (queryAreaEntity.QueryAreaID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    queryAreaEntity.QueryAreaID = queryAreaEntity.QueryAreaID.NewID(); ;
                    Add(queryAreaEntity);
                }
                else
                {
                    Update(queryAreaEntity);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Poll_QueryAreaCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryArea.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Poll_QueryAreaName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryArea.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }



        /// <summary>
        /// 批量添加组织机构到问卷调查的发布范围表中
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="queryPublishID">问卷发布ID</param>
        /// <param name="areaType">发布范围类型,定义参见枚举EnumQueryAreaType</param>
        /// <param name="orgIDArray">要添加的组织机构数组,或者学生数组等,对应与areaType定义</param>
        /// <param name="createUserID">操作用户ID</param>
        /// <param name="createUser">操作用户名称</param>
        public void BatchAdd(Guid queryID,  EnumQueryAreaType areaType, string[] areaCodeArray, int createUserID, string createUser)
        {
            int noSuccessNum = 0;
            foreach (string areaCode in areaCodeArray)
            {
                try
                {
                    QS_QueryArea queryAreaEntity = new QS_QueryArea();
                    queryAreaEntity.QueryID = queryID;
                    queryAreaEntity.QueryAreaID = Guid.NewGuid();
                    queryAreaEntity.AreaType = areaType.ToString();
                    queryAreaEntity.AreaCode = areaCode;
                    queryAreaEntity.Creator = createUser;
                    queryAreaEntity.CreateTime = System.DateTime.Now;
                    Add(queryAreaEntity);
                }
                catch
                {
                    noSuccessNum++;
                }
            }
            if (noSuccessNum > 0)
            {
                string errorMsg = "添加完毕：当前要添加的记录数为“{0}”个，有“{1}”个添加不成功，原因可能是这些数据已经被添加！";
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, areaCodeArray.Length, noSuccessNum));
            }

        }



        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid queryAreaID)
        {
            try
            {
                DAL.Remove(queryAreaID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(queryAreaID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Poll_QueryArea.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }


        /// <summary>
        /// 查询某个问卷调查在某个组织机构下所有不是其发布围的组织机构信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgType">发布范围:1只是本组织机构,2本组织机构及下级组织机构,3仅所有下级组织机构</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByOrg(Guid queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetNoSelectInfoByOrg(queryID, orgID, orgType, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 查询所有问卷调查的发布范围是组织机构的组织机构信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByOrg(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetSelectInfoByOrg(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 查询某个问卷调查的发布范围是组织机构的组织机构信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectOrgInfoByQueryID(Guid queryID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format("  AND pq.QueryID = '{0}'", queryID);
            return GetSelectInfoByOrg(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }





        /// <summary>
        /// 查询某个问卷调查在某个组织机构下所有不是其发布围内的所有启用学员信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgType">发布范围:1只是本组织机构,2本组织机构及下级组织机构,3仅所有下级组织机构</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByStudent(Guid queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetNoSelectInfoByStudent(queryID, orgID, orgType, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }





        /// <summary>
        /// 查询问卷调查的发布对象为学员的所有学员信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByStudent(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetSelectInfoByStudent(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 查询某个问卷调查的发布对象为学员的所有学员信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectStudentInfoByQueryID(Guid queryID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format("  AND pq.QueryID = '{0}'", queryID);
            return GetSelectInfoByStudent(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }








    }


}

