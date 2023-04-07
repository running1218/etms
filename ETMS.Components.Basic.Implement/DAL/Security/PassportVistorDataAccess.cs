using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Basic.Implement.DAL
{
    public class PassportVistorDataAccess
    {
        public void Save(int id, int num)
        {
            string command = "Update Passport_VistorNum Set Num = @Num Where ID = @ID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, "Passport_VistorNum_Update");
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@Num", SqlDbType.Int),
                    new SqlParameter("@ID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, "Passport_VistorNum_Update", parms);
            }

            parms[0].Value = num;
            parms[1].Value = id;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, command, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public int GetTotalNum(Int32 id)
        {
            string command = "SELECT Num FROM dbo.Passport_VistorNum WHERE ID = 1";

            var result = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, command, null);
            if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
            {
                return int.Parse(result.Tables[0].Rows[0]["Num"].ToString());
            }

            return 0;
        }

    }
}
