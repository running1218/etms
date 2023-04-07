using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    /// <summary>
    /// ��ί�����ݷ���
    /// </summary>
    public partial class Sty_ClassMonitorDataAccess
	{
        /// <summary>
        /// ɾ��
        /// </summary>
        public void RemoveStudentPositions(Guid classStudentID)
        {
            string commandName = "dbo.[Pr_Sty_ClassMonitor_DeleteByClassStudentID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ClassStudentID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = classStudentID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }		
	}
}
