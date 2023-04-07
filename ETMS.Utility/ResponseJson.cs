using ETMS.AppContext;
using ETMS.Utility.Logging;
using System;
using System.Net.Http;
using System.Text;

namespace ETMS.Utility
{
    public static class ResponseJson
    {

        private static Encoding encode = Encoding.GetEncoding("UTF-8");

        /// <summary>
        /// 返回调用成功JSON信息，不返回数据
        /// </summary>
        /// <returns></returns>
        public static HttpResponseMessage GetSuccessJson()
        {
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeSuccessJson(), encode, "application/json") };
        }

        /// <summary>
        /// 返回调用成功JSON信息，包含数据
        /// <para>如果data=null，返回：{"Status":true,"Code":1,"Message":"","Data":null}</para>
        /// <para>如果data="12"，返回：{"Status":true,"Code":1,"Message":"","Data":"12"}</para>
        /// <para>如果data为对象，返回相应的Json数据：{"Status":true,"Code":1,"Message":"","Data":..."}</para>
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns></returns>
        public static HttpResponseMessage GetSuccessJson(Object data)
        {
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeSuccessJson(data), encode, "application/json") };
        }

        /// <summary>
        /// 返回调用成功JSON信息，包含数据列表，以及数据总数
        /// <para>返回示例：{"Status":true,"Code":1,"Message":"","Data":{"TotalRecords":10,"DataList":..."}</para>
        /// </summary>
        /// <param name="dataList">数据列表对象</param>
        /// <param name="totalRecords">数据总数</param>
        public static HttpResponseMessage GetSuccessJson(Object dataList, int totalRecords)
        {
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeSuccessJson(dataList, totalRecords), encode, "application/json") };
        }

        /// <summary>
        /// 返回参数错误JSON信息
        /// </summary>
        /// <returns>示例：{"Status":false,"Code":0,"Message":"参数错误！"}</returns>
        public static HttpResponseMessage GetFailedJson()
        {
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetParametersInValidJson(), encode, "application/json") };
        }

        /// <summary>
        /// 返回调用失败JSON信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="message">信息</param>
        /// <returns>示例：{"Status":false,"Code":-2,"Message":"用户名：User01已经存在！"}</returns>
        public static HttpResponseMessage GetFailedJson(Int32 code, String message)
        {
            ErrorLogHelper.WriteLog(message);
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeFailedJson(code, "系统错误，请与管理员联系！"), encode, "application/json") };
        }

        public static HttpResponseMessage GetBusinessFailedJson(Int32 code, String message)
        {
            return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeFailedJson(code, message), encode, "application/json") };
        }

        public static HttpResponseMessage GetFailedJson(Exception ex)
        {
            if (ex is BusinessException)
            {
                return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeFailedJson(0, ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex)), encode, "application/json") };
            }
            else
            {
                ErrorLogHelper.WriteFileLog(ex);
                return new HttpResponseMessage { Content = new StringContent(JsonHelper.GetInvokeFailedJson(0, "系统错误，请与管理员联系！"), encode, "application/json") };
            }
        }
    }
}
