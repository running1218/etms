using System;
using System.Collections.Generic;

namespace ETMS.Controls
{
    /// <summary>
    /// 载入字典数据回调方法
    /// </summary>
    /// <param name="dic_TypeName">字典名称</param>
    /// <returns>字典数据 期望的DataTable(ColumnNameValue,ColumnCodeValue)</returns>
    public delegate System.Data.DataTable LoadDictionaryDataHandler(string dic_TypeName);

    public class DictionaryDropDownList : System.Web.UI.WebControls.DropDownList
    {
        public DictionaryDropDownList()
        {
            IsShowAll = true;
            IsShowChoose = false;
        }

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

        /// <summary>
        /// 是否显示全部（用于查询功能）
        /// </summary>
        public bool IsShowAll { get; set; }

        /// <summary>
        /// 是否显示请选择（用于表单录入功能）
        /// </summary>
        public bool IsShowChoose { get; set; }

        /// <summary>
        /// 预设默认选中的值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 选择默认值（用于表单录入页面，【非必填】字段时设定，防止外键约束问题）
        /// </summary>
        public string SelectedDefaultValue { get; set; }

        //做为绑定控件出现
        public override void DataBind()
        {
            LoadData();
            base.DataBind();
        }
        //做为页面控件载入
        protected override void OnPagePreLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
                base.OnPagePreLoad(sender, e);
            }
        }

        bool IsCalled = false;

        private void LoadData()
        {
            if (this.IsCalled)
            {
                return;
            }
            this.IsCalled = true;

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
            //if (string.IsNullOrEmpty(this.DataTextField))
            //{
            //    this.DataTextField = "ColumnNameValue";//默认名称
            //}
            //if (string.IsNullOrEmpty(this.DataValueField))
            //{
            //    this.DataValueField = "ColumnCodeValue";//默认编码
            //}
            //this.DataSource = dt;
            //base.DataBind();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                this.Items.Add(new System.Web.UI.WebControls.ListItem(Convert.ToString(row["ColumnNameValue"]), Convert.ToString(row["ColumnCodeValue"])));
            }            

            if (IsShowChoose)
            {
                if (this.SelectedDefaultValue == null)
                {
                    this.SelectedDefaultValue = "";
                }
                this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择", this.SelectedDefaultValue));
            }
            else if (IsShowAll)
            {
                if (DictionaryType == "Dic_DegreeDifficulty" || DictionaryType == "Dic_QuestionType")
                {
                    this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("全部", "0"));
                }
                else
                {
                    this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("全部", "-1"));
                }
            }

            if (!string.IsNullOrEmpty(DefaultValue))
            {
                this.SelectedValue = DefaultValue;
            }
        }
    }
}
