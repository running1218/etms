
namespace ETMS.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    /// 自定义分页控件
    /// 说明：此控件继承自GridView，因此，具备GridView的所有呈现功能！
    /// 可自定义的属性：
    ///     EmptyDataSourceTip
    ///     FirstPageImageUrl
    ///     PrePageImageUrl
    ///     NextPageImageUrl
    ///     LastPageImageUrl
    ///     GoToPageImageUrl
    /* 使用范例：
    protected void Page_Load(object sender, EventArgs e)
    {       
	    //如果需要分页，则请设置为true
        this.customGridViewExample.CustomAllowPaging = false;
	    //取消自动创建列功能
        this.customGridViewExample.AutoGenerateColumns = false;
	    //如果查询后数据为空时，默认提供的空数据类型，控件自动按照此数据类型模拟一条空数据。
        this.customGridViewExample.EmptyDataType = typeof(System.Data.DataView)
	    //调用自定义分页空间的初始化函数（每次Page_Load事件都需要执行！）
        this.customGridViewExample.Initialization(new IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            //如果需要分页的话，请设置此值。
            totalRecordCount = 0;
            //在此调用你自身的业务方法提前数据
            return new Object[]{new object()};
        }), null);

	    //可根据实际情况决定是否在页面载入时绑定数据
        if (!Page.IsPostBack)
        {
            this.customGridViewExample.CustomDataBind();
        }
    }
    */
    /// </summary>
    [SupportsEventValidation, Designer("System.Web.UI.Design.WebControls.GridViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultEvent("SelectedIndexChanged"), ControlValueProperty("SelectedValue")]
    public class CustomGridView : GridView
    {
        /// <summary>
        ///  默认取消分页,开启页脚（自定义分页）
        /// </summary>
        public CustomGridView()
        {
            if (DesignMode)
                return;

            this.AllowPaging = false;
            this.ShowFooter = false;
        }

        #region 事件处理
        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            if (!string.IsNullOrEmpty(this.Page.Request.Form[this.RequestPageClientID]))
            {
                //如果有翻页请求，则进行翻页操作！
                int newPageIndex = Convert.ToInt32(this.Page.Request.Form[this.RequestPageClientID]);
                newPageIndex = newPageIndex >= this.CurrentPageCount ? this.CurrentPageCount : newPageIndex;
                //更新页面索引
                this.CurrentPageIndex = newPageIndex;
                this.OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));
            }
        }
        #endregion

        #region 私有成员
        private string ScriptFormatString
        {
            get
            {
                return string.Format("javascript:document.getElementById(\"{0}\").value={1};document.forms[\"{2}\"].submit();", this.RequestPageClientID, "{0}", this.Page.Form.ClientID);
            }
        }
        private string PageFormatString = @"<a href='{0}'><img border='0' src='{1}' /></a>";

        private bool IsCallComstomDataBoundFunction = false;

        private string RequestPageClientID
        {
            get
            {
                return string.Format("{0}_CustomGridView_RequestPageClientID", this.ID);
            }
        }
        #endregion

        #region 公开属性
        /// <summary>
        /// 当前记录集是否为空，方便用于GridView_DataRowBound事件过滤
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (ViewState["IsEmpty"] == null)
                    ViewState["IsEmpty"] = false;
                return (bool)ViewState["IsEmpty"];
            }
            set
            {
                ViewState["IsEmpty"] = value;
            }
        }

        private IList m_EmptyData;
        /// <summary>
        /// 空数据时提供的绑定实例
        /// 注意：EmptyData必须实现System.Collections.ICollection接口！
        /// </summary>
        public IList EmptyData
        {
            get
            {
                return m_EmptyData;
            }
            set
            {
                m_EmptyData = value;
            }
        }
        private Type m_EmptyDataType = typeof(DataView);//默认绑定数据源为DataView
        /// <summary>
        /// 空数据项类型
        /// </summary>
        public Type EmptyDataType
        {
            get
            {
                return m_EmptyDataType;
            }
            set
            {
                if (value.IsAbstract)
                {
                    throw new Exception(string.Format("{0}抽象类型，不允许实例化，请通过设置EmptyData来完成空数据填充！", value.ToString()));
                }
                bool isFindDefaultConstructorInfo = false;
                foreach (System.Reflection.ConstructorInfo constructorInfo in value.GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (constructorInfo.GetParameters().Length == 0)
                    {
                        isFindDefaultConstructorInfo = true;
                        break;
                    }
                }
                if (!isFindDefaultConstructorInfo)
                {
                    throw new Exception(string.Format("{0}，没有公开的默认构造函数。", value.ToString()));
                }
                m_EmptyDataType = value;
            }
        }

        /// <summary>
        /// 空数据源提示信息
        /// 默认：“没有任何记录！"
        /// </summary>
        public string EmptyDataSourceTip
        {
            get
            {
                if (ViewState["EmptyDataSourceTip"] == null)
                    ViewState["EmptyDataSourceTip"] = "没有任何记录！";
                return (string)ViewState["EmptyDataSourceTip"];
            }
            set
            {
                ViewState["EmptyDataSourceTip"] = value;
            }
        }
        /// <summary>
        /// 首页图片路径
        /// </summary>
        public string FirstPageImageUrl
        {
            get
            {
                return (string)ViewState["FirstPageImageUrl"];
            }
            set
            {
                ViewState["FirstPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// 前页面图片路径
        /// </summary>
        public string PrePageImageUrl
        {
            get
            {
                return (string)ViewState["PrePageImageUrl"];
            }
            set
            {
                ViewState["PrePageImageUrl"] = value;
            }
        }
        /// <summary>
        /// 后页面图片路径
        /// </summary>
        public string NextPageImageUrl
        {
            get
            {
                return (string)ViewState["NextPageImageUrl"];
            }
            set
            {
                ViewState["NextPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// 尾页面图片路径
        /// </summary>
        public string LastPageImageUrl
        {
            get
            {
                return (string)ViewState["LastPageImageUrl"];
            }
            set
            {
                ViewState["LastPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// GO图片路径
        /// </summary>
        public string GoToPageImageUrl
        {
            get
            {
                return (string)ViewState["GoToPageImageUrl"];
            }
            set
            {
                ViewState["GoToPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// 页数
        /// </summary>
        public int CurrentPageCount
        {
            get
            {
                if ((this.TotalRecordCount / this.PageSize) > 0)//如果记录大于每页需求数
                {
                    if ((TotalRecordCount % PageSize) == 0)
                        return TotalRecordCount / PageSize;
                    else
                    {
                        return TotalRecordCount / PageSize + 1;
                    }
                }
                else　//不足一页
                    return 1;
            }
        }
        /// <summary>
        /// 当前页面索引
        /// 默认返回第一页
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] == null)
                {
                    return 1;
                }
                return (int)ViewState["CurrentPageIndex"];
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }
        /// <summary>
        /// 是否允许分页
        /// 注：屏蔽了本身的AllowPaging属性
        /// </summary>
        public bool CustomAllowPaging
        {
            get
            {
                if (ViewState["CustomAllowPaging"] == null)
                {
                    return false;
                }
                return (bool)ViewState["CustomAllowPaging"];
            }
            set
            {
                ViewState["CustomAllowPaging"] = value;
                this.ShowFooter = value;
            }
        }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalRecordCount
        {
            get
            {
                if (ViewState["TotalRecordCount"] == null)
                {
                    return 0;
                }
                return (int)ViewState["TotalRecordCount"];
            }
            set
            {
                this.ViewState["TotalRecordCount"] = value;
            }
        }

        /// <summary>
        /// 分页选择项
        /// </summary>
        public List<string> CheckValues
        {
            get
            {
                if (null != ViewState["CheckValues"])
                {
                    return (List<string>)ViewState["CheckValues"];
                }
                else
                {
                    return new List<string>();
                }
            }
            set
            {
                ViewState["CheckValues"] = value;
            }
        }

        /// <summary>
        /// 所有选中的数据项
        /// </summary>
        public List<string> AllCheckValues
        {
            get
            {
                List<string> checkedValues = this.CheckValues;

                if (this.IsRemeberChecks)
                {
                    foreach (GridViewRow row in this.Rows)
                    {
                        foreach (Control cell in row.Controls)
                        {
                            foreach (Control control in cell.Controls)
                            {
                                if (control is CheckBox)
                                {
                                    string index = string.Empty;
                                    foreach (DictionaryEntry key in this.DataKeys[row.RowIndex].Values)
                                    {
                                        index = key.Value.ToString();

                                        if (!checkedValues.Contains(index) && ((CheckBox)control).Checked)
                                        {
                                            checkedValues.Add(index);
                                        }

                                        if (checkedValues.Contains(index) && !((CheckBox)control).Checked)
                                        {
                                            checkedValues.Remove(index);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return checkedValues;
            }
        }

        /// <summary>
        /// 是否分页记录选择数据
        /// </summary>
        public bool IsRemeberChecks
        {
            get
            {
                if (null != ViewState["IsRemeberChecks"])
                {
                    return (bool)ViewState["IsRemeberChecks"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["IsRemeberChecks"] = value;
            }
        }

        private IPageDataSource m_PageDataSourceImpl;
        public IPageDataSource PageDataSourceImpl
        {
            get
            {
                return m_PageDataSourceImpl;
                //return (IPageDataSource)ViewState["PageDataSourceImpl"];
            }
            set
            {
                m_PageDataSourceImpl = value;
                //ViewState["PageDataSourceImpl"] = value;
            }
        }
        private GridViewCommandEventHandler m_RowComandImpl;
        public GridViewCommandEventHandler RowComandImpl
        {
            get
            {
                return m_RowComandImpl;
                //return (GridViewCommandEventHandler)ViewState["RowComandImpl"];
            }
            set
            {
                m_RowComandImpl = value;
                //ViewState["RowComandImpl"] = value;
            }
        }
        /// <summary>
        /// 自动创建列插入位置，从索引0开始
        /// </summary>
        public int AutoCreateColumnInsertIndex
        {
            get
            {
                if (ViewState["AutoCreateColumnInsertIndex"] == null)
                {
                    return 0;
                }
                return (int)ViewState["AutoCreateColumnInsertIndex"];
            }
            set
            {
                this.ViewState["AutoCreateColumnInsertIndex"] = value;
            }
        }

        /// <summary>
        /// 以","分割列索引
        /// </summary>
        public string FootableColIndexs
        { 
           get
           {
               if (ViewState["FootableColIndexs"] == null)
                   return string.Empty;
               else
                   return (string)ViewState["FootableColIndexs"];
           }
            set
            {
                this.ViewState["FootableColIndexs"] = value;
            }
           
        }

        /// <summary>
        /// 控件初始化
        /// 说明：如果需要使用此控件的分页功能，请在页面Page_Load事件中调用此函数初始化次控件。
        /// </summary>
        /// <param name="pageDataSourceImpl">分页数据源</param>
        /// <param name="rowComandImpl">行命令处理器</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, null, null);
        }
        /// <summary>
        /// 控件初始化
        /// 说明：如果需要使用此控件的分页功能，请在页面Page_Load事件中调用此函数初始化次控件。
        /// </summary>
        /// <param name="pageDataSourceImpl">分页数据源</param>
        /// <param name="rowComandImpl">行命令处理器</param>
        /// <param name="emptyDataType">待绑定的数据类型（要求：此类型必须有公开的默认构造函数。用法：如果提取的数据为空，则依赖此类型通过反射方式自动创建一空数据实例。）</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, Type emptyDataType)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, emptyDataType, null);
        }
        /// <summary>
        /// 控件初始化
        /// 说明：如果需要使用此控件的分页功能，请在页面Page_Load事件中调用此函数初始化次控件。
        /// </summary>
        /// <param name="pageDataSourceImpl">分页数据源</param>
        /// <param name="rowComandImpl">行命令处理器</param>
        /// <param name="emptyData">模拟一条空数据</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, IList emptyData)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, null, emptyData);
        }
        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="pageDataSourceImpl"></param>
        /// <param name="rowComandImpl"></param>
        /// <param name="emptyDataType"></param>
        /// <param name="emptyData"></param>
        private void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, Type emptyDataType, IList emptyData)
        {
            this.FirstPageImageUrl = "~/Resources/Images/Static/FirstPage.gif";
            this.NextPageImageUrl = "~/Resources/Images/Static/NextPage.gif";
            this.PrePageImageUrl = "~/Resources/Images/Static/PrePage.gif";
            this.LastPageImageUrl = "~/Resources/Images/Static/LastPage.gif";
            this.GoToPageImageUrl = "~/Resources/Images/Static/go.gif";
            this.PageDataSourceImpl = pageDataSourceImpl;
            this.RowComandImpl = rowComandImpl;
            if (emptyDataType != null)
                this.EmptyDataType = emptyDataType;
            if (emptyData != null)
                this.EmptyData = emptyData;
        }
        /// <summary>
        /// 自定义数据绑带
        /// 替代GridView自带的DataBind()方法
        /// </summary>
        public void CustomDataBind()
        {
            if (this.PageDataSourceImpl != null)
            {
                int Total;
                IList dataList = this.PageDataSourceImpl(this.CurrentPageIndex, this.PageSize, out Total);
                //如果提取当前页的数据为空，且当前页不是首页，则当前页号-1，读取前一页数据。
                if (dataList.Count == 0 && this.CurrentPageIndex > 1)
                {
                    this.CurrentPageIndex--;
                    dataList = this.PageDataSourceImpl(this.CurrentPageIndex, this.PageSize, out Total);
                }
                this.DataSource = dataList;
                this.TotalRecordCount = Total;
                base.DataBind();
                this.IsCallComstomDataBoundFunction = true;
            }
        }
        #endregion

        #region 重载基类方法
        /// <summary>
        /// 默认CssClass名称"gridview"
        /// </summary>
        public override string CssClass
        {
            get
            {
                if (string.IsNullOrEmpty(base.CssClass))
                {
                    base.CssClass = "gridview";
                }
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
            }
        }
        [DefaultValue((string)null), Editor("System.Web.UI.Design.WebControls.DataControlFieldTypeEditor, System.Design", typeof(UITypeEditor)), Description("DataControls_Columns"), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty), Category("Default")]
        public override DataControlFieldCollection Columns
        {
            get
            {
                return base.Columns;
            }
        }
        [DefaultValue((string)null), TypeConverter(typeof(StringArrayConverter)), Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design", typeof(UITypeEditor))]
        public override string[] DataKeyNames
        {
            get
            {
                return base.DataKeyNames;
            }
            set
            {
                base.DataKeyNames = value;
            }
        }
        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0", typeof(UITypeEditor)), DefaultValue(""), UrlProperty]
        public override string BackImageUrl
        {
            get
            {
                return base.BackImageUrl;
            }
            set
            {
                base.BackImageUrl = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            if (DesignMode)
                return;

            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.PageIndexChanging += new GridViewPageEventHandler(CustomGridView_PageIndexChanging);
            this.RowCancelingEdit += new GridViewCancelEditEventHandler(CustomGridView1_RowCancelingEdit);
            this.RowEditing += new GridViewEditEventHandler(CustomGridView1_RowEditing);
            this.RowUpdating += new GridViewUpdateEventHandler(CustomGridView1_RowUpdating);
            this.RowCommand += new GridViewCommandEventHandler(CustomGridView_RowCommand);
            this.RowDataBound += new GridViewRowEventHandler(CustomGridView_RowDataBound);
            base.OnInit(e);
        }
        void CustomGridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        void CustomGridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        void CustomGridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        void CustomGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (this.RowComandImpl != null)
            {
                this.RowComandImpl(sender, e);
                this.CustomDataBind();
            }
        }

        void CustomGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //footable style
                SetFootableRowStyle(e.Row);
            }
        }

        protected void CustomGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.CurrentPageIndex = e.NewPageIndex;
            this.CustomDataBind();
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (DesignMode)
                return;

            this.AllowPaging = false;
            if (!this.IsCallComstomDataBoundFunction && this.IsEmpty)
            {
                OnDataBound(e);
            }

            if (string.IsNullOrEmpty(this.CssClass))//默认皮肤样式
            {
                //控件默认风格定义
                this.CellPadding = 4;
                this.CellSpacing = 1;
                this.GridLines = GridLines.None;
                this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");
                this.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                this.Font.Size = new FontUnit("9pt");
                this.Width = new Unit("100%");

                this.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
                this.HeaderStyle.Font.Bold = false;
                this.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#165967");

                this.RowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                this.RowStyle.HorizontalAlign = HorizontalAlign.Left;
                this.RowStyle.Height = new Unit("25px");

                this.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAFAFA");

                this.EditRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");

                this.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DDF1");
                this.SelectedRowStyle.Font.Bold = true;
                this.SelectedRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");

                this.FooterStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
                this.FooterStyle.Font.Bold = true;
                this.FooterStyle.Height = new Unit("25px");

                this.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#507C81");
                this.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
                this.PagerStyle.VerticalAlign = VerticalAlign.Middle;
                this.PagerStyle.Wrap = false;
                this.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                this.EmptyDataRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
            }          

            base.OnPreRender(e);
        }
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (!DesignMode)
                {
                    IList dataList = (IList)value;
                    if (dataList.Count == 0)
                    {
                        if (this.m_EmptyData == null && this.m_EmptyDataType == null)
                        {
                            throw new Exception("至少得设置：EmptyData,EmptyDataType中的一项。");
                        }
                        this.IsEmpty = true;
                        if (this.EmptyData == null)
                        {
                            //追加空数据行
                            if (value is DataView)
                            {
                                DataRowView rowView = ((DataView)value).AddNew();
                                foreach (DataColumn col in ((DataView)value).Table.Columns)
                                {
                                    object defaultValue = null;
                                    if (col.DataType.Equals(typeof(System.String)))
                                    {
                                        defaultValue = string.Empty;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Guid)))
                                    {
                                        defaultValue = Guid.Empty;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Boolean)))
                                    {
                                        defaultValue = false;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Int64)) || col.DataType.Equals(typeof(System.Int32)) || col.DataType.Equals(typeof(System.Int16)))
                                    {
                                        defaultValue = 0;
                                    }
                                    else if (col.DataType.Equals(typeof(System.DateTime)))
                                    {
                                        defaultValue = DateTime.MinValue;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Single)) || col.DataType.Equals(typeof(System.Decimal)) || col.DataType.Equals(typeof(System.Double)))
                                    {
                                        defaultValue = 0;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("不支持类型为{0}列，建议修改成String类型！", col.DataType));
                                    }
                                    rowView[col.ColumnName] = defaultValue;
                                }
                            }
                            else
                            {
                                IList ilist = (IList)value;
                                Type dataType = this.m_EmptyDataType;

                                if (value.GetType().IsArray)
                                {
                                    object emptyItem = Activator.CreateInstance(value.GetType().GetElementType());
                                    ilist = Array.CreateInstance(emptyItem.GetType(), 1);
                                    ilist[0] = emptyItem;
                                    value = ilist;
                                }
                                else
                                {
                                    object emptyItem = Activator.CreateInstance(value.GetType().GetGenericArguments()[0]);
                                    ilist.Add(emptyItem);
                                }
                            }
                        }
                        else
                        {
                            while (this.EmptyData.Count > 1)
                            {
                                this.EmptyData.RemoveAt(this.EmptyData.Count - 1);
                            }
                            value = this.EmptyData;
                        }
                    }
                    else
                    {
                        this.IsEmpty = false;
                    }
                }
                base.DataSource = value;
            }
        }

        protected override void OnDataBound(EventArgs e)
        {
            if (DesignMode)
                return;

            base.OnDataBound(e);
            if (IsEmpty)
            {
                if (this.Rows.Count > 0)
                {
                    this.Rows[0].Cells.Clear();
                    TableCell tc = new TableCell();
                    tc.Text = this.EmptyDataSourceTip;
                    tc.ColumnSpan = CellVisibleCount();
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    this.Rows[0].Cells.Add(tc);
                }
            }
            if (CustomAllowPaging)
            {
                this.FooterRow.Cells.Clear();
                TableCell tc = new TableCell();
                this.FooterRow.Cells.Add(tc);
                tc.ColumnSpan = this.HeaderRow.Cells.Count;
                tc.HorizontalAlign = HorizontalAlign.Center;
                if (!IsEmpty)
                {
                    tc.Text += string.Format("共{0}条&nbsp;&nbsp;", this.TotalRecordCount, this.CurrentPageCount);
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == 1 ? "#" : string.Format(ScriptFormatString, 1), GetNormalRelationPath(this.FirstPageImageUrl));
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == 1 ? "#" : string.Format(ScriptFormatString, this.CurrentPageIndex - 1), GetNormalRelationPath(this.PrePageImageUrl), this.CurrentPageIndex > 1 ? "" : "return;");
                    tc.Text += string.Format("&nbsp;{0}/{1}&nbsp;", this.CurrentPageIndex, this.CurrentPageCount);
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == this.CurrentPageCount ? "#" : string.Format(ScriptFormatString, this.CurrentPageIndex + 1), GetNormalRelationPath(this.NextPageImageUrl), this.CurrentPageIndex < this.CurrentPageCount ? "" : "return;");
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == this.CurrentPageCount ? "#" : string.Format(ScriptFormatString, this.CurrentPageCount), GetNormalRelationPath(this.LastPageImageUrl), this.CurrentPageIndex < this.CurrentPageCount ? "" : "return;");
                    tc.Text += string.Format("&nbsp;<input type='text' id='{0}' name='{0}' style='width:40px' maxlength='4' value='{1}'/>", this.RequestPageClientID, this.CurrentPageIndex);
                    tc.Text += string.Format("<a href='javascript:document.forms[\"{1}\"].submit()'><img border='0' src='{0}' /></a>", GetNormalRelationPath(this.GoToPageImageUrl), this.Page.Form.ClientID);
                }
            }
        }

        private int CellVisibleCount()
        {
            int cells = 0;
            int columns = 0;

            foreach (TableCell cell in this.HeaderRow.Cells)
            {
                var dataCell = (System.Web.UI.WebControls.DataControlFieldCell)cell;
                if (cell.Visible && !dataCell.ContainingField.HeaderStyle.CssClass.Contains("hide"))
                    cells++;
            }

            foreach (DataControlField col in this.Columns)
            {
                if (col.Visible)
                    columns++;
            }

            return cells <= columns ? cells : columns;
        }
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ArrayList list = base.CreateColumns(dataSource, useDataSource) as ArrayList;
            if (AutoCreateColumnInsertIndex > this.Columns.Count - 1)//如果错误指定插入位置，则按照系统默认方式提供
            {
                return list;
            }
            ArrayList newList = new ArrayList();
            int autoCreateColumnsStartIndex = this.Columns.Count;
            for (int i = 0; i <= AutoCreateColumnInsertIndex; i++)
            {
                newList.Add(list[i]);
            }
            for (int i = autoCreateColumnsStartIndex; i < list.Count; i++)
            {
                newList.Add(list[i]);
            }
            for (int i = AutoCreateColumnInsertIndex + 1; i < this.Columns.Count; i++)
            {
                newList.Add(list[i]);
            }
            return newList;
        }
        #endregion

        public static string GetNormalRelationPath(string ASPNETRelationPath)
        {
            if (string.IsNullOrEmpty(ASPNETRelationPath))
            {
                return ASPNETRelationPath;
            }
            string root = System.Web.HttpContext.Current.Request.ApplicationPath;
            return ASPNETRelationPath.Replace("~", root == "/" ? "" : root);
        }

        /// <summary>
        /// 获取选择的项主键值
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="checkboxColumnIndex"></param>
        /// <param name="checkboxID"></param>
        /// <returns></returns>
        public static T[] GetSelectedValues<T>(GridView gridView, int checkboxColumnIndex = 0, string checkboxID = "CheckBox1")
        {
            List<T> list = new List<T>();
            foreach (GridViewRow row in gridView.Rows)
            {
                CheckBox control = row.Cells[checkboxColumnIndex].FindControl(checkboxID) as CheckBox;
                if (control != null && control.Checked)
                {
                    list.Add((T)gridView.DataKeys[row.RowIndex].Value);
                }
            }
            return list.ToArray();
        }

        private void SetFootableRowStyle(GridViewRow row)
        {
            Regex reg = new Regex(@"^[-]?\d+[.]?\d*$");            
            TableCellCollection headerCells = row.Cells;

            if (string.IsNullOrEmpty(FootableColIndexs) && this.CssClass == "footable")
            { 
                FootableColIndexs = string.Join(",", new string[]{"0", (headerCells.Count -1).ToString()});
            }

            string[] cellIndexs = FootableColIndexs.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < headerCells.Count; i++)
            {
                for (int j = 0; j < cellIndexs.Length; j++)
                {
                    if (reg.IsMatch(cellIndexs[j]) && i == int.Parse(cellIndexs[j]))
                    {
                        headerCells[i].Attributes.Add("data-ignore", "true");
                        continue;
                    }

                    headerCells[i].Attributes.Add("data-hide", "tablet");
                }
            }
        }

    }
}
