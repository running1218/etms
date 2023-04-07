using System;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ETMS.Utility
{
    public static class JsonHelper
    {

        #region Newtonsoft

        /// <summary>  
        /// 将对象序列化为JSON数据  
        /// </summary>  
        /// <param name="obj">对象实体</param>  
        /// <returns>JSON数据</returns>
        public static string SerializeObject(object obj)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };

            return JsonConvert.SerializeObject(obj, Formatting.None, timeConverter);
        }

        public static string SerializeObject(object obj, IsoDateTimeConverter timeConverter)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, timeConverter);
        }

        /// <summary>  
        /// 将JSON数据反序列化为对象 
        /// </summary>  
        /// <param name="json">JSON数据</param>  
        /// <returns>对象实体</returns>  
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion

        #region ContextResponse

        private static string GetInvokeResultJson(Boolean status, Int32 code, String message)
        {
            var result = new
            {
                Status = status,
                Code = code,
                Message = message
            };
            return SerializeObject(result);
        }

        private static string GetInvokeResultJson(Boolean status, Int32 code, String message, Object data)
        {
            var result = new
            {
                Status = status,
                Code = code,
                Message = message,
                Data = data
            };
            return SerializeObject(result);
        }

        private static string GetInvokeResultJson(Boolean status, Int32 code, String message, Object data, IsoDateTimeConverter timeConverter)
        {
            var result = new
            {
                Status = status,
                Code = code,
                Message = message,
                Data = data
            };
            return SerializeObject(result, timeConverter);
        }

        /// <summary>
        /// 返回调用成功JSON信息，不返回数据
        /// </summary>
        /// <returns>示例：{"Status":true,"Code":1,"Message":""}</returns>
        public static string GetInvokeSuccessJson()
        {
            return GetInvokeResultJson(true, 1, "");
        }

        /// <summary>
        /// 返回调用成功JSON信息，包含数据
        /// <para>如果data=null，返回：{"Status":true,"Code":1,"Message":"","Data":null}</para>
        /// <para>如果data="12"，返回：{"Status":true,"Code":1,"Message":"","Data":"12"}</para>
        /// <para>如果data为对象，返回相应的Json数据：{"Status":true,"Code":1,"Message":"","Data":..."}</para>
        /// </summary>
        /// <param name="obj">数据对象</param>
        public static string GetInvokeSuccessJson(Object data)
        {
            return GetInvokeResultJson(true, 1, "", data);
        }

        /// <summary>
        /// 返回调用成功JSON信息，包含数据列表，以及数据总数
        /// <para>返回示例：{"Status":true,"Code":1,"Message":"","Data":{"TotalRecords":10,"DataList":..."}</para>
        /// </summary>
        /// <param name="obj">数据列表对象</param>
        /// <param name="totalRecords">数据总数</param>
        public static string GetInvokeSuccessJson(Object dataList, int totalRecords)
        {
            return GetInvokeResultJson(true, 1, "", new
            {
                TotalRecords = totalRecords,
                DataList = dataList
            });
        }

        public static string GetInvokeSuccessJson(object dataList, int totalRecords, IsoDateTimeConverter timeConverter)
        {
            return GetInvokeResultJson(true, 1, "", new
            {
                TotalRecords = totalRecords,
                DataList = dataList
            }, timeConverter);
        }

        /// <summary>
        /// 返回参数错误JSON信息
        /// </summary>
        /// <returns>示例：{"Status":false,"Code":0,"Message":"参数错误！"}</returns>
        public static string GetParametersInValidJson()
        {
            return GetInvokeResultJson(false, 0, "参数错误！");
        }

        /// <summary>
        /// 返回调用失败JSON信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="message">信息</param>
        /// <returns>示例：{"Status":false,"Code":-2,"Message":"用户名：User01已经存在！"}</returns>
        public static string GetInvokeFailedJson(Int32 code, String message)
        {
            return GetInvokeResultJson(false, code, message);
        }

        #endregion


        //转换为Json格式输出  
        public static string ToJson(this object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream();

            ser.WriteObject(ms, t);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());

            ms.Close();

            //替换Json的Date字符串

            string p = @"\\/Date\((\d+)\+\d+\)\\/";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            return jsonString;

        }


        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {

            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式

            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            T obj = (T)ser.ReadObject(ms);

            ms.Flush();
            ms.Close();

            return obj;

        }


        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ConvertJsonDateToDateString(Match m)
        {

            string result = string.Empty;

            DateTime dt = new DateTime(1970, 1, 1);

            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));

            dt = dt.ToLocalTime();

            result = dt.ToString("yyyy-MM-dd HH:mm:ss");

            return result;

        }



        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ConvertDateStringToJsonDate(Match m)
        {

            string result = string.Empty;

            DateTime dt = DateTime.Parse(m.Groups[0].Value);

            dt = dt.ToUniversalTime();

            TimeSpan ts = dt - DateTime.Parse("1970-01-01");

            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;

        }
    }  
}
