//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Poll.API.Entity;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// ���ݷ���
    /// </summary>
    public partial class Poll_QueryAreaDataAccess
    {
        /// <summary>
        /// ��ȡ��ҳ�ʾ���������(�����ͨ�û�)
        /// </summary>
        /// <param name="resourceType">��Դ����</param>
        /// <returns>��ҳ�ʾ���������</returns>
        public DataTable GetQueryAreaForUserMode(string resourceType)
        {
            string commandName = "";
            if (resourceType == "R4")
            {
                commandName = "select * from vw_IndexQueryArea_UserMode where ResourceTypeCode='" + resourceType + "' order by Createtime desc";
            }
            else
            {
                commandName = "select * from vw_IndexQueryArea_UserMode where ResourceTypeCode='" + resourceType + "' order by AreaType";
            }

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName).Tables[0];
        }
        /// <summary>
        /// ��ȡ��ҳ�ʾ�����������(��Թ������û�)
        /// </summary>
        /// <param name="queryID">��Դ���</param>
        /// <param name="resourceType">��Դ����</param>
        /// <returns>��ҳ�ʾ�����������</returns>
        public List<Poll_QueryArea> GetQueryAreaByQueryIDForManagerMode(Int32 queryID, string resourceType)
        {
            string commandName = "dbo.Pr_tb_r_QueryArea_GetByQueryID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					 new SqlParameter("@QueryID", SqlDbType.Int),
                     new SqlParameter("@ResourceType", SqlDbType.VarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = resourceType;

            #endregion

            List<Poll_QueryArea> tb_r_QueryAreas = new List<Poll_QueryArea>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    tb_r_QueryAreas.Add(PopulatePoll_QueryAreaFromDataReader(dataReader));
                }
            }

            return tb_r_QueryAreas;
        }




        /// <summary>
        /// ��ѯĳ���ʾ�������ĳ����֯���������в����䷢��Χ����֯������Ϣ
        /// </summary>
        /// <param name="queryID">�ʾ�����ID</param>
        /// <param name="orgID">��֯����ID</param>
        /// <param name="orgType">������Χ:1ֻ�Ǳ���֯����,2����֯�������¼���֯����,3�������¼���֯����</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">�����ֶ�</param>
        /// <param name="criteria">��AND��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�������������ļ�¼��</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByOrg(int queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetNoSelectInfoByOrg]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = orgID;
            parms[2].Value = orgType;
            parms[3].Value = pageIndex;
            parms[4].Value = pageSize;
            parms[5].Value = sortExpression;
            parms[6].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[7].Value;
            return dt;

        }





        /// <summary>
        /// ��ѯĳ���ʾ�����ķ�����Χ����֯��������֯������Ϣ
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">�����ֶ�</param>
        /// <param name="criteria">��AND��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�������������ļ�¼��</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByOrg(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetSelectInfoByOrg]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;

        }




        /// <summary>
        /// ��ѯĳ���ʾ�������ĳ����֯���������в����䷢��Χ����֯������Ϣ
        /// </summary>
        /// <param name="queryID">�ʾ�����ID</param>
        /// <param name="orgID">��֯����ID</param>
        /// <param name="orgType">������Χ:1ֻ�Ǳ���֯����,2����֯�������¼���֯����,3�������¼���֯����</param>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">�����ֶ�</param>
        /// <param name="criteria">��AND��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�������������ļ�¼��</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByStudent(int queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetNoSelectInfoByStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = orgID;
            parms[2].Value = orgType;
            parms[3].Value = pageIndex;
            parms[4].Value = pageSize;
            parms[5].Value = sortExpression;
            parms[6].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[7].Value;
            return dt;

        }





        /// <summary>
        /// ��ѯ�ʾ�����ķ�������ΪѧԱ������ѧԱ��Ϣ
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="sortExpression">�����ֶ�</param>
        /// <param name="criteria">��AND��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�������������ļ�¼��</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByStudent(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetSelectInfoByStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;

        }


        /// <summary>
        /// ��ѯĳ���ʾ������Ѿ����õ��鷶Χ��ѧԱ��
        /// </summary>
        /// <param name="queryID">�ʾ�����ID</param>
        /// <returns></returns>
        public int GetStudentNumFromQueryArea(int queryID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'";
            string sql = string.Format(sqlModal, queryID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }


        /// <summary>
        /// ��ѯĳ���ʾ�������ĳ����֯���������õ��鷶Χ��ѧԱ��
        /// </summary>
        /// <param name="queryID">�ʾ�����ID</param>
        /// <param name="orgID">��֯����ID</param>
        /// <returns></returns>
        public int GetStudentNumFromQueryAreaByOrg(int queryID, int orgID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'
                                    AND u.OrganizationID = '{1}'";
            string sql = string.Format(sqlModal, queryID, orgID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }


        /// <summary>
        /// ��ѯĳ���ʾ����鲻����ĳ��֯�����µ��Ѿ����õ��鷶Χ��ѧԱ��
        /// </summary>
        /// <param name="queryID">�ʾ�����ID</param>
        /// <param name="orgID">��֯����ID</param>
        /// <returns></returns>
        public int GetNoStudentNumFromQueryAreaByOrg(int queryID, int orgID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'
                                    AND u.OrganizationID != '{1}'";
            string sql = string.Format(sqlModal, queryID, orgID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }




    }
}