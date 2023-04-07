using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace ETMS.Controls
{
    /// <summary>
    /// ���CSS+DIV��ʽ��ҳ�����ݼ򵥰
    /// </summary>
    /// <example>����1������ԴSystem.Data.DataRowCollection
    /// <code lang="C#">
    ///     ETMS.Controls.DataListTag&lt;DataRow&gt; tag = new ETMS.Controls.DataListTag&lt;DataRow&gt;(); //1���������ݰ�ؼ�   
    ///     tag.DataSource = tag.ConvertIEnumerable((System.Data.DataTable.Rows)); //2����������Դ
    ///     tag.BindItem = new ETMS.Controls.BindItem&lt;System.Data.DataRow&gt;
    ///     (
    ///       delegate(System.Data.DataRow row, int rowIndex)
    ///       {
    ///                    return string.Format
    ///                    (
    ///                        "&lt;li style=\"border:{0};\"&gt;&lt;a href=\"{1}\"&gt;&lt;span&gt;{2}&lt;/span&gt;&lt;/a&gt;[{3}���ظ�]&lt;/li&gt;"//��ʽ����
    ///                        , new object[] 
    ///                        { 
    ///                         (rowIndex==1 ? "block" : "none")
    ///                            ,(string.Format(ShowTopicURLForamt, ConfigurationManager.AppSettings["JumpToBBSURL"], row["tid"]))
    ///                           ,(row["title"].ToString().Trim().Length &gt; TitleMaxLength ? row["title"].ToString().Trim().Substring(0, TitleMaxLength) + "..." : row["title"].ToString().Trim())
    ///                          ,row["replies"] 
    ///                       }
    ///                   );
    ///         }
    ///     ); //3��������������ʾ��ʽ
    ///     string html = tag.GetHtml();//4����ȡ��������
    /// </code>
    /// </example>
    /// <example>����2������Դ<code lang="C#">List&lt;Tb_e_Query&gt;()</code>
    /// <code lang="C#">
    ///     ETMS.Controls.DataListTag&lt;Tb_e_Query&gt; tag = new ETMS.Controls.DataListTag&lt;Tb_e_Query&gt;(); //1���������ݰ�ؼ�   
    ///     tag.DataSource = new List&lt;Tb_e_Query&gt;(); //2����������Դ
    ///     tag.BindItem = new ETMS.Controls.BindItem&lt;Tb_e_Query&gt;
    ///     (
    ///       
    ///                delegate(Tb_e_Query item, int rowIndex) 
    ///               {
    ///                   return string.Format("&lt;li&gt;&lt;a href=\"{0}\"&gt;{1}&lt;/a&gt;&lt;/li&gt;",
    ///                       new object[] { string.Format(ViewURLFormat, query.QueryID, ResourceType, ResourceCode).Replace("~", OES.LMS.WebBase.WebUtility.AppPath), item.QueryName });
    ///               }
    ///     ); //3��������������ʾ��ʽ
    ///     string html = tag.GetHtml();//4����ȡ��������
    /// </code>
    /// </example>
    public class DataListTag<T> :IDataBind
    {
        public DataListTag() { }
        public DataListTag(BindItem<T> bindItemFormat, IEnumerable<T> dataSource)
            : this(null, null, bindItemFormat, dataSource)
        {
        }
        public DataListTag(string header, string footer, BindItem<T> bindItemFormat, IEnumerable<T> dataSource)
        {
            this.m_Header = header;
            this.m_Footer = footer;
            this.m_BindItem = bindItemFormat;
            this.m_DataSource = dataSource;
        }
        public DataListTag(BindItem<T> bindItemFormat, IEnumerable dataSource)
            : this(null, null, bindItemFormat, dataSource)
        {
        }
        public DataListTag(string header, string footer, BindItem<T> bindItemFormat, IEnumerable dataSource)
        {
            this.m_Header = header;
            this.m_Footer = footer;
            this.m_BindItem = bindItemFormat;
            this.m_DataSource = dataSource;
        }
        private string m_Header;
        /// <summary>
        /// ��ͷ
        /// </summary>
        public string Header
        {
            get
            {
                return m_Header;
            }
            set
            {
                m_Header = value;
            }
        }
        private string m_Footer;
        /// <summary>
        /// ��β
        /// </summary>
        public string Footer
        {
            get
            {
                return m_Footer;
            }
            set
            {
                m_Footer = value;
            }
        }
        private BindItem<T> m_BindItem;
        /// <summary>
        /// ������ģ�淽��
        /// </summary>
        public BindItem<T> BindItem
        {
            get
            {
                return m_BindItem;
            }
            set
            {
                m_BindItem = value;
            }
        }

        private IEnumerable m_DataSource;
        /// <summary>
        /// ��ͷ
        /// </summary>
        public IEnumerable DataSource
        {
            get
            {
                return m_DataSource;
            }
            set
            {
                m_DataSource = value;
            }
        }

        public string GetHtml()
        {
            StringBuilder htmlWriter = new StringBuilder();
            htmlWriter.Append(m_Header);
            Int32 rowIndex = 0;
            if (m_DataSource != null)
            {
                foreach (T item in m_DataSource)
                {
                    rowIndex++;
                    htmlWriter.Append(m_BindItem(item, rowIndex));
                }
            }
            htmlWriter.Append(m_Footer);
            return htmlWriter.ToString();
        }
        /// <summary>
        /// ��IEnumeratorת��ΪIEnumerator<T>"/>
        /// </summary>
        /// <param name="iEnumerator"></param>
        /// <returns></returns>
        public IEnumerable<T> ConvertIEnumerable(System.Collections.IEnumerable iEnumerable)
        {
            System.Collections.Generic.List<T> result = new System.Collections.Generic.List<T>();
            foreach (T item in iEnumerable)
            {
                result.Add(item);
            }
            return (IEnumerable<T>)result;
        }
    }
}
