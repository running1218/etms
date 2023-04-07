using System;
using System.Text;

namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 业务操作日志
    /// </summary>
    [Serializable]
    public class BusinessLog : BaseLog
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get;
            set;
        }
        /// <summary>
        /// 模块内方法名称
        /// </summary>
        public string MethodName
        {
            get;
            set;
        }
        /// <summary>
        /// 操作对象ID
        /// </summary>
        public object TargetID
        {
            get;
            set;
        }
        /// <summary>
        /// 动作
        /// </summary>
        public String Action
        {
            get;
            set;
        }

        /// <summary>
        /// 数据所属机构
        /// </summary>
        public int OrganizationID
        {
            get;
            set;
        }
        public override string ToString()
        {
            StringBuilder writer = new StringBuilder();
            writer.AppendLine();
            writer.AppendFormat("模块名称：{0}\r\n", this.ModuleName);
            writer.AppendFormat("方法名称：{0}\r\n", this.MethodName);
            writer.AppendFormat("操作对象ID：{0}\r\n", this.TargetID);
            writer.AppendFormat("数据所属机构：{0}\r\n", this.OrganizationID);
            writer.AppendFormat("动作描述：{0}\r\n", this.Action);
            writer.AppendFormat("当前用户：{0}\r\n", this.LoginName);
            writer.AppendFormat("Web服务器地址：{0}\r\n", this.ServerName);
            writer.AppendFormat("客户端IP：{0}\r\n", this.ClientIP);
            writer.AppendFormat("用户请求URL：{0}\r\n", this.PageUrl);
            return writer.ToString();
        }
    }
}