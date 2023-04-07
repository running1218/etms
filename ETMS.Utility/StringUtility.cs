using System;
using System.Collections.Generic;
using System.Text;
using ETMS.AppContext;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ETMS.Utility
{
    public static class StringUtility
    {
        /// <summary>
        /// 生成安全的SQL查询条件，防止SQL注入
        /// </summary>
        /// <param name="sqlValue"></param>
        /// <returns></returns>
        public static string ToSafeSQLValue(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            //先将单引号'的内码chr(27)替换成单引号'
            source = source.Replace("chr(27)", "'");
            source = source.Replace("Chr(27)", "'");
            //防止SQL注入
            source = source.Replace("'", "''");
            return source;
        }

        /// <summary>
        /// String => Guid
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string source)
        {
            Guid value = default(Guid);
            Guid.TryParse(source, out value);
            return value;
        }
        /// <summary>
        /// 将普通格式文本转为HTML格式文本
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TextToHtml(this string source)
        {
            return source.Replace(" ", "&nbsp;").Replace("\r\n", "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
        }

        /// <summary>
        /// String => Int
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(int);

            int value = 0;
            if (!int.TryParse(source.Trim(), out value))
            {                
                //throw new BusinessException("输入数据不合法或者超出输入范围！");
            }
            return value;
        }

        public static int ToInt(this decimal? source)
        {
            if (null != source)
            {
                return Decimal.ToInt32((decimal)source);
            }
            else {
                return 0;
            }
        }
        /// <summary>
        /// Object => Int
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt(this object source)
        {
            int value = 0;
            if (!int.TryParse(source.ToString().Trim(), out value))
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }            
            return value;
        }

        /// <summary>
        /// String => Int16
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Int16 ToInt16(this string source)
        {
            Int16 value = 0;
            if (!Int16.TryParse(source.Trim(), out value))
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }
            return value;
        }

        /// <summary>
        /// Object => Int16
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Int16 ToInt16(this object source)
        {
            Int16 value = 0;
            if (!Int16.TryParse(source.ToString().Trim(), out value))
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }
            return value;
        }

        /// <summary>
        /// Enum => Int
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToEnumValue(this object source)
        {
            return (int)source;
        }

        /// <summary>
        ///String => Decimal
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(decimal);

            decimal value = 0;
            if(!decimal.TryParse(source.Trim(), out value))
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }
            return value;
        }

        public static double ToDouble(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(double);

            double value = 0;
            if (!double.TryParse(source.Trim(), out value))
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }
            return value;
        }

        /// <summary>
        /// Decimal => Int
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInterger(this decimal source)
        {
            return Decimal.Round(source, 0).ToString().ToInt();
        }

        /// <summary>
        /// String => Boolean
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string source)
        {
            bool flag = false;
            if (source == "1" || source.ToLower() == "true")
                flag = true;

            return flag;
        }

        /// <summary>
        /// object => Boolean
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object source)
        {
            if (null == source)
            {
                throw new BusinessException("输入数据不合法或者超出输入范围！");
            }

            return ToBoolean(source.ToString());
        }

        /// <summary>
        /// DateTime => String
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime source)
        {
            return source == DateTime.MinValue ? string.Empty: source.ToString("yyyy-MM-dd");
        }

        public static decimal ToHours(this object source)
        {
            decimal value = source.ToString().ToDecimal();
            return value;
        }

        /// <summary>
        /// Object => String
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDate(this object source)
        {
            return source.ToDateTime() == DateTime.MinValue ? string.Empty : source.ToDateTime().ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// DateTime => String
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format">格式自定义</param>
        /// <returns></returns>
        public static string ToDate(this DateTime source, string format)
        {
            return source == DateTime.MinValue ? string.Empty : source.ToString(format); 
        }

      

        /// <summary>
        /// Object => Guid
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object source)
        {
            Guid value = default(Guid);
            if (null != source)
            {
                Guid.TryParse(source.ToString(), out value);
            }
            return value;
        }

        /// <summary>
        /// String => DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source)
        {
            DateTime value = default(DateTime);
            DateTime.TryParse(source, out value);
            return value;
        }

        /// <summary>
        /// object => DateTime
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object source)
        {
            DateTime value = default(DateTime);
            DateTime.TryParse(source.ToString(), out value);
            return value;
        }

        public static DateTime ToStartDateTime(this string source)
        {
            DateTime value = default(DateTime);
            DateTime.TryParse(source.ToString(), out value);

            string date = "1900-01-01";
            DateTime.TryParse(string.Format("{0} 00:00:00", date), out value);
            return value;
        }

        public static DateTime ToEndDateTime(this string source)
        {
            DateTime value = default(DateTime);
            DateTime.TryParse(source.ToString(), out value);

            string date = value.ToDate();
            if (string.IsNullOrEmpty(date))
            {
                date = "2090-12-31";
            }
            DateTime.TryParse(string.Format("{0} 23:59:59", date), out value);
            return value;
        }
        /// <summary>
        /// 字符串保留位数截取
        /// </summary>
        /// <param name="source"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        public static string ShortText(this string source, int remain)
        {
            if (null != source)
                return source.Length > remain ? string.Format("{0}...", source.Substring(0, remain)) : source;
            else
                return string.Empty;

        }
        /// <summary>
        /// 字符串保留位数截取
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="remain">截取长度</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static string ShortText(this string source, int remain,int startIndex)
        {
            if (null != source)
                return source.Length > remain + 1 ? string.Format("{0}", source.Substring(startIndex, remain)) + "..." : source.Substring(startIndex, source.Length - startIndex);
            else
                return string.Empty;

        }

        /// <summary>
        /// 字符串保留位数截取
        /// </summary>
        /// <param name="source"></param>
        /// <param name="remain"></param>
        /// <returns></returns>
        public static string ShortText(this object source, int remain)
        {
            string text = source.ToString();
            return text.Length >= remain ? string.Format("{0}...", text.Substring(0, remain)) : text;
        }

        /// <summary>
        /// 以【AccessibleHeaderText】定义隐藏的列参数
        /// </summary>
        /// <param name="control"></param>
        /// <param name="columnFlag"></param>
        public static void HideColumn(this GridView control, string columnFlag)
        {
            foreach (DataControlField filed in control.Columns)
            {
                if (filed.AccessibleHeaderText.Equals(columnFlag, StringComparison.OrdinalIgnoreCase))
                {
                    filed.Visible = false;
                    break;
                }
            }
        }

        /// <summary>
        /// 控件Disable设置
        /// </summary>
        /// <param name="obj"></param>
        public static void Disable(this WebControl obj)
        {
            obj.Attributes["OnClick"] = "return false;";
            obj.Style.Add("color", "gray");
        } 


        /// <summary>
        /// 字符串首字母小写
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string LowerFirst(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return source.Substring(0, 1).ToLower() + source.Substring(1);
        }


        /// <summary>
        /// 将播放时长转换成时间格式
        /// </summary>
        /// <param name="PlayingFlag"></param>
        /// <returns></returns>
        public static string formatSeconds(this int PlayingFlag)
        {

            int theTime = (PlayingFlag % 3600) % 60;// 秒

            int theTime1 = (PlayingFlag % 3600) / 60;// 分

            int theTime2 = PlayingFlag / 3600;// 小时

            string result = "";

            result += theTime2 + ":";


            if (theTime1 > 9)
            {
                result += theTime1 + ":";
            }
            else
            {
                result += "0" + theTime1 + ":";
            }


            if (theTime > 9)
            {
                result += theTime;
            }
            else
            {
                result += "0" + theTime;
            }


            return result;

        }

        /// <summary>
        /// 获取Request参数值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public static string ToParamValue(this NameValueCollection obj, string ParamName)
        {
            if (null != obj)
            {
                return obj[ParamName].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Reques参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static T ToparamValue<T>(this System.Web.HttpRequest request, string paramName)
        {
            if (null != request.QueryString[paramName])
            {
                return ETMS.Utility.Converter.ChangeType<T>(request.QueryString[paramName]);
            }

            return default(T);
        }

        /// <summary>
        /// List<T> 分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<T> PageList<T>(this List<T> entities, int pageIndex, int pageSize)
        { 
            if (null == entities)
            {
                return default(List<T>);
            }

            int index = (pageIndex -1) * pageSize;
            int count = entities.Count - index > pageSize ? pageSize : entities.Count - index;
            return entities.GetRange(index, count);
        }

        /// <summary>
        /// List<T> 分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<T> PageList<T>(this List<T> entities, int pageIndex, int pageSize, out int totalRecords)
        {
            if (null != entities)
            {
                totalRecords = entities.Count;
                int index = (pageIndex - 1) * pageSize;
                int count = entities.Count - index > pageSize ? pageSize : entities.Count - index;
                return entities.GetRange(index, count);
            }
            else
            {
                totalRecords = 0;
                return default(List<T>);
            }
        }

        /// <summary>
        /// 字符串首字母大写
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string UpperFirst(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }

        /// <summary>
        /// 去掉字符串中所有的半角、全角空格
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TrimAllSpace(this string source)
        {
            return Regex.Replace(source, "([ ]+)", string.Empty);
        }

        /// <summary>
        /// 转义单、双引号【&apos;&quot;】
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReplaceQuotAndApos(this string source)
        {
            return source.Replace("\"", "&apos;").Replace("'", "&quot;");
        }

        /// <summary>
        /// js escape
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Escape(this string source)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(source);
            for (int i = 0; i < ba.Length; i += 2)
            {
                if (ba[i + 1] == 0)
                {
                    //数字,大小写字母,以及"+-*/._"不变
                    if (
                          (ba[i] >= 48 && ba[i] <= 57)
                        || (ba[i] >= 64 && ba[i] <= 90)
                        || (ba[i] >= 97 && ba[i] <= 122)
                        || (ba[i] == 42 || ba[i] == 43 || ba[i] == 45 || ba[i] == 46 || ba[i] == 47 || ba[i] == 95)
                        )//保持不变
                    {
                        sb.Append(Encoding.Unicode.GetString(ba, i, 2));

                    }
                    else//%xx形式
                    {
                        sb.Append("%");
                        sb.Append(ba[i].ToString("X2"));
                    }
                }
                else
                {
                    sb.Append("%u");
                    sb.Append(ba[i + 1].ToString("X2"));
                    sb.Append(ba[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }

        #region 截取指定字节长度（汉字）
        public static String MutiSubString(this String aOrgStr, int aLength)
        {
            int intLen = aOrgStr.Length;
            int start = 0;
            int end = intLen;
            int single = 0;
            char[] chars = aOrgStr.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (System.Convert.ToInt32(chars[i]) > 255)
                {
                    start += 2;
                }
                else
                {
                    start += 1;
                    single++;
                }
                if (start >= aLength)
                {

                    if (end % 2 == 0)
                    {
                        if (single % 2 == 0)
                        {
                            end = i + 1;
                        }
                        else
                        {
                            end = i;
                        }
                    }
                    else
                    {
                        end = i + 1;
                    }
                    break;
                }
            }
            string temp = aOrgStr.Substring(0, end);
            return temp;
            /*
             运行结果 ：
                str = MutiSubString("abc汉字字符",  5 )
                str = "abc汉"
            */
        }
        #endregion        

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="strHtml">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string StripHTML(this string strHtml)
        {
            string[] aryReg ={
                                  @"<script[^>]*?>.*?</script>",
 
                                  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                  @"([\r\n])[\s]+",
                                  @"&(quot|#34);",
                                  @"&(amp|#38);",
                                  @"&(lt|#60);",
                                  @"&(gt|#62);", 
                                  @"&(nbsp|#160);", 
                                  @"&(iexcl|#161);",
                                  @"&(cent|#162);",
                                  @"&(pound|#163);",
                                  @"&(copy|#169);",
                                  @"&#(\d+);",
                                  @"-->",
                                  @"<!--.*\n"         
                             };

            string[] aryRep = {
               "",
               "",
               "",
               "\"",
               "&",
               "<",
               ">",
               " ",
               "\xa1",//chr(161),
               "\xa2",//chr(162),
               "\xa3",//chr(163),
               "\xa9",//chr(169),
               "",
               "\r\n",
               ""
          };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            //strOutput.Replace("\r\n", "");

            return strOutput;
        }
    }

    /// <summary>
    /// 类型转换，支持Guid
    /// </summary>
    public static class Converter
    {
        public static T ChangeType<T>(object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
            return (T)tc.ConvertFrom(value);
        }
        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }

    public class VersionConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string strvalue = value as string;

            if (strvalue != null)
            {
                return new Version(strvalue);
            }
            else
            {
                return new Version();
            }
        }
    }
}
