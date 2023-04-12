//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-23 11:37:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// �ʾ�������Ŀѡ����ѡ������ݷ���
    /// </summary>
    public partial class QS_QueryTitleOptionDataAccess
    {
        /// <summary>
        /// ��ȡĳ�������ʾ���Ŀ�µ�ѡ��ĵ�ǰ������
        /// </summary>
        /// <param name="titleID">�����ʾ���ĿID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleOptionNo(Guid titleID)
        {
            string commandName = string.Format("SELECT ISNULL(MAX(OptionNo),0) FROM QS_QueryTitleOption WHERE TitleID='{0}'", titleID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




        /// <summary>
        /// ��ȡ�ʾ���������б�����ϸ��Ϣ
        /// </summary>
        /// <param name="pageIndex">��ʼҳ</param>
        /// <param name="pageSize">ÿҳ�ļ�¼��</param>
        /// <param name="sortExpression">����ʽ</param>
        /// <param name="criteria">�� AND ��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�����ܵ����������ļ�¼��</param>
        public DataTable GetQueryTitleOptionAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_QS_QueryTitleOption_GetALLInfoList]";
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

        public void RemoveByTitleID(Guid titleID) 
        {
            string commandName = "dbo.Pr_QS_QueryTitleOption_DeleteByTitleID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = titleID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }


    }
}