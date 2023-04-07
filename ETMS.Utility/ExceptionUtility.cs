using System;

namespace ETMS.Utility
{
    using ETMS.AppContext;
    using System.Configuration;
    /// <summary>
    /// 各种异常翻译工具
    /// appSettings中：System.ApplicationException.ShowMore控制异常是否显示全部！
    /// </summary>
    public class ExceptionUtility
    {
        /// <summary>
        /// 翻译异常，内部规则分：
        /// 业务异常==>通过资源文件，翻译为业务描述
        /// 系统异常==>则通过系统配置确定是否向用户呈现内部错误详情
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetTranslateExceptionMessage(Exception ex)
        {
            string translateMessage = "";
            if (BusinessException.IsBusinessException(ex))//业务异常
            {
                //业务异常处理，转义
                translateMessage = doBusinessException(ex);
            }
            else//未知异常处理
            {
                //获取系统对内部错误的转义（规则：发布环境=系统内部出错；开发环境=显示详细错误信息）
                translateMessage = doUnKnowException(ex);
            }
            return translateMessage;
        }
        #region helper
        /// <summary>
        ///  业务异常处理，完成：
        ///  1、错误码转义，
        ///  2、记录异常日志！
        /// </summary>
        /// <param name="ex">业务异常</param>
        /// <returns>错误码转义</returns>
        private static string doBusinessException(Exception ex)
        {
            BusinessException bizException = ex as BusinessException;
            string errorCode = BusinessException.GetBusinessErrorCode(ex);
            string message = MessageTranslatorUtility.GetMessage(errorCode, bizException.FormatParams);           
            return message;
        }

        /// <summary>
        /// 其他未知异常处理，完成：
        /// 1、记录异常日志
        /// 2、统一消息输出，避免普通用户看到内部错误！
        /// 
        /// （规则：发布环境=系统内部出错；开发环境=显示详细错误信息）
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string doUnKnowException(Exception ex)
        {
            string sysInnerExceptionMessage = MessageTranslatorUtility.GetMessage("Application.SystemInnerException");
          
            //如果含有内部异常，则显示第一级内部异常
            if ("true".Equals(ConfigurationManager.AppSettings["System.ApplicationException.ShowMore"], StringComparison.InvariantCultureIgnoreCase))
            {
                string moreInfo = (ex.InnerException == null ? ex.Message : (ex.Message + "(" + ex.InnerException.Message + ")"));
                return sysInnerExceptionMessage + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["System.ApplicationException.ShowMore"]) ? "" : moreInfo);
            }
            else
            {
                return sysInnerExceptionMessage;
            }
        }
        #endregion
    }
}
