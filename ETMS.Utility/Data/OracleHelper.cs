using System;
using System.Data;
using System.Data.OracleClient;

namespace ETMS.Utility.Data
{
    /// <summary>
    /// oracle���ݿ������
    /// </summary>
    public class OracleHelper : IDisposable
    {

        /// <summary>
        /// �������ݿ������ַ���
        /// </summary>
        private String strDbConnectionString;

        /// <summary>
        /// ���ݿ����ӱ�־
        /// </summary>
        private bool bConnected;
  
        /// <summary>
        ///���ݿ����Ӷ���
        /// </summary>
        private OracleConnection dbConnection;

        /// <summary>
        /// ���캯������ʼ���ڲ�����
        /// </summary>
        /// <param name="">��</param>
        /// <returns>��</returns>
        public OracleHelper()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //		
        }

        /// <summary>
        /// ���캯������ʼ���ڲ�����
        /// </summary>
        /// <param name="l_sConnectionString">���ݿ����Ӵ�</param>
        /// <returns>��</returns>
        public OracleHelper(String l_sConnectionString)
        {
            if (l_sConnectionString != "")
            {
                strDbConnectionString = l_sConnectionString;
                bConnected = false;
            }
        }

        /// <summary>
        /// ���ڲ�����strDbConnectionString��ֵ
        /// </summary>
        /// <param name="l_sConnectionString">���ݿ����Ӵ�</param>
        /// <returns>��</returns>
        public void setConnString(String l_sConnectionString)
        {
            if (l_sConnectionString != "")
            {
                strDbConnectionString = l_sConnectionString;
                CloseDb();
            }
        }

        /// <summary>
        /// �������ݿ⣬�������ݿ�����
        /// </summary>
        /// <param name="">��</param>
        /// <returns>�ɹ�����true</returns>
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
        /// �ر����ݿ⣬�ͷ����ݿ���Դ
        /// </summary>
        /// <param name="">��</param>
        /// <returns>�ɹ�����true</returns>
        public bool CloseDb()
        {
            Dispose();
            return true;
        }

        /// <summary>
        /// ��ȥ������Դ.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        /// <summary>
        ///	�ͷŶ���ʵ������.
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
        /// ִ���޸����ݿ���������ӡ��޸ġ�ɾ�����洢���̵Ȳ���
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool ExecuteNonQuery(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //���������ݿ�ʧ���򷵻ؿ�
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
        /// ��ѯ���ݿ⣬����DataReader���
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //���������ݿ�ʧ���򷵻ؿ�
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
        /// ��ѯ���ݿ⣬����object���
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //���������ݿ�ʧ���򷵻ؿ�
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
        /// ��ѯ���ݿ⣬����DataSet���
        /// </summary>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //���������ݿ�ʧ���򷵻ؿ�
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
        /// ��ѯ���ݿ⣬����DataTable���
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(CommandType cmdType, string cmdText, OracleParameterCollection commandParameters)
        {
            //���������ݿ�ʧ���򷵻ؿ�
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
