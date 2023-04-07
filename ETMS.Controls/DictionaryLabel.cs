using System;
using System.Collections.Generic;

namespace ETMS.Controls
{

    /// <summary>
    /// 载入字典单项数据回调方法
    /// </summary>
    /// <param name="dic_TypeName">字典名称</param>
    /// <param name="dicIDValue">字典项值</param>
    /// <returns>字典数据 期望的DataTable(ColumnNameValue,ColumnCodeValue)</returns>
    public delegate string LoadDictionaryItemDataHandler(string dic_TypeName, string dicIDValue);

    public delegate string LoadDictionaryToolTipHandler(string dic_TypeName, string dicIDValue);

    public class DictionaryLabel : System.Web.UI.WebControls.Label
    {
        /// <summary>
        /// 通过回调方式获取字典数据源
        /// </summary>
        public static IDictionary<string, LoadDictionaryItemDataHandler> HandlerDictionarys = new Dictionary<string, LoadDictionaryItemDataHandler>(StringComparer.InvariantCultureIgnoreCase);
        public static IDictionary<string, LoadDictionaryToolTipHandler> HandlerDictionarysTip = new Dictionary<string, LoadDictionaryToolTipHandler>(StringComparer.InvariantCultureIgnoreCase);


        /// <summary>
        /// 数据字典类型
        /// </summary>
        public String DictionaryType { get; set; }

        /// <summary>
        /// 字典ID，通过字典ID获取Name
        /// 注意：需要通过ViewState保存，防止页面回传时值丢失
        /// </summary>
        public String FieldIDValue
        {
            get
            {
                return (string)ViewState["FieldIDValue"];
            }
            set
            {
                ViewState["FieldIDValue"] = value;
            }
        }


        /// <summary>
        /// 字典ID，通过字典ID获取ToolTip
        /// 注意：需要通过ViewState保存，防止页面回传时值丢失
        /// </summary>
        public String FieldToolTipValue
        {
            get
            {
                return (string)ViewState["FieldToolTipValue"];
            }
            set
            {
                ViewState["FieldToolTipValue"] = value;
            }
        }


        /// <summary>
        /// 截取字符串长度
        /// </summary>
        public int TextLength
        {
            get
            {
                if (ViewState["TextLength"] == null)
                {
                    ViewState["TextLength"] = 0;
                }
                return (int)ViewState["TextLength"];
            }
            set
            {
                ViewState["TextLength"] = value;
            }
        }

        public override void DataBind()
        {
            base.DataBind();
            if (!IsCalled)//如果没有调用，则执行
            {
                OnPreRender(null);
            }
            
        }

        bool IsCalled = false;
        protected override void OnPreRender(EventArgs e)
        {
            //如果已经执行，则跳过
            if (IsCalled)
            {
                return;
            }
            //如果没有调用，则执行
            IsCalled = true;
            if (FieldIDValue == "")
            {
                this.Text = "";
            }
            else
            {
                LoadDictionaryItemDataHandler handler = HandlerDictionarys.ContainsKey(this.DictionaryType) ? HandlerDictionarys[this.DictionaryType] : null;
                if (handler != null)
                {
                    this.Text = handler(this.DictionaryType, this.FieldIDValue);//调用方法获取
                    if (this.FieldToolTipValue == null)
                        this.ToolTip = this.Text;
                    else
                    {
                        LoadDictionaryToolTipHandler handlerTip = HandlerDictionarysTip.ContainsKey(this.DictionaryType) ? HandlerDictionarysTip[this.DictionaryType] : null;
                        this.ToolTip = handlerTip(this.DictionaryType, this.FieldToolTipValue);//调用方法获取;
                        if(this.ToolTip == "")
                            this.ToolTip = this.Text;
                    }
                }
            }
        }
    }
}
