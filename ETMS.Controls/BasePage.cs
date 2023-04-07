using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Data;


namespace ETMS.Controls
{
    #region 页面调用条件检测机制

    /// <summary>
    /// 参数规则校验处理句柄
    /// </summary>
    /// <param name="value">参数值</param>
    /// <returns></returns>
    public delegate void RuleVerifyHandler(object value);
    public delegate bool RangeVerifyHandler(object value);
    /// <summary>
    /// 页面调用请求参数定义
    /// </summary>
    public class RequestParameter
    {
        public RequestParameter(string parmName, object parmValue)
            : this(parmName, parmValue, null)
        {

        }
        public RequestParameter(string parmName, object parmValue, RuleVerifyHandler verifyHandler)
        {
            if (string.IsNullOrEmpty(parmName))
                throw new ArgumentNullException("parmName");

            this.m_ParameterName = parmName;
            this.m_ParameterValue = parmValue;

            this.m_RuleVerifyHandler = verifyHandler;
        }
        private string m_ParameterName;
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName
        {
            get
            {
                return m_ParameterName;
            }
            set
            {
                m_ParameterName = value;
            }
        }

        private object m_ParameterValue;
        /// <summary>
        /// 参数值
        /// </summary>
        public object ParameterValue
        {
            get
            {
                return m_ParameterValue;
            }
            set
            {
                m_ParameterValue = value;
            }
        }

        private RuleVerifyHandler m_RuleVerifyHandler;
        /// <summary>
        /// 参数校验规则
        /// </summary>
        public RuleVerifyHandler VerifyHandler
        {
            get
            {
                return m_RuleVerifyHandler;
            }
            set
            {
                m_RuleVerifyHandler = value;
            }
        }
        /// <summary>
        /// 校验
        /// </summary>
        public void Verify()
        {
            if (this.m_RuleVerifyHandler != null)
            {
                this.m_RuleVerifyHandler(this.m_ParameterValue);
            }
        }

        /// <summary>
        /// 创建默认的请求参数
        /// </summary>
        /// <param name="parmName">参数名称，默认从Request[]中检索</param>
        /// <returns></returns>
        public static RequestParameter CreateDefaultRequestParameter(string parmName)
        {
            return CreateDefaultRequestParameter(parmName, HttpContext.Current.Request[parmName]);
        }
        /// <summary>
        /// 创建默认的请求参数
        /// </summary>
        /// <param name="parmName">参数名称</param>
        /// <param name="parmValue">参数值</param>
        /// <returns></returns>
        public static RequestParameter CreateDefaultRequestParameter(string parmName, object parmValue)
        {
            RequestParameter entity = new RequestParameter(parmName, parmValue);
            entity.VerifyHandler = RequestVerify(entity);
            return entity;
        }
        /// <summary>
        /// 创建一组默认的请求参数
        /// </summary>
        /// <param name="parmNames">一组参数名称</param>
        /// <returns></returns>
        public static RequestParameter[] CreateDefaultRequestParameter(string[] parmNames)
        {
            RequestParameter[] parms = new RequestParameter[parmNames.Length];
            for (int i = 0; i < parmNames.Length; i++)
            {
                parms[i] = CreateDefaultRequestParameter(parmNames[i]);
            }
            return parms;
        }
        /// <summary>
        /// 创建默认的范围验证参数
        /// </summary>
        /// <param name="parmName">参数名，默认从Request[]中检索</param>
        /// <param name="rangeVerifyImpl"></param>
        /// <returns></returns>
        public static RequestParameter CreateRangeRequestParameter(string parmName, RangeVerifyHandler rangeVerifyImpl)
        {
            return CreateRangeRequestParameter(parmName, HttpContext.Current.Request[parmName], rangeVerifyImpl);
        }
        /// <summary>
        /// 创建默认的范围验证参数
        /// </summary>
        /// <param name="parmName"></param>
        /// <param name="parmValue"></param>
        /// <param name="rangeVerifyImpl"></param>
        /// <returns></returns>
        public static RequestParameter CreateRangeRequestParameter(string parmName, object parmValue, RangeVerifyHandler rangeVerifyImpl)
        {
            RequestParameter entity = new RequestParameter(parmName, parmValue);
            entity.VerifyHandler = RequestAndRangeVerify(entity, rangeVerifyImpl);
            return entity;
        }
        /// <summary>
        /// 参数不允许为空验证
        /// </summary>
        /// <param name="requestParameter">请求参数</param>
        /// <returns></returns>
        public static RuleVerifyHandler RequestVerify(RequestParameter requestParameter)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    throw new ArgumentNullException(requestParameter.ParameterName, "参数不允许为空！");
                }
            });
        }
        /// <summary>
        /// 参数不允许超出范围验证
        /// </summary>
        /// <param name="requestParameter">请求参数</param>
        /// <param name="rangeVerifyImpl">参数范围定义</param>
        /// <returns></returns>
        public static RuleVerifyHandler RangeVerify(RequestParameter requestParameter, RangeVerifyHandler rangeVerifyImpl)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                if (!rangeVerifyImpl(value))
                {
                    throw new ArgumentOutOfRangeException(requestParameter.ParameterName, "参数超出范围！");
                }
            });
        }
        /// <summary>
        /// 参数既不允许为空也不允许超出范围验证
        /// </summary>
        /// <param name="requestParameter">请求参数</param>
        /// <param name="rangeVerifyImpl">参数范围定义</param>
        /// <returns></returns>
        public static RuleVerifyHandler RequestAndRangeVerify(RequestParameter requestParameter, RangeVerifyHandler rangeVerifyImpl)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                //1、要求参数验证
                RequestVerify(requestParameter)(value);
                //2、范围验证
                RangeVerify(requestParameter, rangeVerifyImpl)(value);
            });
        }
        /// <summary>
        /// 默认数组类型范围的参数验证
        /// </summary>
        /// <param name="range">数组</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static RangeVerifyHandler EnumTypeRangeVerify(object[] range)
        {
            return new RangeVerifyHandler(delegate(object value)
                {
                    return (Array.IndexOf(range, value) != -1);
                });
        }

        private static RangeVerifyHandler Int32RangeVerifyInstance = new RangeVerifyHandler(delegate(object value)
               {
                   bool isException = false;
                   try
                   {
                       Convert.ToInt32(value);
                   }
                   catch
                   {
                       isException = true;
                   }
                   return !isException;
               });
        /// <summary>
        /// 整数类型的范围验证
        /// </summary>
        /// <returns></returns>
        public static RangeVerifyHandler Int32RangeVerify
        {
            get
            {
                return Int32RangeVerifyInstance;
            }
        }

        private static RangeVerifyHandler PositiveInt32RangeVerifyInstance = new RangeVerifyHandler(delegate(object value)
               {
                   bool isException = false;
                   //1、整型校验
                   isException = Int32RangeVerify(value);
                   if (!isException)
                       return false;
                   //2、正整数验证
                   isException = (Convert.ToInt32(value) <= 0);
                   return !isException;
               });
        /// <summary>
        /// 正整数类型的范围验证
        /// </summary>
        /// <returns></returns>
        public static RangeVerifyHandler PositiveInt32RangeVerify
        {
            get
            {
                return PositiveInt32RangeVerifyInstance;
            }
        }

        private static RangeVerifyHandler NaturalInt32RangeVerifyInstance = new RangeVerifyHandler(delegate(object value)
               {
                   bool isException = false;
                   //1、整型校验
                   isException = Int32RangeVerify(value);
                   if (!isException)
                       return false;
                   //2、自然数验证
                   isException = (Convert.ToInt32(value) < 0);
                   return !isException;
               });
        /// <summary>
        /// 自然数类型的范围验证
        /// </summary>
        /// <returns></returns>
        public static RangeVerifyHandler NaturalInt32RangeVerify
        {
            get
            {
                return NaturalInt32RangeVerifyInstance;
            }
        }
    }

    #endregion

    /// <summary>
    /// 基础页面
    /// </summary>
    public class BasePage : Page
    {
        private string ErrorMessage = "";

        public const String REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$";  //电子邮件校验常量
        public const String REGEXP_IS_VALID_URL = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";    //网址校验常量
        public const String REGEXP_IS_VALID_ZIP = @"\d{6}";											//邮编校验常量
        public const String REGEXP_IS_VALID_SSN = @"\d{18}|\d{15}";									//身份证校验常量	
        public const String REGEXP_IS_VALID_INT = @"^\d{1,}$";										//整数校验常量
        public const String REGEXP_IS_VALID_DEMICAL = @"^-?(0|\d+)(\.\d+)?$";							//数值校验常量 "^[-+]?\d*\.?\d*$"//^-?\d+(\.\d+)?$
        public const String REGEXP_IS_VALID_DATE = @"^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\/|-|\.)(?:0?2\1(?:29))$)|(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:(?:0?[13578]|1[02])\2(?:31))|(?:(?:0?[1,3-9]|1[0-2])\2(29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\2(?:0?[1-9]|1\d|2[0-8]))$";
        public const String REGEXP_IS_VALID_PHONE = @"(\(\d{3}\)|\d{3}-)?\d{8}";						//电话校验常量(有问题)	
        private const String REGEXP_IS_VALID_USERNAME = @"^\w+((-\w+)|(\.\w+))*\w+$";
        private const String REGEXP_IS_VALID_USERPASSWORD = @"^[A-Za-z0-9]+((-[A-Za-z0-9]+)|(\.[A-Za-z0-9]+))*[A-Za-z0-9]+$";


        public BasePage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //CheckPower(this);
        }

        #region 获取页面传递参数的值，参数值的编码和解码等


        /// <summary>
        /// 对某个数据进行编码，主要是针对页面传递的参数值进行
        /// 用UrlParamDecode方法进行解码
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string UrlParamEncode(string paramValue)
        {
            return HttpUtility.UrlEncode(paramValue);
        }

        /// <summary>
        /// 对某个数据进行解码，主要是针对页面传递的参数值进行
        /// </summary>
        /// <param name="paramValue">要解码的内容</param>
        /// <returns></returns>
        public static string UrlParamDecode(string paramValue)
        {
            return HttpUtility.UrlDecode(paramValue);
        }

        /// <summary>
        /// 从页面URL(Request)中获取指定参数的值:
        /// 将参数值转换成安全的参数,防止SQL注入
        /// 如果参数不存在,或者参数的类型不一致,返回null
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>字符串</returns>
        public static string getSafeRequest(Page page, string paramName)
        {
            return getSafeRequest(page, paramName, false);
        }


        /// <summary>
        /// 生成安全的SQL查询条件，防止SQL注入
        /// </summary>
        /// <param name="sqlValue"></param>
        /// <returns></returns>
        public static string getSafeSQLValue(string sqlValue)
        {
            //先将单引号'的内码chr(27)替换成单引号'
            sqlValue = sqlValue.Replace("chr(27)", "'");
            sqlValue = sqlValue.Replace("Chr(27)", "'");
            //防止SQL注入
            sqlValue = sqlValue.Replace("'", "''");
            return sqlValue;
        }


        /// <summary>
        /// 从页面URL(Request)中获取指定参数的值:
        /// 将参数值转换成安全的参数,防止SQL注入
        /// 如果参数不存在,或者参数的类型不一致,返回null
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="isEncode">是否要进行解码，如果是true，调用方法UrlParamDecode解码</param>
        /// <returns>字符串</returns>
        public static string getSafeRequest(Page page, string paramName, bool isDecode)
        {
            string paramValue = null;
            if (page.Request[paramName] != null)
            {
                paramValue = page.Request[paramName].ToString();
                //防止SQL注入
                paramValue = getSafeSQLValue(paramValue);
                //解码
                if (isDecode)
                    paramValue = UrlParamDecode(paramValue);
            }
            return paramValue;
        }

        #endregion

        #region 查询条件的自动生成



        /// <summary>
        /// 根据查询字段的ID，按照指定的命名规则，获取对应的字段名称
        /// </summary>
        /// <param name="fieldID">页面的控件ID名称</param>
        /// <returns></returns>
        public static string getFieldNameFromQueryFieldID(string fieldID)
        {
            string fieldName = fieldID;
            int loc = fieldID.IndexOf("_");
            if (loc > 0)
                fieldName = fieldID.Substring(loc + 1);

            return fieldName.Replace("999", ".");
        }


        /// <summary>
        /// 生成查询条件,查询条件都放到HtmlTable里，控件的ID命名规则：前缀_字段名
        /// 如果是查询时间的区间，起始时间的前缀为begin_，截止时间为end_
        /// 如果要加表名或者表的别名，表名和字段名之间用三个999分隔，比如a999FieldName,系统会自动把999替换为小数点.
        /// </summary>
        /// <param name="tableQueryControlList">要生成的查询条件的表名称，类型为System.Web.UI.HtmlControls.HtmlTable</param>
        /// <returns>所有的查询条件的组合，默认是与的关系，即是 AND；即如果有条件，则生成与 AND 开头的一个查询条件</returns>
        public static string getQueryConditionFromQueryControlList(System.Web.UI.HtmlControls.HtmlTable tableQueryControlList)
        {
            string condition = "";
            string fieldLikeModal = "{0} like '%{1}%'";
            string fieldNoLikeModal = "{0} {1} '{2}'";
            for (int row = 0; row < tableQueryControlList.Rows.Count; row++)
            {
                //System.Web.UI.WebControls.
                string rowIndex = tableQueryControlList.Rows[row].ToString();
                HtmlTableRow tableRow = tableQueryControlList.Rows[row];
                for (int col = 0; col < tableRow.Cells.Count; col++)
                {
                    HtmlTableCell tc = tableRow.Cells[col];
                    for (int id = 0; id < tc.Controls.Count; id++)
                    {
                        string controlType = tc.Controls[id].GetType().Name;
                        string fieldValue = "";
                        string fieldName = "";
                        switch (controlType)
                        {
                            case "TextBox":
                                TextBox textBox = (TextBox)tc.Controls[id];
                                //取查询内容
                                fieldValue = textBox.Text.Trim();
                                if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(textBox.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    //防止SQL注入
                                    fieldValue = getSafeSQLValue(fieldValue);
                                    //生成查询添加
                                    condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                                }
                                break;
                            //case "AjaxTextBox":
                            //    AjaxTextBox aTextBox = (AjaxTextBox)tc.Controls[id];
                            //    //取查询内容
                            //    fieldValue = aTextBox.Text.Trim();
                            //    if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                            //        continue;
                            //    //取字段名称
                            //    fieldName = getFieldNameFromQueryFieldID(aTextBox.ID);
                            //    if ((fieldName != "") && (fieldValue != ""))
                            //    {
                            //        if (condition != "")
                            //            condition += " AND ";
                            //        //防止SQL注入
                            //        fieldValue = getSafeSQLValue(fieldValue);
                            //        //生成查询添加
                            //        condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                            //    }
                            //    break;
                            case "DropDownList":
                                DropDownList ddl0 = (DropDownList)tc.Controls[id];
                                fieldValue = ddl0.SelectedValue;
                                if (fieldValue == "-1")//如果没有选择查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(ddl0.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            case "DictionaryDropDownList":
                                DictionaryDropDownList ddl = (DictionaryDropDownList)tc.Controls[id];
                                fieldValue = ddl.SelectedValue;
                                if (fieldValue == "-1")//如果没有选择查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(ddl.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            case "DateTimeTextBox":
                                DateTimeTextBox dtt = (DateTimeTextBox)tc.Controls[id];
                                fieldValue = dtt.Text.Trim();
                                if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                                    continue;
                                string fieldID = dtt.ID;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(fieldID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    if (fieldID.ToLower().StartsWith("begin_"))
                                    {
                                        condition += string.Format(fieldNoLikeModal, fieldName, ">=", fieldValue);
                                    }
                                    else if (fieldID.ToLower().StartsWith("end_"))
                                    {
                                        try
                                        {
                                            DateTime dt = Convert.ToDateTime(fieldValue);
                                            if (dt.TimeOfDay.Seconds == 0)//没有时间，取下一天
                                                fieldValue = (dt.AddDays(1)).ToString("yyyy-MM-dd");
                                            condition += string.Format(fieldNoLikeModal, fieldName, "<", fieldValue);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    else
                                        condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }

            }
            if (condition != "")
                condition = " AND " + condition;
            return condition;
        }

        /// <summary>
        /// 生成查询条件,查询条件都放到HtmlTable里，控件的ID命名规则：前缀_字段名
        /// 如果是查询时间的区间，起始时间的前缀为begin_，截止时间为end_
        /// 如果要加表名或者表的别名，表名和字段名之间用三个999分隔，比如a999FieldName,系统会自动把999替换为小数点.
        /// </summary>
        /// <param name="tableQueryControlList">要生成的查询条件的表名称，类型为System.Web.UI.HtmlControls.HtmlTable</param>
        /// <returns>所有的查询条件的组合，默认是与的关系，即是 AND；即如果有条件，则生成与 AND 开头的一个查询条件</returns>
        public static string getQueryConditionFromQueryControlListNew(System.Web.UI.HtmlControls.HtmlTable tableQueryControlList)
        {
            string condition = "";
            string fieldLikeModal = "{0} like '%{1}%'";
            string fieldNoLikeModal = "{0} {1} '{2}'";
            for (int row = 0; row < tableQueryControlList.Rows.Count; row++)
            {
                //System.Web.UI.WebControls.
                string rowIndex = tableQueryControlList.Rows[row].ToString();
                HtmlTableRow tableRow = tableQueryControlList.Rows[row];
                for (int col = 0; col < tableRow.Cells.Count; col++)
                {
                    HtmlTableCell tc = tableRow.Cells[col];
                    for (int id = 0; id < tc.Controls.Count; id++)
                    {
                        string controlType = tc.Controls[id].GetType().Name;
                        string fieldValue = "";
                        string fieldName = "";
                        switch (controlType)
                        {
                            case "TextBox":
                                TextBox textBox = (TextBox)tc.Controls[id];
                                //取查询内容
                                fieldValue = textBox.Text.Trim();
                                if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(textBox.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    //防止SQL注入
                                    fieldValue = getSafeSQLValue(fieldValue);
                                    //生成查询添加
                                    condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                                }
                                break;
                            //case "AjaxTextBox":
                            //    AjaxTextBox aTextBox = (AjaxTextBox)tc.Controls[id];
                            //    //取查询内容
                            //    fieldValue = aTextBox.Text.Trim();
                            //    if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                            //        continue;
                            //    //取字段名称
                            //    fieldName = getFieldNameFromQueryFieldID(aTextBox.ID);
                            //    if ((fieldName != "") && (fieldValue != ""))
                            //    {
                            //        if (condition != "")
                            //            condition += " AND ";
                            //        //防止SQL注入
                            //        fieldValue = getSafeSQLValue(fieldValue);
                            //        //生成查询添加
                            //        condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                            //    }
                            //    break;
                            case "DropDownList":
                                DropDownList ddl0 = (DropDownList)tc.Controls[id];
                                fieldValue = ddl0.SelectedValue;
                                if (fieldValue == "-1")//如果没有选择查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(ddl0.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            case "DictionaryDropDownList":
                                DictionaryDropDownList ddl = (DictionaryDropDownList)tc.Controls[id];
                                fieldValue = ddl.SelectedValue;
                                if (fieldValue == "-1")//如果没有选择查询条件，则默认为不查询，直接取下一个
                                    continue;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(ddl.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            case "DateTimeTextBox":
                                DateTimeTextBox dtt = (DateTimeTextBox)tc.Controls[id];
                                fieldValue = dtt.Text.Trim();
                                if (fieldValue == "")//如果没有输入查询条件，则默认为不查询，直接取下一个
                                    continue;
                                string fieldID = dtt.ID;
                                //取字段名称
                                fieldName = getFieldNameFromQueryFieldID(fieldID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (fieldID.ToLower().StartsWith("begin_"))
                                    {
                                        if (condition != "")
                                            condition += " AND ";
                                        condition += string.Format(fieldNoLikeModal, fieldName, ">=", fieldValue);
                                    }
                                    else if (fieldID.ToLower().StartsWith("end_"))
                                    {
                                        try
                                        {
                                            DateTime dt = Convert.ToDateTime(fieldValue);
                                            if (dt.TimeOfDay.Seconds == 0)//没有时间，取下一天
                                                fieldValue = (dt.AddDays(1)).ToString("yyyy-MM-dd");
                                            if (condition != "")
                                                condition += " AND ";
                                            condition += string.Format(fieldNoLikeModal, fieldName, "<", fieldValue);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    //else
                                    //    condition += string.Format(fieldNoLikeModal, fieldName, "=", fieldValue);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }

            }
            if (condition != "")
                condition = " AND " + condition;
            return condition;
        }
        #endregion



        #region 页面参数验证机制
        /// <summary>
        /// 需进行页面参数验证的参数列表
        /// 子类通过重新此方法来注入页面参数
        /// </summary>
        protected virtual RequestParameter[] PageRequestArgs
        {
            get
            {
                return new RequestParameter[] { };
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.PreLoad += new EventHandler(delegate(object sender, EventArgs ex)
            {
                foreach (RequestParameter requestParm in this.PageRequestArgs)
                {
                    requestParm.Verify();
                }
            });
        }
        #endregion
        #region 文件上传集成
        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="functionType">功能配置名称</param>
        /// <returns></returns>
        public virtual ETMS.Utility.Service.FileUpload.FileUploadCard SaveUploadFiles(string functionType)
        {
            //如果功能配置名称不为空，则追加"_"分隔符
            string functionConfigName = "_" + functionType.ToString();
            ETMS.Utility.Service.FileUpload.IFileUploadService fileUploadService = ETMS.Utility.Service.ServiceRepository.FileUploadService;
            return fileUploadService.Save(Request.Form["FileUpload_ID" + functionConfigName]);
        }
        #endregion

        #region 将某列中相邻行中单元各内容相同的多行合并为一行
        /// <summary>
        /// 合并单元格
        /// 将某列中相邻行中单元各内容相同的多行合并为一行
        /// </summary>
        /// <remarks>
        /// 创建：李连友      2004-04-20
        /// </remarks>
        /// <param name="MyDataGrid">网格控件名称</param>
        /// <param name="ColumnIndex">列号：起始为 0 例如：为ColumnIndex = 3 表示 第 4 列</param>
        public static void DataGridRowSpan(DataGrid MyDataGrid, int ColumnIndex)
        {
            string strTmp = "";
            int SpanCount = 1;
            int SpanStart = 1;
            int SpanOffSet = 0;
            int RowIndex = 0;

            for (RowIndex = 0; RowIndex < MyDataGrid.Items.Count; RowIndex++)
            {

                MyDataGrid.Items[RowIndex].Cells[ColumnIndex].BackColor = Color.AliceBlue;
                MyDataGrid.Items[RowIndex].Cells[ColumnIndex].ForeColor = Color.Black;

                if (strTmp == MyDataGrid.Items[RowIndex].Cells[ColumnIndex].Text)
                {
                    SpanCount++;
                }
                else
                {
                    strTmp = MyDataGrid.Items[RowIndex].Cells[ColumnIndex].Text;
                    if (RowIndex != 0)
                    {
                        MyDataGrid.Items[SpanStart].Cells[ColumnIndex].RowSpan = SpanCount;
                        for (SpanOffSet = 1; SpanOffSet < SpanCount; SpanOffSet++)
                        {
                            MyDataGrid.Items[SpanStart + SpanOffSet].Cells[ColumnIndex].Visible = false;
                        }
                    }
                    SpanStart = RowIndex;
                    SpanCount = 1;
                }
            }
            if (RowIndex != 0)
            {
                MyDataGrid.Items[SpanStart].Cells[ColumnIndex].RowSpan = SpanCount;
                for (SpanOffSet = 1; SpanOffSet < SpanCount; SpanOffSet++)
                {
                    MyDataGrid.Items[SpanStart + SpanOffSet].Cells[ColumnIndex].Visible = false;
                }
            }
        }
        #endregion

        #region 将某列中相邻行中单元各内容相同的多行合并为一行,空行不合并
        /// <summary>
        /// 合并单元格
        /// 将某列中相邻行中单元各内容相同的多行合并为一行
        /// </summary>
        /// <remarks>
        /// 创建：李连友      2004-04-20
        /// </remarks>
        /// <param name="MyDataGrid">网格控件名称</param>
        /// <param name="ColumnIndex">列号：起始为 0 例如：为ColumnIndex = 3 表示 第 4 列</param>
        public static void DataGridRowSpanValueNotNull(DataGrid MyDataGrid, int ColumnIndex)
        {
            string strTmp = "";
            int SpanCount = 1;
            int SpanStart = 1;
            int SpanOffSet = 0;
            int RowIndex = 0;

            for (RowIndex = 0; RowIndex < MyDataGrid.Items.Count; RowIndex++)
            {

                if (strTmp == MyDataGrid.Items[RowIndex].Cells[ColumnIndex].Text && MyDataGrid.Items[RowIndex].Cells[ColumnIndex].Text != "")
                {
                    SpanCount++;
                }
                else
                {
                    strTmp = MyDataGrid.Items[RowIndex].Cells[ColumnIndex].Text;
                    if (RowIndex != 0)
                    {
                        MyDataGrid.Items[SpanStart].Cells[ColumnIndex].RowSpan = SpanCount;
                        for (SpanOffSet = 1; SpanOffSet < SpanCount; SpanOffSet++)
                        {
                            MyDataGrid.Items[SpanStart + SpanOffSet].Cells[ColumnIndex].Visible = false;
                        }
                    }
                    SpanStart = RowIndex;
                    SpanCount = 1;
                }
            }
            if (RowIndex != 0)
            {
                MyDataGrid.Items[SpanStart].Cells[ColumnIndex].RowSpan = SpanCount;
                //MyDataGrid.Items[SpanStart].Cells[ColumnIndex].CssClass="EE";
                for (SpanOffSet = 1; SpanOffSet < SpanCount; SpanOffSet++)
                {
                    MyDataGrid.Items[SpanStart + SpanOffSet].Cells[ColumnIndex].Visible = false;
                }
            }
        }
        #endregion

        #region 客户端消息框提示
        /// <summary>
        /// 获得错误提示信息字符串
        /// </summary>		
        public string GetErrorDescription()
        {
            return ErrorMessage.Replace("#%@", "\\n").Replace("###", "");
        }
        /// <summary>
        /// 提示消息信息框
        /// </summary>
        /// <param name="Message">消息字符串</param>
        public static void MessgeBox(string Message, Page page)
        {
            page.RegisterStartupScript("showMessage1", "<script languge='javascript'>alert('" + Message + "');</script>");
        }

        /// <summary>
        /// 提示消息信息框，并关闭当前窗口
        /// </summary>
        /// <param name="Message">消息字符串</param>
        /// <param name="IsCloseWindow">是否关闭当前窗口</param>
        public static void MessgeBox(string Message, bool IsCloseWindow, Page page)
        {
            page.RegisterStartupScript("showMessage2", "<script languge='javascript'>alert('" + Message + "');window.close();</script>");
        }

        /// <summary>
        /// 提示消息信息框，并返回要刷新的地址
        /// </summary>
        /// <param name="Message">消息字符串</param>
        /// <param name="linkURL">关闭后需要刷新的地址</param>
        public static void MessgeBox(string Message, string linkURL, Page page)
        {
            page.RegisterStartupScript("showMessage3", "<script languge='javascript'>alert('" + Message + "');window.location.href='" + linkURL + "';</script>");
        }

        /// <summary>
        /// 提示消息信息框，并关闭当前窗口
        /// </summary>
        /// <param name="Message">消息字符串</param>
        /// <param name="IsCloseWindow">是否关闭当前窗口</param>
        /// <param name="linkURL">关闭后需要刷新的地址</param>
        public static void MessgeBox(string Message, bool IsCloseWindow, string linkURL, Page page)
        {
            page.RegisterStartupScript("showMessage4", "<script languge='javascript'>alert('" + Message + "');window.close();window.returnValue='" + linkURL + "';</script>");
        }
        #endregion

        #region 校验字段是否为空 或 字段长度太短 或 字段长度太长 方法

        public static string GetFieldTooShortError(string ErrorField, int minlen)
        {
            return ErrorField + "信息太短，至少" + minlen.ToString() + "个字符！";
        }

        public bool IsValidField(string fieldValue, int minLen, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            if (i < 1 && (!AllowNull))
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldNullError(ErrorField) + "#%@";
                return false;
            }
            else
            {
                if (i < minLen)
                {
                    ErrorMessage = ErrorMessage + "###" + GetFieldTooShortError(ErrorField, minLen) + "#%@";
                    return false;
                }
                else if (i > maxLen)
                {
                    ErrorMessage = ErrorMessage + "###" + GetFieldTooLongError(ErrorField, maxLen) + "#%@";
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 校验两个字段是否相等 方法

        public string GetFieldNotSameError(string ErrorField1, string ErrorField2)
        {
            return ErrorField1 + "与" + ErrorField2 + "不相符！";
        }

        public bool IsValidSame(string fieldValue1, string fieldValue2, string ErrorField1, string ErrorField2)
        {

            if (fieldValue1 != fieldValue2)
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldNotSameError(ErrorField1, ErrorField2) + "#%@";
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

        #region 校验 是否输入重复 反馈方法
        public bool GetFieldNotUniqueError(string ErrorField)
        {
            ErrorMessage = ErrorMessage + "###" + ErrorField + "已经存在，请重新输入！" + "#%@";
            return false;
        }
        #endregion

        #region 校验字段是否为空 或 字段长度超长 方法

        public string GetFieldTooLongError(string ErrorField, int maxlen)
        {
            return ErrorField + "信息超长，请删减至" + maxlen.ToString() + "个字符！";
        }

        public static string GetFieldNullError(string ErrorField)
        {
            return ErrorField + "是必填项，不允许为空！";
        }

        public bool IsValidField(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (int)(fieldValue.ToString().Trim().Length);

            if (i < 1 && (!AllowNull))
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldNullError(ErrorField) + "#%@";
                return false;
            }
            else if (i > maxLen)
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldTooLongError(ErrorField, maxLen) + "#%@";
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 电子邮件 类型字段格式 方法

        public string GetEmailFieldError(string ErrorField)
        {
            return ErrorField + "格式不正确(a@b.c)！";
        }
        public bool IsValidEmail(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_EMAIL)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetEmailFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 邮编 类型字段格式 方法

        public string GetZipFieldError(string ErrorField)
        {
            return ErrorField + "格式不正确(157032)，请输入6位数字！";
        }
        public bool IsValidZip(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_ZIP)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetZipFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 身份证 类型字段格式 方法

        public string GetSSNFieldError(string ErrorField)
        {
            return ErrorField + "格式不正确(长度为15或18位)！";
        }
        public bool IsValidSSN(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_SSN)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetSSNFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 网址 类型字段格式 方法

        public string GetUrlFieldError(string ErrorField)
        {
            return ErrorField + "格式不正确(http://www.abc.com)！";
        }
        public bool IsValidUrl(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_URL)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetUrlFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 日期 类型字段格式 方法

        public string GetDateFieldError(string ErrorField)
        {
            return ErrorField + "日期格式不正确！";
        }
        public bool IsValidDate(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_DATE)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetDateFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 电话 类型字段格式 方法

        public string GetPhoneFieldError(string ErrorField)
        {
            return ErrorField + "电话格式不正确(010-89898989)！";
        }
        public bool IsValidPhone(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_PHONE)).IsMatch(fieldValue.ToString());

                if ((!isValid) && (i > 0))
                {
                    ErrorMessage = ErrorMessage + "###" + GetPhoneFieldError(ErrorField) + "#%@";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 校验 数值 类型字段格式 方法
        //这也是个判断数值的办法
        private bool IsNumeric(string Value)
        {
            try
            {
                int i = int.Parse(Value);
                return true;
            }
            catch
            { return false; }
        }

        public static string GetFieldNumberError(string ErrorField)
        {
            return ErrorField + "必须是数字(例如：90)！";
        }

        public bool IsValidNumber(string fieldValue, string ErrorField, bool AllowNull)
        {
            int i = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = (new Regex(REGEXP_IS_VALID_DEMICAL)).IsMatch(fieldValue.ToString());

            if (i < 1 && (!AllowNull))
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldNullError(ErrorField) + "#%@";
                return false;
            }
            else if ((!isValid) && (i > 0))
            {
                ErrorMessage = ErrorMessage + "###" + GetFieldNumberError(ErrorField) + "#%@";
                return false;
            }
            return true;
        }


        #endregion

        #region 判断用户名格式和长度
        private string GetFieldUserNameError(string ErrorField)
        {
            return ErrorField + "必须是字母或数字或_，并且长度至少6位(例如：abc_123)！";
        }




        /// <summary>
        /// 判断用户名格式是否正确
        /// </summary>
        /// <remarks>
        /// </remarks>		
        /// <param name="fieldValue">用户名字串</param> 
        /// <param name="maxLen">最大长度</param>
        /// <param name="ErrorField">错误信息名称</param> 
        /// <param name="AllowNull">是否为空</param>
        /// <returns>正确返回true;否则返回false</returns>
        public bool IsValidUserName(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int iFieldValueLength = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, 6, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_USERNAME)).IsMatch(fieldValue.ToString());

                if (iFieldValueLength < 1 && (!AllowNull))
                {
                    ErrorMessage = ErrorMessage + "###" + GetFieldNullError(ErrorField) + "#%@";
                    return false;
                }
                else
                {
                    if ((!isValid) && (iFieldValueLength > 0))
                    {
                        ErrorMessage = ErrorMessage + "###" + GetFieldUserNameError(ErrorField) + "#%@";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断密码格式和长度

        private string GetFieldUserPasswordError(string ErrorField)
        {
            return ErrorField + "必须是字母或数字,并且长度至少为6位(例如：abc123)！";
        }


        /// <summary>
        /// 判断口令格式是否正确
        /// </summary>
        /// <remarks>
        /// </remarks>		
        /// <param name="fieldValue">口令字串</param> 
        /// <param name="maxLen">最大长度</param>
        /// <param name="ErrorField">错误信息名称</param> 
        /// <param name="AllowNull">是否为空</param>
        /// <returns>正确返回true;否则返回false</returns>
        public bool IsValidUserPassword(string fieldValue, int maxLen, string ErrorField, bool AllowNull)
        {
            int iFieldValueLength = (short)(fieldValue.ToString().Trim().Length);

            bool isValid = IsValidField(fieldValue, 6, maxLen, ErrorField, AllowNull);

            if (isValid)
            {
                isValid = (new Regex(REGEXP_IS_VALID_USERPASSWORD)).IsMatch(fieldValue.ToString());

                if (iFieldValueLength < 1 && (!AllowNull))
                {
                    ErrorMessage = ErrorMessage + "###" + GetFieldNullError(ErrorField) + "#%@";
                    return false;
                }
                else
                {
                    if ((!isValid) && (iFieldValueLength > 0))
                    {
                        ErrorMessage = ErrorMessage + "###" + GetFieldUserPasswordError(ErrorField) + "#%@";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 下拉列表绑定
        public static void SetDropDownList(DataTable source, DropDownList ddlControl, string textField, string valueFeild, bool isShowAll)
        {
            ddlControl.DataSource = source;
            ddlControl.DataTextField = textField;
            ddlControl.DataValueField = valueFeild;
            ddlControl.DataBind();
            if (isShowAll)
            {
                ddlControl.Items.Insert(0, new ListItem("全部", "*"));
            }
        }
        #endregion
    }

}
