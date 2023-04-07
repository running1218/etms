
using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// �ʾ������Ŀ�����ݷ���
    /// </summary>
    public partial class QS_QueryTitleDataAccess
    {


        /// <summary>
        /// ��ȡĳ�������ʾ�ĵ�ǰ�����Ŀ���
        /// </summary>
        /// <param name="queryID">�����ʾ�ID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleNo(Guid queryID)
        {
            string commandName = string.Format("SELECT ISNULL(MAX(TitleNo),0) FROM QS_QueryTitle WHERE QueryID='{0}'", queryID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// ��ȡ�ʾ��������б�����ϸ��Ϣ
        /// </summary>
        /// <param name="pageIndex">��ʼҳ</param>
        /// <param name="pageSize">ÿҳ�ļ�¼��</param>
        /// <param name="sortExpression">����ʽ</param>
        /// <param name="criteria">�� AND ��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�����ܵ����������ļ�¼��</param>
        public DataTable GetQueryTitleAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_QS_QueryTitle_GetALLInfoList]";
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
        /// �޸��������
        /// </summary>
        /// <param name="titleEntity"></param>
        public void UpdateQSTitleSort(QS_QueryTitle titleEntity)
        {
            string commandName = "dbo.[Pr_QS_QueryTitleSort_Update]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TitleNo", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = titleEntity.TitleID;
            parms[1].Value = titleEntity.TitleNo;

            #endregion
            int i = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

        }


    }
}
