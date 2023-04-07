using System;
using System.Collections.Generic;

namespace ETMS.Controls
{
    public class DictionaryRadioButtonList : System.Web.UI.WebControls.RadioButtonList
    {
        /// <summary>
        /// 通过回调方式获取字典数据源
        /// </summary>
        public static IDictionary<string, LoadDictionaryDataHandler> HandlerDictionarys = new Dictionary<string, LoadDictionaryDataHandler>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// 通过本地缓存方式获取字典数据源
        /// </summary>
        public static IDictionary<string, System.Data.DataTable> Dictionarys = new Dictionary<string, System.Data.DataTable>(StringComparer.InvariantCultureIgnoreCase);
        /// <summary>
        /// 数据字典类型
        /// </summary>
        public String DictionaryType { get; set; }

        public string CheckedValue { get; set; }

        protected override void OnPagePreLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                System.Data.DataTable dt = null;
                LoadDictionaryDataHandler handler = HandlerDictionarys.ContainsKey(this.DictionaryType) ? HandlerDictionarys[this.DictionaryType] : null;
                if (handler != null)
                {
                    dt = handler(this.DictionaryType);//调用方法获取
                }
                else
                {
                    dt = Dictionarys.ContainsKey(this.DictionaryType) ? Dictionarys[this.DictionaryType] : null;//从控件字典中获取
                }
                if (string.IsNullOrEmpty(this.DataTextField))
                {
                    this.DataTextField = "ColumnNameValue";//默认名称
                }
                if (string.IsNullOrEmpty(this.DataValueField))
                {
                    this.DataValueField = "ColumnCodeValue";//默认编码
                }
                this.DataSource = dt;
                this.DataBind();
                if (this.Items.Count > 0)
                {
                    this.SelectedValue = CheckedValue;
                }
            }

            base.OnPagePreLoad(sender, e);
        }


    }
}
