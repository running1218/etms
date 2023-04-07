using System;
using System.Data;
using System.Data.OracleClient;

namespace ETMS.Utility.Data
{
    /// <summary>
    /// oracle数据库操作类
    /// </summary>
    public class OracleHelper : IDisposable
    {

        /// <summary>
        /// 保存数据库连接字符串
        /// </summary>
        private String strDbConnectionString;

        /// <summary>
        /// 数据库连接标志
        /// </summary>
        private bool bConnected;
  
        /// <summary>
        ///数据库连接对象
        /// </summary>
        private OracleConnection dbConnection;

        /// <summary>
        /// 构造函数，初始化内部变量
        /// </summary>
        /// <param name="">无</param>
        /// <returns>无</returns>
        public OracleHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //		
        }

        /// <summary>
        /// 构造函数，初始化内部变量
        /// </summary>
        /// <param name="l_sConnectionString">数据库连接串</param>
        /// <returns>无</returns>
        public OracleHelper(String l_sConnectionString)
        {
            if (l_sConnectionString != "")
            {
                strDbConnectionString = l_sConnectionString;
                bConnected = false;
            }
        }

        /// <summary>
        /// 对内部变量strDbConnectionString赋值
        /// </summary>
        /// <param name="l_sConnectionString">数据库连接串</param>
        /// <returns>无</returns>
        public void setConnString(String l_sConnectionString)
        {
            if (l_sConnectionString != "")
            {
                strDbConnectionString = l_sConnectionString;
                CloseDb();
            }
        }

        /// <summary>
        /// 连接数据库，并打开数据库连接
        /// </summary>
        /// <param name="">无</param>
        /// <returns>成功返回true</returns>
        private bool ConnectDb()
        {
            if (!bConnected)
            {
                if (dbConnection == null)
                {
                    dbConnection = new OracleConnection(strDbConnectionString);
                    dbConnection.Open();
                }
                bConnected = true;
            }
            return true;
        }

        /// <summary>
        /// 关闭数据库，释放数据库资源
        /// </summary>
        /// <param name="">无</param>
        /// <returns>成功返回true</returns>
        public bool CloseDb()
        {
            Dispose();
            return true;
        }

        /// <summary>
        /// 除去对象资源.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        /// <summary>
        ///	释放对象实例变量.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object

            if (bConnected)
            {
                if (dbConnection.State != ConnectionState.Closed)
                {
                    dbConnection.Dispose();
                    dbConnection.Close();
                    bConnected = false;
                }
            }
        }

        /// <summary>
        /// 执行修改数据库操作：增加、修改、删除、存储过程等操作
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>是否成功</returns>
        public bool ExecuteNonQuery(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //若连接数据库失败则返回空
            if (!ConnectDb())
            {
                return false;
            }

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, dbConnection, null, cmdType, cmdText, commandParameters);
            cmd.ExecuteNonQuery();
            foreach (OracleParameter parm in commandParameters)
            {
                if (parm.Direction == ParameterDirection.Output)
                {
                    parm.Value = cmd.Parameters[parm.ParameterName].Value;
                }
            }
            cmd.Parameters.Clear();
            return true;
        }

        /// <summary>
        /// 查询数据库，返回DataReader结果
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //若连接数据库失败则返回空
            if (!ConnectDb())
            {
                return null;
            }

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, dbConnection, null, cmdType, cmdText, commandParameters);

            OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }

        /// <summary>
        /// 查询数据库，返回object结果
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //若连接数据库失败则返回空
            if (!ConnectDb())
            {
                return null;
            }

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, dbConnection, null, cmdType, cmdText, commandParameters);

            object o = cmd.ExecuteOracleScalar();
            cmd.Parameters.Clear();
            return o;
        }

        /// <summary>
        /// 查询数据库，返回DataSet结果
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //若连接数据库失败则返回空
            if (!ConnectDb())
            {
                return null;
            }

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, dbConnection, null, cmdType, cmdText, commandParameters);

            DataSet ds = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 查询数据库，返回DataTable结果
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //若连接数据库失败则返回空
            if (!ConnectDb())
            {
                return null;
            }

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, dbConnection, null, cmdType, cmdText, commandParameters);

            DataSet ds = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            cmd.Parameters.Clear();
            return dt;
        }

        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (trans != null)
                cmd.Transaction = trans;

            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                {
                    cmd.Parameters.Add(parm.ParameterName, parm.OracleType, parm.Size);
                    cmd.Parameters[parm.ParameterName].Direction = parm.Direction;
                    cmd.Parameters[parm.ParameterName].Value = parm.Value;
                }
            }
        }

    }
}
