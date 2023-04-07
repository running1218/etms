
using log4net.Appender;
using System.Data;
using log4net.Layout;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 业务日志记录
    /// </summary>
    public class ErrorLogAdoNetAppender : CustomAdoNetAppender
    {
        public ErrorLogAdoNetAppender()
        {
            //数据库连接类型：SQLServer客户端
            this.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            //记录日志时禁用事务
            base.UseTransactions = false;
            //命令类型：脚本
            this.CommandType = CommandType.Text;
            this.CommandText = @"INSERT INTO [Log_SystemException]([ApplicationName],[Message],[BaseMessage],[StackTrace],[LoginName],[CreateTime],[ServerName],[ClientIP],[PageUrl])
     VALUES(@ApplicationName,@Message,@BaseMessage,@StackTrace,@LoginName,@CreateTime,@ServerName,@ClientIP,@PageUrl)";
            //设置转换器
            string parameterName = "ApplicationName";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "Message";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 500, Layout = new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "BaseMessage";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 500, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "StackTrace";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = int.MaxValue, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "LoginName";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 50, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "CreateTime";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.DateTime, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "ServerName";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 50, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "ClientIP";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 20, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "PageUrl";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 1024, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });

            //必须强制激活，开启数据库连接
            base.ActivateOptions();
        }
    }
}
