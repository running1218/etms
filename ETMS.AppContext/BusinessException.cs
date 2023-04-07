using System;

namespace ETMS.AppContext
{
    /// <summary>
    /// 业务异常定义
    /// 内含：业务异常编码，供前端应用转义！
    /// </summary>
    [Serializable]
    public class BusinessException : System.Exception
    {
        private static string BusinessExceptionPreFix = "BusinessException.";
        private static string InnerExceptionSplitChars = "~##~";
        public object[] FormatParams { get; set; }

        public BusinessException(string errorCode, object[] formatParams)
            : base(BusinessExceptionPreFix + errorCode)
        {
            this.FormatParams = formatParams;
        }

        public BusinessException(string errorCode, object[] formatParams, Exception innerException)
            : base(BusinessExceptionPreFix + errorCode, innerException)
        {
            this.FormatParams = formatParams;
        }

        public BusinessException(string errorCode)
            : base(BusinessExceptionPreFix + errorCode)
        {

        }
        public BusinessException(string errorCode, Exception innerException)
            : base(string.Format("{0}{1}{2}", BusinessExceptionPreFix, errorCode, (InnerExceptionSplitChars + (string.IsNullOrEmpty(innerException.StackTrace) ? innerException.Message : innerException.StackTrace))))
        {

        }
        /// <summary>
        /// 判断当前异常是否是业务异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>true：业务异常 false:其它异常</returns>
        public static bool IsBusinessException(System.Exception ex)
        {
            return ex.Message.StartsWith(BusinessExceptionPreFix);
        }

        /// <summary>
        /// 从当前异常中获取业务异常错误码
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns> 业务异常：返回异常错误码，其它异常：返回消息描述</returns>
        public static string GetBusinessErrorCode(System.Exception ex)
        {
            if (IsBusinessException(ex))
            {
                int endIndex = ex.Message.IndexOf(InnerExceptionSplitChars);
                if (endIndex != -1)
                {
                    return ex.Message.Substring(BusinessExceptionPreFix.Length, endIndex - BusinessExceptionPreFix.Length);
                }
                else
                {
                    return ex.Message.Substring(BusinessExceptionPreFix.Length);
                }
            }
            else
            {
                return ex.Message;
            }
        }
    }
}
