using System.Data;
using ETMS.Utility.Data;


namespace ETMS.Components.Basic.Implement.DAL.Dictionary
{
    class SysDictionaryDataAccess
    {

        /// <summary>
        /// 运行一个查询语句，返回DataTable
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public DataTable ExecuteSelectSQL(string sql)
        {
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql).Tables[0];

            return dt;
        }


    }
}
