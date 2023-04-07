
using Common.Logging;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 业务日志记录器
    /// </summary>
    public abstract class BizLogHelper
    {

        public static void Operate(object targetID, string actionInfo)
        {
            System.Reflection.MethodBase methodInfo = new System.Diagnostics.StackFrame(1).GetMethod();
            ILog logger = LogManager.GetLogger(methodInfo.DeclaringType);
            logger.Fatal(new BusinessLog()
            {
                ModuleName = methodInfo.DeclaringType.FullName,
                MethodName = methodInfo.Name,
                TargetID = targetID,
                Action = actionInfo,
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl(),
                OrganizationID = UserHelper.GetUserInOrganizationID(),
            });
        }

        public static void AddOperate(ETMS.AppContext.IObject newEntity)
        {
            System.Reflection.MethodBase methodInfo = new System.Diagnostics.StackFrame(1).GetMethod();
            ILog logger = LogManager.GetLogger(methodInfo.DeclaringType);
            logger.Fatal(new BusinessLog()
            {
                ModuleName = methodInfo.DeclaringType.FullName,
                MethodName = methodInfo.Name,
                TargetID = newEntity.PK.Value,
                Action = "添加操作：\r\n" + newEntity.ObjectJSONSerialization(),//序列化试题
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl(),
                OrganizationID = UserHelper.GetUserInOrganizationID(),
            });
        }

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="moduleType">模块</param>
        /// <param name="originalEntity">原始数据</param>
        /// <param name="newEntity">新数据</param>
        public static void UpdateOperate(ETMS.AppContext.IObject originalEntity, ETMS.AppContext.IObject newEntity)
        {
            System.Reflection.MethodBase methodInfo = new System.Diagnostics.StackFrame(1).GetMethod();
            ILog logger = LogManager.GetLogger(methodInfo.DeclaringType);
            logger.Fatal(new BusinessLog()
            {
                ModuleName = methodInfo.DeclaringType.FullName,
                MethodName = methodInfo.Name,
                TargetID = newEntity.PK.Value,
                Action = "更新操作：\r\n" + "原始数据:\r\n" + originalEntity.ObjectJSONSerialization() + "\r\n更新数据：\r\n" + newEntity.ObjectJSONSerialization(),//序列化实体
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl(),
                OrganizationID = UserHelper.GetUserInOrganizationID(),
            });
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="moduleType">模块</param>
        /// <param name="originalEntity">原始数据</param>
        public static void DeleteOperate(ETMS.AppContext.IObject originalEntity)
        {
            System.Reflection.MethodBase methodInfo = new System.Diagnostics.StackFrame(1).GetMethod();
            ILog logger = LogManager.GetLogger(methodInfo.DeclaringType);
            logger.Fatal(new BusinessLog()
            {
                ModuleName = methodInfo.DeclaringType.FullName,
                MethodName = methodInfo.Name,
                TargetID = originalEntity.PK.Value,
                Action = "删除操作：\r\n" + originalEntity.ObjectJSONSerialization(),//序列化试题
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl(),
                OrganizationID = UserHelper.GetUserInOrganizationID(),
            });
        }


    }
}
