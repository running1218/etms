
using log4net.Appender;
using System.Data;
using log4net.Layout;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// ҵ����־��¼
    /// </summary>
    public class BusinessLogAdoNetAppender : CustomAdoNetAppender
    {
        public BusinessLogAdoNetAppender()
        {
            //���ݿ��������ͣ�SQLServer�ͻ���
            this.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            //��¼��־ʱ��������
            base.UseTransactions = false;
            //�������ͣ��ű�
            this.CommandType = CommandType.Text;
            this.CommandText = @"INSERT INTO Log_BusinessOperate([ModuleName],[MethodName],[TargetID],[Action],[LoginName],[CreateTime],[ServerName],[ClientIP],[PageUrl],[OrganizationID])
VALUES(@ModuleName,@MethodName,@TargetID,@Action,@LoginName,@CreateTime,@ServerName,@ClientIP,@PageUrl,@OrganizationID)";
            //����ת����
            string parameterName = "ModuleName";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 100, Layout = new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "MethodName";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "TargetID";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.String, Size = 50, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });
            parameterName = "Action";
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
            parameterName = "OrganizationID";
            this.AddParameter(new AdoNetAppenderParameter() { ParameterName = "@" + parameterName, DbType = DbType.Int32, Size = 4, Layout = (IRawLayout)new Layout2RawLayoutAdapter(new LogLayout("%" + parameterName)) });

            //����ǿ�Ƽ���������ݿ�����
            base.ActivateOptions();
        } 
    }
}
