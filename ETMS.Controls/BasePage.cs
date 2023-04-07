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
    #region ҳ���������������

    /// <summary>
    /// ��������У�鴦����
    /// </summary>
    /// <param name="value">����ֵ</param>
    /// <returns></returns>
    public delegate void RuleVerifyHandler(object value);
    public delegate bool RangeVerifyHandler(object value);
    /// <summary>
    /// ҳ����������������
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
        /// ������
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
        /// ����ֵ
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
        /// ����У�����
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
        /// У��
        /// </summary>
        public void Verify()
        {
            if (this.m_RuleVerifyHandler != null)
            {
                this.m_RuleVerifyHandler(this.m_ParameterValue);
            }
        }

        /// <summary>
        /// ����Ĭ�ϵ��������
        /// </summary>
        /// <param name="parmName">�������ƣ�Ĭ�ϴ�Request[]�м���</param>
        /// <returns></returns>
        public static RequestParameter CreateDefaultRequestParameter(string parmName)
        {
            return CreateDefaultRequestParameter(parmName, HttpContext.Current.Request[parmName]);
        }
        /// <summary>
        /// ����Ĭ�ϵ��������
        /// </summary>
        /// <param name="parmName">��������</param>
        /// <param name="parmValue">����ֵ</param>
        /// <returns></returns>
        public static RequestParameter CreateDefaultRequestParameter(string parmName, object parmValue)
        {
            RequestParameter entity = new RequestParameter(parmName, parmValue);
            entity.VerifyHandler = RequestVerify(entity);
            return entity;
        }
        /// <summary>
        /// ����һ��Ĭ�ϵ��������
        /// </summary>
        /// <param name="parmNames">һ���������</param>
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
        /// ����Ĭ�ϵķ�Χ��֤����
        /// </summary>
        /// <param name="parmName">��������Ĭ�ϴ�Request[]�м���</param>
        /// <param name="rangeVerifyImpl"></param>
        /// <returns></returns>
        public static RequestParameter CreateRangeRequestParameter(string parmName, RangeVerifyHandler rangeVerifyImpl)
        {
            return CreateRangeRequestParameter(parmName, HttpContext.Current.Request[parmName], rangeVerifyImpl);
        }
        /// <summary>
        /// ����Ĭ�ϵķ�Χ��֤����
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
        /// ����������Ϊ����֤
        /// </summary>
        /// <param name="requestParameter">�������</param>
        /// <returns></returns>
        public static RuleVerifyHandler RequestVerify(RequestParameter requestParameter)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    throw new ArgumentNullException(requestParameter.ParameterName, "����������Ϊ�գ�");
                }
            });
        }
        /// <summary>
        /// ��������������Χ��֤
        /// </summary>
        /// <param name="requestParameter">�������</param>
        /// <param name="rangeVerifyImpl">������Χ����</param>
        /// <returns></returns>
        public static RuleVerifyHandler RangeVerify(RequestParameter requestParameter, RangeVerifyHandler rangeVerifyImpl)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                if (!rangeVerifyImpl(value))
                {
                    throw new ArgumentOutOfRangeException(requestParameter.ParameterName, "����������Χ��");
                }
            });
        }
        /// <summary>
        /// �����Ȳ�����Ϊ��Ҳ����������Χ��֤
        /// </summary>
        /// <param name="requestParameter">�������</param>
        /// <param name="rangeVerifyImpl">������Χ����</param>
        /// <returns></returns>
        public static RuleVerifyHandler RequestAndRangeVerify(RequestParameter requestParameter, RangeVerifyHandler rangeVerifyImpl)
        {
            return new RuleVerifyHandler(delegate(object value)
            {
                //1��Ҫ�������֤
                RequestVerify(requestParameter)(value);
                //2����Χ��֤
                RangeVerify(requestParameter, rangeVerifyImpl)(value);
            });
        }
        /// <summary>
        /// Ĭ���������ͷ�Χ�Ĳ�����֤
        /// </summary>
        /// <param name="range">����</param>
        /// <param name="value">����ֵ</param>
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
        /// �������͵ķ�Χ��֤
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
                   //1������У��
                   isException = Int32RangeVerify(value);
                   if (!isException)
                       return false;
                   //2����������֤
                   isException = (Convert.ToInt32(value) <= 0);
                   return !isException;
               });
        /// <summary>
        /// ���������͵ķ�Χ��֤
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
                   //1������У��
                   isException = Int32RangeVerify(value);
                   if (!isException)
                       return false;
                   //2����Ȼ����֤
                   isException = (Convert.ToInt32(value) < 0);
                   return !isException;
               });
        /// <summary>
        /// ��Ȼ�����͵ķ�Χ��֤
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
    /// ����ҳ��
    /// </summary>
    public class BasePage : Page
    {
        private string ErrorMessage = "";

        public const String REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$";  //�����ʼ�У�鳣��
        public const String REGEXP_IS_VALID_URL = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";    //��ַУ�鳣��
        public const String REGEXP_IS_VALID_ZIP = @"\d{6}";											//�ʱ�У�鳣��
        public const String REGEXP_IS_VALID_SSN = @"\d{18}|\d{15}";									//���֤У�鳣��	
        public const String REGEXP_IS_VALID_INT = @"^\d{1,}$";										//����У�鳣��
        public const String REGEXP_IS_VALID_DEMICAL = @"^-?(0|\d+)(\.\d+)?$";							//��ֵУ�鳣�� "^[-+]?\d*\.?\d*$"//^-?\d+(\.\d+)?$
        public const String REGEXP_IS_VALID_DATE = @"^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\/|-|\.)(?:0?2\1(?:29))$)|(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:(?:0?[13578]|1[02])\2(?:31))|(?:(?:0?[1,3-9]|1[0-2])\2(29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\2(?:0?[1-9]|1\d|2[0-8]))$";
        public const String REGEXP_IS_VALID_PHONE = @"(\(\d{3}\)|\d{3}-)?\d{8}";						//�绰У�鳣��(������)	
        private const String REGEXP_IS_VALID_USERNAME = @"^\w+((-\w+)|(\.\w+))*\w+$";
        private const String REGEXP_IS_VALID_USERPASSWORD = @"^[A-Za-z0-9]+((-[A-Za-z0-9]+)|(\.[A-Za-z0-9]+))*[A-Za-z0-9]+$";


        public BasePage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //CheckPower(this);
        }

        #region ��ȡҳ�洫�ݲ�����ֵ������ֵ�ı���ͽ����


        /// <summary>
        /// ��ĳ�����ݽ��б��룬��Ҫ�����ҳ�洫�ݵĲ���ֵ����
        /// ��UrlParamDecode�������н���
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string UrlParamEncode(string paramValue)
        {
            return HttpUtility.UrlEncode(paramValue);
        }

        /// <summary>
        /// ��ĳ�����ݽ��н��룬��Ҫ�����ҳ�洫�ݵĲ���ֵ����
        /// </summary>
        /// <param name="paramValue">Ҫ���������</param>
        /// <returns></returns>
        public static string UrlParamDecode(string paramValue)
        {
            return HttpUtility.UrlDecode(paramValue);
        }

        /// <summary>
        /// ��ҳ��URL(Request)�л�ȡָ��������ֵ:
        /// ������ֵת���ɰ�ȫ�Ĳ���,��ֹSQLע��
        /// �������������,���߲��������Ͳ�һ��,����null
        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="paramName">��������</param>
        /// <returns>�ַ���</returns>
        public static string getSafeRequest(Page page, string paramName)
        {
            return getSafeRequest(page, paramName, false);
        }


        /// <summary>
        /// ���ɰ�ȫ��SQL��ѯ��������ֹSQLע��
        /// </summary>
        /// <param name="sqlValue"></param>
        /// <returns></returns>
        public static string getSafeSQLValue(string sqlValue)
        {
            //�Ƚ�������'������chr(27)�滻�ɵ�����'
            sqlValue = sqlValue.Replace("chr(27)", "'");
            sqlValue = sqlValue.Replace("Chr(27)", "'");
            //��ֹSQLע��
            sqlValue = sqlValue.Replace("'", "''");
            return sqlValue;
        }


        /// <summary>
        /// ��ҳ��URL(Request)�л�ȡָ��������ֵ:
        /// ������ֵת���ɰ�ȫ�Ĳ���,��ֹSQLע��
        /// �������������,���߲��������Ͳ�һ��,����null
        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="paramName">��������</param>
        /// <param name="isEncode">�Ƿ�Ҫ���н��룬�����true�����÷���UrlParamDecode����</param>
        /// <returns>�ַ���</returns>
        public static string getSafeRequest(Page page, string paramName, bool isDecode)
        {
            string paramValue = null;
            if (page.Request[paramName] != null)
            {
                paramValue = page.Request[paramName].ToString();
                //��ֹSQLע��
                paramValue = getSafeSQLValue(paramValue);
                //����
                if (isDecode)
                    paramValue = UrlParamDecode(paramValue);
            }
            return paramValue;
        }

        #endregion

        #region ��ѯ�������Զ�����



        /// <summary>
        /// ���ݲ�ѯ�ֶε�ID������ָ�����������򣬻�ȡ��Ӧ���ֶ�����
        /// </summary>
        /// <param name="fieldID">ҳ��Ŀؼ�ID����</param>
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
        /// ���ɲ�ѯ����,��ѯ�������ŵ�HtmlTable��ؼ���ID��������ǰ׺_�ֶ���
        /// ����ǲ�ѯʱ������䣬��ʼʱ���ǰ׺Ϊbegin_����ֹʱ��Ϊend_
        /// ���Ҫ�ӱ������߱�ı������������ֶ���֮��������999�ָ�������a999FieldName,ϵͳ���Զ���999�滻ΪС����.
        /// </summary>
        /// <param name="tableQueryControlList">Ҫ���ɵĲ�ѯ�����ı����ƣ�����ΪSystem.Web.UI.HtmlControls.HtmlTable</param>
        /// <returns>���еĲ�ѯ��������ϣ�Ĭ������Ĺ�ϵ������ AND����������������������� AND ��ͷ��һ����ѯ����</returns>
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
                                //ȡ��ѯ����
                                fieldValue = textBox.Text.Trim();
                                if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
                                fieldName = getFieldNameFromQueryFieldID(textBox.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    //��ֹSQLע��
                                    fieldValue = getSafeSQLValue(fieldValue);
                                    //���ɲ�ѯ���
                                    condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                                }
                                break;
                            //case "AjaxTextBox":
                            //    AjaxTextBox aTextBox = (AjaxTextBox)tc.Controls[id];
                            //    //ȡ��ѯ����
                            //    fieldValue = aTextBox.Text.Trim();
                            //    if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                            //        continue;
                            //    //ȡ�ֶ�����
                            //    fieldName = getFieldNameFromQueryFieldID(aTextBox.ID);
                            //    if ((fieldName != "") && (fieldValue != ""))
                            //    {
                            //        if (condition != "")
                            //            condition += " AND ";
                            //        //��ֹSQLע��
                            //        fieldValue = getSafeSQLValue(fieldValue);
                            //        //���ɲ�ѯ���
                            //        condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                            //    }
                            //    break;
                            case "DropDownList":
                                DropDownList ddl0 = (DropDownList)tc.Controls[id];
                                fieldValue = ddl0.SelectedValue;
                                if (fieldValue == "-1")//���û��ѡ���ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
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
                                if (fieldValue == "-1")//���û��ѡ���ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
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
                                if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                string fieldID = dtt.ID;
                                //ȡ�ֶ�����
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
                                            if (dt.TimeOfDay.Seconds == 0)//û��ʱ�䣬ȡ��һ��
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
        /// ���ɲ�ѯ����,��ѯ�������ŵ�HtmlTable��ؼ���ID��������ǰ׺_�ֶ���
        /// ����ǲ�ѯʱ������䣬��ʼʱ���ǰ׺Ϊbegin_����ֹʱ��Ϊend_
        /// ���Ҫ�ӱ������߱�ı������������ֶ���֮��������999�ָ�������a999FieldName,ϵͳ���Զ���999�滻ΪС����.
        /// </summary>
        /// <param name="tableQueryControlList">Ҫ���ɵĲ�ѯ�����ı����ƣ�����ΪSystem.Web.UI.HtmlControls.HtmlTable</param>
        /// <returns>���еĲ�ѯ��������ϣ�Ĭ������Ĺ�ϵ������ AND����������������������� AND ��ͷ��һ����ѯ����</returns>
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
                                //ȡ��ѯ����
                                fieldValue = textBox.Text.Trim();
                                if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
                                fieldName = getFieldNameFromQueryFieldID(textBox.ID);
                                if ((fieldName != "") && (fieldValue != ""))
                                {
                                    if (condition != "")
                                        condition += " AND ";
                                    //��ֹSQLע��
                                    fieldValue = getSafeSQLValue(fieldValue);
                                    //���ɲ�ѯ���
                                    condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                                }
                                break;
                            //case "AjaxTextBox":
                            //    AjaxTextBox aTextBox = (AjaxTextBox)tc.Controls[id];
                            //    //ȡ��ѯ����
                            //    fieldValue = aTextBox.Text.Trim();
                            //    if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                            //        continue;
                            //    //ȡ�ֶ�����
                            //    fieldName = getFieldNameFromQueryFieldID(aTextBox.ID);
                            //    if ((fieldName != "") && (fieldValue != ""))
                            //    {
                            //        if (condition != "")
                            //            condition += " AND ";
                            //        //��ֹSQLע��
                            //        fieldValue = getSafeSQLValue(fieldValue);
                            //        //���ɲ�ѯ���
                            //        condition += string.Format(fieldLikeModal, fieldName, fieldValue);
                            //    }
                            //    break;
                            case "DropDownList":
                                DropDownList ddl0 = (DropDownList)tc.Controls[id];
                                fieldValue = ddl0.SelectedValue;
                                if (fieldValue == "-1")//���û��ѡ���ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
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
                                if (fieldValue == "-1")//���û��ѡ���ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                //ȡ�ֶ�����
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
                                if (fieldValue == "")//���û�������ѯ��������Ĭ��Ϊ����ѯ��ֱ��ȡ��һ��
                                    continue;
                                string fieldID = dtt.ID;
                                //ȡ�ֶ�����
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
                                            if (dt.TimeOfDay.Seconds == 0)//û��ʱ�䣬ȡ��һ��
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



        #region ҳ�������֤����
        /// <summary>
        /// �����ҳ�������֤�Ĳ����б�
        /// ����ͨ�����´˷�����ע��ҳ�����
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
        #region �ļ��ϴ�����
        /// <summary>
        /// �����ϴ����ļ�
        /// </summary>
        /// <param name="functionType">������������</param>
        /// <returns></returns>
        public virtual ETMS.Utility.Service.FileUpload.FileUploadCard SaveUploadFiles(string functionType)
        {
            //��������������Ʋ�Ϊ�գ���׷��"_"�ָ���
            string functionConfigName = "_" + functionType.ToString();
            ETMS.Utility.Service.FileUpload.IFileUploadService fileUploadService = ETMS.Utility.Service.ServiceRepository.FileUploadService;
            return fileUploadService.Save(Request.Form["FileUpload_ID" + functionConfigName]);
        }
        #endregion

        #region ��ĳ�����������е�Ԫ��������ͬ�Ķ��кϲ�Ϊһ��
        /// <summary>
        /// �ϲ���Ԫ��
        /// ��ĳ�����������е�Ԫ��������ͬ�Ķ��кϲ�Ϊһ��
        /// </summary>
        /// <remarks>
        /// ������������      2004-04-20
        /// </remarks>
        /// <param name="MyDataGrid">����ؼ�����</param>
        /// <param name="ColumnIndex">�кţ���ʼΪ 0 ���磺ΪColumnIndex = 3 ��ʾ �� 4 ��</param>
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

        #region ��ĳ�����������е�Ԫ��������ͬ�Ķ��кϲ�Ϊһ��,���в��ϲ�
        /// <summary>
        /// �ϲ���Ԫ��
        /// ��ĳ�����������е�Ԫ��������ͬ�Ķ��кϲ�Ϊһ��
        /// </summary>
        /// <remarks>
        /// ������������      2004-04-20
        /// </remarks>
        /// <param name="MyDataGrid">����ؼ�����</param>
        /// <param name="ColumnIndex">�кţ���ʼΪ 0 ���磺ΪColumnIndex = 3 ��ʾ �� 4 ��</param>
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

        #region �ͻ�����Ϣ����ʾ
        /// <summary>
        /// ��ô�����ʾ��Ϣ�ַ���
        /// </summary>		
        public string GetErrorDescription()
        {
            return ErrorMessage.Replace("#%@", "\\n").Replace("###", "");
        }
        /// <summary>
        /// ��ʾ��Ϣ��Ϣ��
        /// </summary>
        /// <param name="Message">��Ϣ�ַ���</param>
        public static void MessgeBox(string Message, Page page)
        {
            page.RegisterStartupScript("showMessage1", "<script languge='javascript'>alert('" + Message + "');</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��Ϣ�򣬲��رյ�ǰ����
        /// </summary>
        /// <param name="Message">��Ϣ�ַ���</param>
        /// <param name="IsCloseWindow">�Ƿ�رյ�ǰ����</param>
        public static void MessgeBox(string Message, bool IsCloseWindow, Page page)
        {
            page.RegisterStartupScript("showMessage2", "<script languge='javascript'>alert('" + Message + "');window.close();</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��Ϣ�򣬲�����Ҫˢ�µĵ�ַ
        /// </summary>
        /// <param name="Message">��Ϣ�ַ���</param>
        /// <param name="linkURL">�رպ���Ҫˢ�µĵ�ַ</param>
        public static void MessgeBox(string Message, string linkURL, Page page)
        {
            page.RegisterStartupScript("showMessage3", "<script languge='javascript'>alert('" + Message + "');window.location.href='" + linkURL + "';</script>");
        }

        /// <summary>
        /// ��ʾ��Ϣ��Ϣ�򣬲��رյ�ǰ����
        /// </summary>
        /// <param name="Message">��Ϣ�ַ���</param>
        /// <param name="IsCloseWindow">�Ƿ�رյ�ǰ����</param>
        /// <param name="linkURL">�رպ���Ҫˢ�µĵ�ַ</param>
        public static void MessgeBox(string Message, bool IsCloseWindow, string linkURL, Page page)
        {
            page.RegisterStartupScript("showMessage4", "<script languge='javascript'>alert('" + Message + "');window.close();window.returnValue='" + linkURL + "';</script>");
        }
        #endregion

        #region У���ֶ��Ƿ�Ϊ�� �� �ֶγ���̫�� �� �ֶγ���̫�� ����

        public static string GetFieldTooShortError(string ErrorField, int minlen)
        {
            return ErrorField + "��Ϣ̫�̣�����" + minlen.ToString() + "���ַ���";
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

        #region У�������ֶ��Ƿ���� ����

        public string GetFieldNotSameError(string ErrorField1, string ErrorField2)
        {
            return ErrorField1 + "��" + ErrorField2 + "�������";
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

        #region У�� �Ƿ������ظ� ��������
        public bool GetFieldNotUniqueError(string ErrorField)
        {
            ErrorMessage = ErrorMessage + "###" + ErrorField + "�Ѿ����ڣ����������룡" + "#%@";
            return false;
        }
        #endregion

        #region У���ֶ��Ƿ�Ϊ�� �� �ֶγ��ȳ��� ����

        public string GetFieldTooLongError(string ErrorField, int maxlen)
        {
            return ErrorField + "��Ϣ��������ɾ����" + maxlen.ToString() + "���ַ���";
        }

        public static string GetFieldNullError(string ErrorField)
        {
            return ErrorField + "�Ǳ����������Ϊ�գ�";
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

        #region У�� �����ʼ� �����ֶθ�ʽ ����

        public string GetEmailFieldError(string ErrorField)
        {
            return ErrorField + "��ʽ����ȷ(a@b.c)��";
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

        #region У�� �ʱ� �����ֶθ�ʽ ����

        public string GetZipFieldError(string ErrorField)
        {
            return ErrorField + "��ʽ����ȷ(157032)��������6λ���֣�";
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

        #region У�� ���֤ �����ֶθ�ʽ ����

        public string GetSSNFieldError(string ErrorField)
        {
            return ErrorField + "��ʽ����ȷ(����Ϊ15��18λ)��";
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

        #region У�� ��ַ �����ֶθ�ʽ ����

        public string GetUrlFieldError(string ErrorField)
        {
            return ErrorField + "��ʽ����ȷ(http://www.abc.com)��";
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

        #region У�� ���� �����ֶθ�ʽ ����

        public string GetDateFieldError(string ErrorField)
        {
            return ErrorField + "���ڸ�ʽ����ȷ��";
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

        #region У�� �绰 �����ֶθ�ʽ ����

        public string GetPhoneFieldError(string ErrorField)
        {
            return ErrorField + "�绰��ʽ����ȷ(010-89898989)��";
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

        #region У�� ��ֵ �����ֶθ�ʽ ����
        //��Ҳ�Ǹ��ж���ֵ�İ취
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
            return ErrorField + "����������(���磺90)��";
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

        #region �ж��û�����ʽ�ͳ���
        private string GetFieldUserNameError(string ErrorField)
        {
            return ErrorField + "��������ĸ�����ֻ�_�����ҳ�������6λ(���磺abc_123)��";
        }




        /// <summary>
        /// �ж��û�����ʽ�Ƿ���ȷ
        /// </summary>
        /// <remarks>
        /// </remarks>		
        /// <param name="fieldValue">�û����ִ�</param> 
        /// <param name="maxLen">��󳤶�</param>
        /// <param name="ErrorField">������Ϣ����</param> 
        /// <param name="AllowNull">�Ƿ�Ϊ��</param>
        /// <returns>��ȷ����true;���򷵻�false</returns>
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

        #region �ж������ʽ�ͳ���

        private string GetFieldUserPasswordError(string ErrorField)
        {
            return ErrorField + "��������ĸ������,���ҳ�������Ϊ6λ(���磺abc123)��";
        }


        /// <summary>
        /// �жϿ����ʽ�Ƿ���ȷ
        /// </summary>
        /// <remarks>
        /// </remarks>		
        /// <param name="fieldValue">�����ִ�</param> 
        /// <param name="maxLen">��󳤶�</param>
        /// <param name="ErrorField">������Ϣ����</param> 
        /// <param name="AllowNull">�Ƿ�Ϊ��</param>
        /// <returns>��ȷ����true;���򷵻�false</returns>
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

        #region �����б��
        public static void SetDropDownList(DataTable source, DropDownList ddlControl, string textField, string valueFeild, bool isShowAll)
        {
            ddlControl.DataSource = source;
            ddlControl.DataTextField = textField;
            ddlControl.DataValueField = valueFeild;
            ddlControl.DataBind();
            if (isShowAll)
            {
                ddlControl.Items.Insert(0, new ListItem("ȫ��", "*"));
            }
        }
        #endregion
    }

}
