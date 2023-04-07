using System;
using System.Data.SqlClient;
using System.Data;
using ETMS.Utility.Data;

namespace ETMS.Components.Scrom.Implement.DAL
{
    public class SuspendDataDataAccess
    {
        /// <summary>
        /// 设置用户暂停数据
        /// </summary>
        /// <param name="ResourceID">资源编号</param>
        /// <param name="UserID">用户编号</param>
        /// <param name="val">值</param>
        public void SetSuspendData(Guid ResourceID, int UserID, string val)
        {
            string commandName = "dbo.[Pr_Sco_Cmi_Core_Set_SuspendDataByRUS]";
            SqlParameter[] parms ={ 
              new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@SuspendData",SqlDbType.NVarChar)
                                  };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = val;
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据资源编号、用户编号 获得暂停数据
        /// </summary>
        /// <param name="ResourceID">资源编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns></returns>
        public DataTable GetSuspendData(Guid ResourceID, int UserID)
        {
            string commandName = "dbo.[Pr_Sco_Cmi_Core_Get_SuspendDataByRUS]";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;

            return SqlHelper.ExecuteDataset(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
